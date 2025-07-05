
using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEditorInternal;
using System.Linq;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public TextMeshProUGUI livesText;
    public TextMeshProUGUI coinsText;
    public TextMeshProUGUI warningText;
    public string location;
    public GameObject warningScreen;
    public bool canTransition;

    public GameObject coinsScreen;
    public GameObject livesScreen;
    public GameObject abdurahmanHealthScreen;
    public float fadeNums;
    public GameObject fade;

    public bool startFader;
    public int fadeNumPeriod;
    public bool start = false;
    public bool isDead = false;
    public bool isAlive = true;
    public bool[] startTransitions;

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

        if (coinsScreen == null)
            coinsScreen = GameObject.Find("coinUI");

        if (livesScreen == null)
            livesScreen = GameObject.Find("livesUI");
        if (warningScreen == null)
            warningScreen = GameObject.Find("warningScreen");
        if (abdurahmanHealthScreen == null)
            abdurahmanHealthScreen = GameObject.Find("abdurahmanplayerhealth");

        ScreenControls();

    }

    void Start()
    {
        if (GameManager.instance != null && GameManager.instance.player != null && fade != null)
        {
            fade.transform.position = GameManager.instance.player.transform.position;
        }
        fadeNums = 2;
        location = "Abdurahman's House";
        
        startTransitions = new bool[] {
            // Start of RPG Text Box
            false,
            // Start of Person Name Text
            false,
            // Start of Location Announcer
            false,
        };
        StartCoroutine(ChangeTransitionBools());
    }

    void Update()
    {
        if (GameManager.instance?.player != null)
        {
            if (coinsText != null)
                coinsText.text = GameManager.instance.player.coinNumbers.ToString();
            if (livesText != null)
                livesText.text = GameManager.instance.playerLives.ToString();
        }
    }

    public void ScreenControls()
    {
        var player = GameManager.instance?.player;
        if (player != null)
        {
            if (coinsScreen != null) coinsScreen.SetActive(true);
            if (livesScreen != null) livesScreen.SetActive(true);
            if (warningScreen != null) warningScreen.SetActive(false);
            if (abdurahmanHealthScreen != null) abdurahmanHealthScreen.SetActive(true);
            Debug.Log("The Screen Controller works.");
        }
        else
        {
            if (coinsScreen != null) coinsScreen.SetActive(false);
            if (livesScreen != null) livesScreen.SetActive(false);
            if (abdurahmanHealthScreen != null) abdurahmanHealthScreen.SetActive(false);
        }
    }

    public void FadeController(int seconds)
    {
        Debug.Log("This parent fade function was called.");
        StartCoroutine(Fade(seconds));
    }

    public IEnumerator Fade(int seconds)
    {
        fadeNums = 1;

        if (fade == null || GameManager.instance?.camera == null)
        {
            Debug.Log("Fade or camera is null.");
            yield break;
        }

        GameObject fadeInObject = Instantiate(fade, GameManager.instance.camera.transform.position, Quaternion.identity);

        if (fadeInObject != null)
        {
            Debug.Log("This worked.");
        }
        else
        {
            Debug.Log("This doesn't work.");
        }

        yield return new WaitForSeconds(seconds);

        if (GameManager.instance.player != null)
        {
            fadeInObject.transform.position = GameManager.instance.camera.transform.position;
            fadeNums = 2;
        }
        else
        {
            Debug.Log("This fader is broken.");
        }
    }

    public IEnumerator TimetoDie(float duration)
    {
        Debug.Log("This function was called.");

        if (warningScreen != null && warningText != null)
        {
            warningScreen.SetActive(true);
            warningText.text = "Teleporting to rpgScene1";
        }

        if (GameManager.instance != null)
        {
            GameManager.instance.isDonewithPlatforming = true;
            GameManager.instance.startSpawnBoolPlatforming = true;
        }

        yield return new WaitForSeconds(duration);

        if (warningScreen != null)
            warningScreen.SetActive(false);

        SceneManager.LoadScene("rpgScene1");
    }
    public void SetStartBool()
    {
        start = true;
    }
    public void SetIsDeadandIsAliveBool(bool isDeadinner)
    {
        isDead = isDeadinner;
        isAlive = !isDeadinner;
    }
    public void SetStartBool(int value) => start = value == 1 ? true : false;
    public void SetCanTransitionBool(bool value)
    {
        canTransition = value;
        if (canTransition)
        {
            Debug.Log("Transitioning is enabled.");
        }
        else
        {
            Debug.Log("Transitioning is disabled.");
        }
    }
    public void SetLocation(string newLocation) => location = newLocation;
    public void ChangeStartTransitionsBool(int initialValue)
    {
        startTransitions[initialValue] = true;
    }
    public void ChangeStartTransitionsBoolArray(int initialValue, bool boolValue)
    {
        for (int i = 0; i < startTransitions.Length; i++)
        {
            if (initialValue == i)
            {
                startTransitions[i] = boolValue;
            }
        }
    }
    public IEnumerator ChangeTransitionBools()
    {
        
        
        startTransitions[2] = true;
        canTransition = true;
        yield return new WaitForSeconds(.3f);
        startTransitions[2] = false;
        yield break;

        
       
    }
    public void StartChangeTransitionBools() => StartCoroutine(ChangeTransitionBools());
}
