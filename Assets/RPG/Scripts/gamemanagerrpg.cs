using System.Collections;
using Unity.VisualScripting;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UI;

public class GameManagerRPG : MonoBehaviour
{
    public CutsceneAssembler[] cutsceneAssemblers;
    public static GameManagerRPG instance;
    public AudioSource soundEffectSource;
    public Camera main;
    public CameraControllerRPG cameraController;
    public teleport[] phoneBooths;
    public float targetSize;
    public float cameraSpeed;
    public AudioSource source;
    public AudioClip[] musicClips;
    public AudioClip[] dialogueBlips;
    public AudioClip[] soundEffects;
    public playerpg playerpg;
    public GameObject startSpawnRPG;
    public Vector3 spawnObject;
    public GameObject mainMap;
    public GameObject battleMap;
    public GameObject playerHouse;
    public GameObject doorSpawn;
    public bool isDonewithPlatforming;
    public bool decreaseVolume;
    public bool increaseVolume;
    public bool isPhoneActive = false;
    public bool isCutsceneActive = false;
    public float audioSourceVolume = 1;
    public float fadeSpeed = 0.5f;
    public float moveSpeed = 5f;
    public bool iswalkingdoor = false;
    public bool movingAutonomously = false;
    public bool isInBattle = false;
    public bool isPaused = false;
    public int numberOfEnemies = 1;
    public int enemyHealth = 10;
    public int playerLevel = 1;
    public int enemyLevel = 1;
    public int playerHealth = 20;
    public bool isPlayerTurn = true;
    public GameObject[] enemiesInBattle;
    public GameObject battleEnemyPrefab;
    public GameObject[] battleAlliesPrefab;
    public GameObject[] moveButtons;
    public Move currentPlayerMove;
    public Move currentEnemyMove;
    public int currentAllyIndex = 0;
    public int currentEnemyIndex = 0;
    [System.Serializable]
    public struct CutsceneAssembler
    {
        public AnimatorController[] headAnims;
        public AnimatorController[] bodyAnims;
        public Vector3[] cutsceneObjectPositions;
        public float[] cutsceneObjectSizes;
        public string[] characterNames;
        public object[] additionalData; // This array can accept multiple types
    }

    void Awake()
    {
        instance = this;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        main = Camera.main;
        playerpg = FindFirstObjectByType<playerpg>();
        targetSize = main.orthographicSize;
        audioSourceVolume = GameManager.instance.musicVolume;
        phoneBooths = FindObjectsByType<teleport>(FindObjectsSortMode.None);
        battleAlliesPrefab[0].SetActive(false);

        if (GameManager.instance.startSpawnBool && GameManager.instance.phoneBoothSpawn == Vector3.zero)
        {
            spawnObject = startSpawnRPG.transform.position;
        }
        else
        {
            if (GameManager.instance.phoneBoothSpawn != Vector3.zero)
            {
                spawnObject = GameManager.instance.phoneBoothSpawn;
            }
        }
        if (GameManager.instance.DialogueProgression <= 3)
        {
            movingAutonomously = false;
            mainMap.SetActive(false);
            playerHouse.SetActive(true);
            isCutsceneActive = false;
            source.clip = musicClips[0];
            source.Play();
        }

    }

    // Update is called once per frame
    void Update()
    {
        source.volume = GameManager.instance.musicVolume;
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (UIManagerRPG.instance.options.activeSelf) return;
            MainMenu();
        }
        if (main.orthographicSize != targetSize)
        {
            main.orthographicSize = Mathf.Lerp(main.orthographicSize, targetSize, Time.unscaledDeltaTime * cameraSpeed);
        }


        if (decreaseVolume)
        {
            GradualVolumeDecrease();
        }
        else if (increaseVolume)
        {
            GradualVolumeIncrease();
        }



    }
    public void RespawnPlayerInCheckpoint(Vector3 newSpawnPoint, int index)
    {
        GameManager.instance.startSpawnBool = false;
        spawnObject = newSpawnPoint;
    }
    public void GradualVolumeDecrease()
    {
        if (!decreaseVolume)
        {
            return;
        }
        source.volume = Mathf.MoveTowards(GameManager.instance.musicVolume, 0f, fadeSpeed * Time.deltaTime);
        if (audioSourceVolume <= 0)
        {
            audioSourceVolume = 0;
            decreaseVolume = false;
            return;
        }
    }
    public void GradualVolumeIncrease()
    {
        if (!increaseVolume)
        {
            return;
        }
        source.volume = Mathf.MoveTowards(source.volume, 1f, fadeSpeed * Time.deltaTime);
        if (source.volume >= GameManager.instance.musicVolume)
        {
            source.volume = GameManager.instance.musicVolume;
            increaseVolume = false;
            return;
        }
    }
    public void StartFadingVolume(AudioClip clipNew, int waitDuration)
    {
        StopAllCoroutines();
        decreaseVolume = false;
        audioSourceVolume = GameManager.instance.musicVolume;
        StartCoroutine(FadingVolume(clipNew, waitDuration));
    }
    public IEnumerator FadingVolume(AudioClip clipNew, int waitDuration)
    {
        decreaseVolume = true;
        yield return new WaitForSeconds(waitDuration);
        source.clip = clipNew;
        source.Play();
        increaseVolume = true;

    }

    public void CameraZoom(float newSize, float speed)
    {
        targetSize = newSize;
        cameraSpeed = speed;
    }
    public void MoveCamera(Vector3 newPosition, float speed)
    {
        Debug.Log("This function was called.");
        isCutsceneActive = true;
        Vector3 targetPosition = main.transform.position + newPosition;
        StartCoroutine(MoveCameraCoroutine(targetPosition, speed));
    }

    private IEnumerator MoveCameraCoroutine(Vector3 targetPosition, float speed)
    {
        while (Vector3.Distance(main.transform.position, targetPosition) > 0.01f)
        {
            Debug.Log("Moving camera towards target position: " + targetPosition);
            main.transform.position = Vector3.MoveTowards(main.transform.position, targetPosition, speed * Time.deltaTime);
            yield return null;
        }
        main.transform.position = targetPosition;
    }
    public void MainMenu()
    {
        isPaused = !isPaused;
        Vector3 pos = UIManagerRPG.instance.settingsMenu.transform.position;
        pos.x = !isPaused ? 0f : UIManagerRPG.instance.settingsMenu.originalPosition.x;
        UIManagerRPG.instance.settingsMenu.transform.position = pos;
        UIManagerRPG.instance.settingsMenu.AssignNewWaypointsAndMoveObject(new Vector2[] { new Vector2(isPaused ? 400f : -400f, 0) }, 700f, false);
        Time.timeScale = Time.timeScale == 1 ? 0 : 1;
    }
    public void BeginBattle(GameObject enemy = null)
    {
        StartCoroutine(StartBattle());
    }
    public IEnumerator StartBattle()
    {
        isInBattle = true;
        movingAutonomously = false;
        playerpg.isMovable = false;
        Camera.main.GetComponent<AudioSource>().Pause();
        yield return new WaitForSecondsRealtime(0.5f);
        Time.timeScale = 0;
        // Play the animation even if the game is paused
        playerpg.anim.updateMode = AnimatorUpdateMode.UnscaledTime;
        playerpg.anim.SetTrigger("enemyEncounter");
        yield return new WaitForSecondsRealtime(1f);
        Camera.main.GetComponent<AudioSource>().clip = musicClips[2];
        Camera.main.GetComponent<AudioSource>().Play();
        yield return new WaitForSecondsRealtime(1f);
        CameraZoom(1f, 2f);
        yield return new WaitForSecondsRealtime(.5f);
        UIManager.instance.fadeableGeneralObjects[0].StartFading(3f, 10f);
        yield return new WaitForSecondsRealtime(3f);
        BattleTransition();
        CameraZoom(9f, 2f);
        yield return new WaitForSecondsRealtime(10f);
        UIManager.instance.fadeableGeneralObjects[0].StartFading(2f, 20f);
        yield return new WaitForSecondsRealtime(1.5f);
        battleAlliesPrefab[0].SetActive(true);
        battleAlliesPrefab[0].transform.localPosition = new Vector3(-429, -87, 0);
        enemiesInBattle[0].transform.localPosition = new Vector3(247, 200, 0);
        UIManagerRPG.instance.battleShortMenu.SetActive(true);
        PartyManager.instance.UpdateMoveButtons(PartyManager.instance.partyMembers[currentAllyIndex]);

        // Implement battle initiation logic here
    }
    public void BattleTransition()
    {
        mainMap.SetActive(false);
        playerpg.gameObject.SetActive(false);
        UIManagerRPG.instance.fadeableRPGObjects[0].Fader(false, UIManagerRPG.instance.battleImageBackgrounds[0]);
        CalculateRandomStatsofEnemy();
        enemiesInBattle[0].gameObject.transform.localPosition = new Vector3(0, 0, 0);
        Time.timeScale = 1;
        battleAlliesPrefab[0].GetComponent<battleStats>().health = playerHealth;
        battleAlliesPrefab[0].GetComponent<battleStats>().level = playerLevel;
        DialogueVault.instance.enemyName = enemiesInBattle[0].GetComponent<battleStats>().isFallenMuslim ? "Fallen Muslim" : "Generic Enemy";
        DialogueVault.instance.isFallenMuslim = enemiesInBattle[0].GetComponent<battleStats>().isFallenMuslim;

        DialogueManager.instance.StartDialogueTexts(DialogueVault.instance.dialogueForBattler[0], 0, 2, 0, null, true, 3);
    }
    public void CalculateRandomStatsofEnemy()
    {
        numberOfEnemies = Random.Range(1, 4);
        enemiesInBattle = new GameObject[numberOfEnemies];
        for (int i = 0; i < numberOfEnemies; i++)
        {
            
            bool isFallenMuslim = Random.Range(0, 5) == 0; // 20% chance
            bool isGirlorBoy = Random.Range(0, 1) == 0; // 50% chance
            GameObject enemy = Instantiate(battleEnemyPrefab, UIManagerRPG.instance.cutsceneParent.transform);
            enemy.GetComponent<battleStats>().health = Random.Range(20, 40) * playerLevel;
            enemy.GetComponent<battleStats>().attack = Random.Range(5, 15) * playerLevel;
            enemy.GetComponent<battleStats>().defense = Random.Range(5, 15) * playerLevel;
            enemy.GetComponent<battleStats>().level = playerLevel + Random.Range(-5, 5);
            enemy.GetComponent<battleStats>().isFallenMuslim = isFallenMuslim;
            enemy.GetComponent<battleStats>().isGirlorBoy = isGirlorBoy;

            enemy.transform.localPosition = new Vector3(900, 0, 0);
            enemiesInBattle[i] = enemy;
            if (i != 0)
            {
                enemy.SetActive(false);
            }
        }

    }
    public void CommenceBattle(int moveNumber)
    {
        StartCoroutine(commenceBattle(moveNumber));
    }
    public IEnumerator commenceBattle(int moveNumber)
    {
        UIManagerRPG.instance.battleMovesAnimator.SetTrigger("disperse");
        for (int i = 0; i <= 4; i++)
        {
            if (i == moveNumber)
            {
                Move moveToChange = PartyManager.instance.partyMembers[currentEnemyIndex].assignedMoves[i];
                switch (isPlayerTurn ? 0 : 1)
                {
                    case 0:
                        currentPlayerMove = moveToChange;
                        break;
                    case 1:
                        currentEnemyMove = moveToChange;
                        break;
                }

            }
        }
        yield return new WaitForSeconds(1f);
        DialogueVault.instance.AttackinBattle(isPlayerTurn ? currentPlayerMove.moveName : currentEnemyMove.moveName, isPlayerTurn ? currentPlayerMove.power : currentEnemyMove.power);
    }
    public void SpriteFlicker(Image sprite, int flickerCount = 5, float flickerDuration = 0.1f)
    {
        StartCoroutine(spriteFlicker(sprite, flickerCount, flickerDuration));
    }

    private IEnumerator spriteFlicker(Image sprite, int flickerCount = 5, float flickerDuration = 0.1f)
    {
        if (sprite != null)
        {
            Color originalColor = sprite.color;
            float flickerInterval = flickerDuration / flickerCount;

            while (flickerCount > 0)
            {
                sprite.enabled = !sprite.enabled;
                flickerCount--;
                yield return new WaitForSeconds(flickerInterval);
            }

            sprite.enabled = true; // Ensure the sprite is visible at the end
            sprite.color = originalColor; // Restore original color
        }
    }
}
