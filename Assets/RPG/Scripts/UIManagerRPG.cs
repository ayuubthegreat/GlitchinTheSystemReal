using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.UI;


public class UIManagerRPG : MonoBehaviour
{
    public static UIManagerRPG instance;
    public PlayerPhonePhysical playerPhone;
    [Header("Fadeable Objects")]
    public fader[] fadeableRPGObjects;
    [Header("Cutscene Related Objects")]
    public GameObject cutsceneParent;
    public GameObject cutsceneObjectPrefab;
    [Header("RPG Text Related Objects")]
    public GameObject rpgTextObject;
    public GameObject personNameObject;
    public GameObject phone;
    public GameObject person1DialogueAnimation;
    public GameObject person2DialogueAnimation;
    public Image cutsceneImageObject;
    [Header("Location Announcer Elements")]
    public RectTransform rectTransform;
    public TextMeshProUGUI text;
    public Vector3[] waypoints;
    public int waypointIndex = 0;
    public float yOffset = 10f;
    public float announcementSpeed = 5f;
    public string locationName = "Abdurahman's House";
    [Header("Image Cutscenes")]
    public Sprite[] cutsceneImageBackgrounds;
    [Header("Level Announcer Elements")]
    public Mover levelAnnouncerObject;
    public Transform endTransform;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI worldNumberText;
    public string levelSceneName = "Level1";
    [Header("Menu Elements")]
    public Mover settingsMenu;
    public GameObject options;
    public Sprite[] battleImageBackgrounds;
    public GameObject battleShortMenu;
    public Animator battleMovesAnimator;
    public healthBar playerHealthBar;
    public TextMeshProUGUI playerHealthText;
    public TextMeshProUGUI playerLevelText;
    public TextMeshProUGUI playerNameText;
    public healthBar enemyHealthBar;
    public TextMeshProUGUI enemyHealthText;
    public TextMeshProUGUI enemyLevelText;
    public TextMeshProUGUI enemyNameText;
    public GameObject playerStatsObject;
    public GameObject enemyStatsObject;
    public GameObject partyMemberScreen;
    public GameObject[] partyMembersProfileScreens;
    public TextMeshProUGUI[] partyMembersNames;
    public TextMeshProUGUI[] partyMembersLevels;
    public int currentSelectedPartyMember = 0;
    public AbdurahmanProfileScreen abdurahmanProfileScreen;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ControlRPGUIElements(false);
        SetLocationAnnouncerElements();
        ChangeText(locationName);
        StartMoveLocationAnnouncer(2f);
        battleMovesAnimator = battleShortMenu.GetComponent<Animator>();


    }

    // Update is called once per frame
    void Update()
    {
        rectTransform.anchoredPosition = Vector3.Lerp(rectTransform.anchoredPosition, waypoints[waypointIndex], Time.unscaledDeltaTime * announcementSpeed);
    }
    public void ControlRPGUIElements(bool isActive)
    {
        rpgTextObject.SetActive(isActive);
        personNameObject.SetActive(isActive);
        person1DialogueAnimation.SetActive(isActive);
        person2DialogueAnimation.SetActive(isActive);
        // battleShortMenu.SetActive(isActive);
    }
    public void SetLocationAnnouncerElements()
    {
        if (rectTransform == null)
        {
            Debug.LogError("RectTransform is not assigned in UIManagerRPG.");
            return;
        }
        waypoints = new Vector3[2];
        waypoints[0] = rectTransform.anchoredPosition;
        waypoints[1] = rectTransform.anchoredPosition + new Vector2(0, -yOffset); // Example offset for the second waypoint
    }
    public void ChangeLocationPosition()
    {
        rectTransform.anchoredPosition = Vector3.MoveTowards(rectTransform.anchoredPosition, waypoints[waypointIndex], Time.deltaTime * 5f);
    }
    public bool ChangeText(string newText = "")
    {
        if (text != null)
        {
            text.text = newText;


            return true; // Successfully changed the text
        }
        return false; // Failed to change the text, text component is null
    }
    public IEnumerator MoveLocationAnnouncer(float duration)
    {
        yield return new WaitForSeconds(1f);

        waypointIndex = 1;

        yield return new WaitForSeconds(duration);

        waypointIndex = 0;
    }
    public void StartMoveLocationAnnouncer(float duration)
    {
        rectTransform.anchoredPosition = waypoints[0]; // Reset position to the first waypoint
        waypointIndex = 0; // Start from the first waypoint
        StartCoroutine(MoveLocationAnnouncer(duration));
    }
    public void AnnounceLevel(string levelName, int worldNumber, string scene = "")
    {
        levelAnnouncerObject.AssignNewWaypointsAndMoveObject(new Vector2[] { new Vector2(-400, 0) }, 700f, false);
        levelSceneName = scene;

        levelText.text = levelName;
        worldNumberText.text = "World " + worldNumber.ToString();
    }
    public void HideLevelAnnouncer()
    {
        levelAnnouncerObject.AssignNewWaypointsAndMoveObject(new Vector2[] { new Vector2(400, 0) }, 700f, false);
    }
    public void LoadNewLevel()
    {
        GameManager.instance.LoadNewSceneReal(2, levelSceneName);
        GameManagerRPG.instance.playerpg.isMovable = false;
    }
    public void LoadOptionsMenu()
    {
        options.SetActive(!options.activeSelf);
        settingsMenu.gameObject.SetActive(!options.activeSelf);
    }
    public void UpdateBattleUIStats(string playerName, int playerLevel, int playerHealth, string enemyName, int enemyLevel, int enemyHealth)
    {
        playerStatsObject.SetActive(true);
        enemyStatsObject.SetActive(true);
        playerNameText.text = playerName;
        playerLevelText.text = "Lvl " + playerLevel.ToString();
        playerHealthText.text = "HP: " + playerHealth.ToString();

        enemyNameText.text = enemyName;
        enemyLevelText.text = "Lvl " + enemyLevel.ToString();
        enemyHealthText.text = "HP: " + enemyHealth.ToString();
    }

    public void EndBattle()
    {
        StartCoroutine(endBattle());
    }
    public IEnumerator endBattle()
    {
        UIManager.instance.fadeableGeneralObjects[0].StartFading(4f, 20f);
        yield return new WaitForSeconds(.5f);
        GameManagerRPG.instance.isInBattle = false;
        Camera.main.GetComponent<AudioSource>().clip = GameManagerRPG.instance.musicClips[1];
        Camera.main.GetComponent<AudioSource>().Play();

        battleShortMenu.SetActive(false);
        enemyStatsObject.SetActive(false);
        playerStatsObject.SetActive(false);
        foreach (GameObject enemy in GameManagerRPG.instance.enemiesInBattle)
        {
            Destroy(enemy);
        }
        fadeableRPGObjects[0].Fader(true, cutsceneImageBackgrounds[0]);
        yield return new WaitForSeconds(1f);
        GameManagerRPG.instance.ResetBattleStats();
        yield return new WaitForSeconds(1f);
        GameManagerRPG.instance.playerpg.isMovable = true;

    }
    public void LoadPartyProfileScreen()
    {
        partyMemberScreen.SetActive(!partyMemberScreen.activeSelf);
        if (partyMemberScreen.activeSelf)
        {
            UpdateCurrentSelectedPartyMember(0);
        }
    }
    public void UpdateCurrentSelectedPartyMember(int partyMemberIndex)
    {
        PartyMember memberStats = PartyManager.instance.partyMembers[partyMemberIndex];
        PartyManager.instance.partyMemberNameTextUI.text = memberStats.memberName;
        PartyManager.instance.partyMemberLevelTextUI.text = "Lvl " + memberStats.level.ToString();
        GameManagerRPG.instance.UpdateMovePower(memberStats);
        for (int i = 0; i < PartyManager.instance.moveDetailsPanels.Length; i++)
        {
            if (i < memberStats.assignedMoves.Length)
            {
                PartyManager.instance.moveDetailsPanels[i].SetActive(true);
                PartyManager.instance.moveDetailsTitlesUIs[i].text = memberStats.assignedMoves[i].moveName;
                memberStats.assignedMoves[i].power = memberStats.level * 2 * memberStats.attack;
                PartyManager.instance.moveDetailsTextUIs[i].text = "Power:" + memberStats.assignedMoves[i].power.ToString();
            }
            else
            {
                PartyManager.instance.moveDetailsPanels[i].SetActive(false);
                PartyManager.instance.moveDetailsTitlesUIs[i].text = "";
                PartyManager.instance.moveDetailsTextUIs[i].text = "";
            }
        }
    }
    public void UpdateProfileScreenUI()
    {
        abdurahmanProfileScreen.gameObject.SetActive(!abdurahmanProfileScreen.gameObject.activeSelf);
        if (abdurahmanProfileScreen.gameObject.activeSelf)
        {
            if (abdurahmanProfileScreen != null)
            {
                PartyMember memberStats = PartyManager.instance.partyMembers[0]; // Assuming Abdurahman is the first party member
                abdurahmanProfileScreen.coinsText.text = GameManagerRPG.instance.coins.ToString();
                abdurahmanProfileScreen.levelText.text = "Level: " + memberStats.level.ToString();
                abdurahmanProfileScreen.healthText.text = "Health: " + memberStats.health.ToString() + "/" + GameManagerRPG.instance.originalPlayerHealth.ToString();
            }
        }
    }
}
