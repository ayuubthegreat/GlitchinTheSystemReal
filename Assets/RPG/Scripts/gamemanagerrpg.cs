using System.Collections;
using UnityEngine;
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
    public int maxNumberOfEnemies = 1;
    public int enemyHealth = 10;
    public int playerLevel = 1;
    public int experiencePoints = 0;
    public int xptoNextLevel = 20;
    public int coins = 0;
    public int enemyLevel = 1;
    public int originalPlayerHealth = 20;
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
    public BoxCollider2D npcWall;
    public int npcWallYOffset = -50;
    public NPC[] allNPCcharacters;
    [System.Serializable]
    public struct CutsceneAssembler
    {
        public RuntimeAnimatorController[] headAnims;
        public RuntimeAnimatorController[] bodyAnims;
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
            // movingAutonomously = false;
            mainMap.SetActive(false);
            playerHouse.SetActive(true);
            isCutsceneActive = false;
            // source.clip = musicClips[0];
            // source.Play();
        }

    }

    // Update is called once per frame
    void Update()
    {
        source.volume = GameManager.instance.musicVolume;
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (UIManagerRPG.instance.options.activeSelf || isInBattle) return;
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
        if (isInBattle)
        {

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
        StartCoroutine(StartBattle(enemy));
    }
    public IEnumerator StartBattle(GameObject enemy = null)
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
        yield return new WaitForSecondsRealtime(.5f);
        Camera.main.GetComponent<AudioSource>().clip = musicClips[2];
        Camera.main.GetComponent<AudioSource>().Play();
        yield return new WaitForSecondsRealtime(.5f);
        CameraZoom(1f, 2f);
        yield return new WaitForSecondsRealtime(.3f);
        UIManager.instance.fadeableGeneralObjects[0].StartFading(1.5f, 10f);
        yield return new WaitForSecondsRealtime(1.5f);
        Destroy(enemy);
        BattleTransition();
        CameraZoom(9f, 2f);
        yield return new WaitForSecondsRealtime(enemiesInBattle[0].GetComponent<battleStats>().isFallenMuslim ? 9f : 6f);
        UIManager.instance.fadeableGeneralObjects[0].StartFading(1.5f, 20f);
        yield return new WaitForSecondsRealtime(1.5f);
        
        battleAlliesPrefab[0].SetActive(true);
        battleAlliesPrefab[0].transform.localPosition = new Vector3(-429, -87, 0);
        enemiesInBattle[0].transform.localPosition = new Vector3(247, 200, 0);
        UIManagerRPG.instance.battleShortMenu.SetActive(true);
        PartyManager.instance.UpdateMoveButtons(PartyManager.instance.partyMembers[currentAllyIndex]);
        UIManagerRPG.instance.UpdateBattleUIStats(PartyManager.instance.partyMembers[currentAllyIndex].memberName, playerLevel, playerHealth, DialogueVault.instance.enemyName, enemyLevel, enemyHealth);

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
        UIManagerRPG.instance.playerHealthBar.healthSource = battleAlliesPrefab[0].GetComponent<battleStats>();
        UIManagerRPG.instance.playerHealthBar.originalHealth = playerHealth;
        battleAlliesPrefab[0].GetComponent<battleStats>().health = playerHealth;
        battleAlliesPrefab[0].GetComponent<battleStats>().level = playerLevel;
        DialogueVault.instance.enemyName = enemiesInBattle[0].GetComponent<battleStats>().isFallenMuslim ? "Fallen Muslim" : "Generic Enemy";
        DialogueVault.instance.isFallenMuslim = enemiesInBattle[0].GetComponent<battleStats>().isFallenMuslim;

        DialogueManager.instance.StartDialogueTexts(DialogueVault.instance.dialogueForBattler[0], 0, 2, 0, null, true, 3);
    }
    public void CalculateRandomStatsofEnemy()
    {
        numberOfEnemies = Random.Range(1, maxNumberOfEnemies);
        enemiesInBattle = new GameObject[numberOfEnemies];
        for (int i = 0; i < numberOfEnemies; i++)
        {
            bool isFallenMuslim = Random.Range(0, 5) == 0; // 20% chance
            bool isGirlorBoy = Random.Range(0, 1) == 0; // 50% chance
            GameObject enemy = Instantiate(battleEnemyPrefab, UIManagerRPG.instance.cutsceneParent.transform);
            if (i == 0)
            {
                UIManagerRPG.instance.enemyHealthBar.healthSource = enemy.GetComponent<battleStats>();
                UIManagerRPG.instance.enemyHealthBar.originalHealth = enemyHealth;
            }
            battleStats enemyStats = enemy.GetComponent<battleStats>();
            enemyStats.health = playerLevel * (isFallenMuslim ? Random.Range(15, 25) : Random.Range(8, 15));
            enemyStats.originalHealth = enemyStats.health;
            enemyStats.attack = 1;
            enemyStats.defense = 1;
            enemyStats.level = playerLevel + Random.Range(-1, 2);
            enemyStats.isFallenMuslim = isFallenMuslim;
            enemyStats.isGirlorBoy = isGirlorBoy;
            enemyStats.experiencePointsGained = isFallenMuslim ? (20 + Random.Range(5, 10)) * playerLevel : (10 + Random.Range(3, 8)) * playerLevel;
            enemyStats.coinsGained = isFallenMuslim ? (10 + Random.Range(3, 7)) * playerLevel : (5 + Random.Range(1, 5)) * playerLevel;

            enemy.transform.localPosition = new Vector3(900, 0, 0);
            enemiesInBattle[i] = enemy;
            if (i != 0)
            {
                enemy.SetActive(false);
            }
        }
        UpdateHealthStats();
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
                Move moveToChange = PartyManager.instance.partyMembers[currentAllyIndex].assignedMoves[i];
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
        AttackinBattle(isPlayerTurn ? currentPlayerMove.moveName : currentEnemyMove.moveName, isPlayerTurn ? currentPlayerMove.power : currentEnemyMove.power, isPlayerTurn ? currentPlayerMove.category == MoveCategory.Physical : currentEnemyMove.category == MoveCategory.Physical);
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

    public void AttackinBattle(string moveName = "Slash", int damageAmount = 10, bool isPhysical = true, bool increasesHealth = false, int stageChange = 0) => StartCoroutine(attackinBattle(moveName, damageAmount, isPhysical, increasesHealth, stageChange));
    public IEnumerator attackinBattle(string moveName = "Slash", int damageAmount = 10, bool isPhysical = true, bool increasesHealth = false, int stageChange = 0)
    {
        int currentDefenseStage = isPlayerTurn ? PartyManager.instance.partyMembers[currentAllyIndex].defense : enemiesInBattle[currentEnemyIndex].GetComponent<battleStats>().defense;
        int currentAttackStage = isPlayerTurn ? PartyManager.instance.partyMembers[currentAllyIndex].attack : enemiesInBattle[currentEnemyIndex].GetComponent<battleStats>().attack;
        int currentLevel = GameManagerRPG.instance.isPlayerTurn ? GameManagerRPG.instance.battleAlliesPrefab[currentAllyIndex].GetComponent<battleStats>().level : GameManagerRPG.instance.enemiesInBattle[currentEnemyIndex].GetComponent<battleStats>().level;
        damageAmount += currentLevel * 2 * currentAttackStage / currentDefenseStage;
        int currentHealth = GameManagerRPG.instance.isPlayerTurn ? GameManagerRPG.instance.enemiesInBattle[currentEnemyIndex].GetComponent<battleStats>().health - damageAmount : GameManagerRPG.instance.battleAlliesPrefab[currentAllyIndex].GetComponent<battleStats>().health - damageAmount;
        bool isDead = false;
        yield return new WaitForSeconds(1f);
        UIManagerRPG.instance.battleShortMenu.SetActive(false);
        Debug.Log("Attacking enemy with " + moveName + " for " + damageAmount + " damage!");
        DialogueVault.instance.dialogueForBattler[0][3].dialogueLine = GameManagerRPG.instance.isPlayerTurn ? "You use " + moveName + "!" : DialogueVault.instance.enemyName + " uses " + moveName + "!";
        DialogueManager.instance.StartDialogueTexts(DialogueVault.instance.dialogueForBattler[0], 3, 3, 0, null, true, 2);
        // Implement attack logic here
        yield return new WaitForSeconds(1f);

        if (isPhysical)
        {
            SpriteFlicker(isPlayerTurn ? enemiesInBattle[currentEnemyIndex].GetComponent<Image>() : battleAlliesPrefab[currentAllyIndex].GetComponent<Image>(), 10);
            while (currentHealth < (isPlayerTurn ? enemiesInBattle[currentEnemyIndex].GetComponent<battleStats>().health : battleAlliesPrefab[currentAllyIndex].GetComponent<battleStats>().health))
            {
                battleStats stats = isPlayerTurn ? enemiesInBattle[currentEnemyIndex].GetComponent<battleStats>() : battleAlliesPrefab[currentAllyIndex].GetComponent<battleStats>();
                if (stats.health <= 0)
                {
                    isDead = true;
                    break;
                }
                stats.health--;
                UpdateHealthStats();
                yield return new WaitForSeconds(.02f);
            }


        }
        else
        {

        }
        yield return new WaitForSeconds(.5f);
        if (enemiesInBattle[currentEnemyIndex].GetComponent<battleStats>().health <= 0 || battleAlliesPrefab[currentAllyIndex].GetComponent<battleStats>().health <= 0)
        {
            isDead = true;
        }

        if (isDead)
        {
            if (enemiesInBattle[currentEnemyIndex].GetComponent<battleStats>().health <= 0)
            {
                enemiesInBattle[currentEnemyIndex].SetActive(false);
                yield return new WaitForSeconds(2f);
                RewardPlayer();
                DialogueManager.instance.StartDialogueTexts(DialogueVault.instance.dialogueForBattler[0], 5, (experiencePoints >= xptoNextLevel) ? 9 : 8, 0, null, false, 0, SendNewEnemy);
                while (experiencePoints >= xptoNextLevel)
                {
                    LevelUpPlayer();
                }
                
            }
            else if (battleAlliesPrefab[currentAllyIndex].GetComponent<battleStats>().health <= 0)
            {
                DialogueManager.instance.StartDialogueTexts(DialogueVault.instance.dialogueForBattler[0], 4, 4, 0, null, true, 3);
            }

        }
        else
        {
            isPlayerTurn = !isPlayerTurn;
            switch (isPlayerTurn)
            {
                case true:
                    UIManagerRPG.instance.battleShortMenu.SetActive(true);
                    break;
                case false:
                    CommenceBattle(Random.Range(0, PartyManager.instance.partyMembers[currentAllyIndex].assignedMoves.Length));
                    break;
            }
        }



    }
    public void UpdateHealthStats()
    {
        UIManagerRPG.instance.UpdateBattleUIStats(PartyManager.instance.partyMembers[currentAllyIndex].memberName, battleAlliesPrefab[currentAllyIndex].GetComponent<battleStats>().level, battleAlliesPrefab[currentAllyIndex].GetComponent<battleStats>().health, DialogueVault.instance.enemyName, enemiesInBattle[currentEnemyIndex].GetComponent<battleStats>().level, enemiesInBattle[currentEnemyIndex].GetComponent<battleStats>().health);
        UIManagerRPG.instance.enemyHealthBar.healthSource = enemiesInBattle[currentEnemyIndex].GetComponent<battleStats>();
        UIManagerRPG.instance.playerHealthBar.healthSource = battleAlliesPrefab[currentAllyIndex].GetComponent<battleStats>();
        UIManagerRPG.instance.enemyHealthBar.originalHealth = enemiesInBattle[currentEnemyIndex].GetComponent<battleStats>().originalHealth;
        enemyHealth = enemiesInBattle[currentEnemyIndex].GetComponent<battleStats>().health;
        playerHealth = battleAlliesPrefab[currentAllyIndex].GetComponent<battleStats>().health;
    }
    public void ResetBattleStats()
    {
        playerpg.isMovable = true;
        mainMap.SetActive(true);
        playerpg.gameObject.SetActive(true);
        battleAlliesPrefab[0].SetActive(false);
        currentAllyIndex = 0;
        currentEnemyIndex = 0;
        isPlayerTurn = true;
        playerHealth = battleAlliesPrefab[0].GetComponent<battleStats>().health;
    }
    public void ControlNPCMovement(bool move, NPC[] exceptionNPCs = null)
    {
        allNPCcharacters = FindObjectsByType<NPC>(FindObjectsSortMode.None);
        foreach (NPC NPC in allNPCcharacters)
        {
            NPC.canMove = move;
            if (exceptionNPCs == null) continue;
            foreach (NPC npc in exceptionNPCs)
            {
                if (NPC == npc)
                {
                    NPC.canMove = true;
                }
            }

        }
    }
    public void RewardPlayer()
    {
        experiencePoints += enemiesInBattle[currentEnemyIndex].GetComponent<battleStats>().experiencePointsGained;
        coins += enemiesInBattle[currentEnemyIndex].GetComponent<battleStats>().coinsGained;
        DialogueVault.instance.dialogueForBattler[0][7].dialogueLine = "You gained " + enemiesInBattle[currentEnemyIndex].GetComponent<battleStats>().experiencePointsGained + " experience points!";
        DialogueVault.instance.dialogueForBattler[0][8].dialogueLine = "You gained " + enemiesInBattle[currentEnemyIndex].GetComponent<battleStats>().coinsGained + " coins!";
        Debug.Log("Gained " + enemiesInBattle[currentEnemyIndex].GetComponent<battleStats>().experiencePointsGained + " experience points and " + enemiesInBattle[currentEnemyIndex].GetComponent<battleStats>().coinsGained + " coins!");
    }
    public void LevelUpPlayer()
    {

        originalPlayerHealth += 5;
        playerHealth = originalPlayerHealth;
        experiencePoints -= xptoNextLevel;
        playerLevel++;
        xptoNextLevel = 20 * playerLevel;
        PartyManager.instance.partyMembers[0].level = playerLevel;
        PartyManager.instance.partyMembers[0].health = playerHealth;
        PartyManager.instance.partyMembers[0].attack += 2;
        PartyManager.instance.partyMembers[0].defense += 2;
        PartyManager.instance.partyMembers[0].speed += 1;
        battleAlliesPrefab[0].GetComponent<battleStats>().level = playerLevel;
        battleAlliesPrefab[0].GetComponent<battleStats>().health = playerHealth;
        UIManagerRPG.instance.playerHealthBar.originalHealth = originalPlayerHealth;
        UIManagerRPG.instance.playerHealthBar.health = playerHealth;
        DialogueVault.instance.dialogueForBattler[0][9].dialogueLine = "You leveled up to level " + (playerLevel + 1) + "!";
        Debug.Log("Leveled up to level " + playerLevel + "!");
        UpdateHealthStats();

    }
    public void SendNewEnemy()
    {
        if (!isInBattle || enemiesInBattle[currentEnemyIndex].GetComponent<battleStats>().health > 0) return;
        if (currentEnemyIndex >= enemiesInBattle.Length - 1)
        {
            UIManagerRPG.instance.EndBattle();
        }
        else
        {
            currentEnemyIndex++;
            enemiesInBattle[currentEnemyIndex].SetActive(true);
            UpdateHealthStats();
            enemiesInBattle[currentEnemyIndex].transform.localPosition = new Vector3(247, 200, 0);
            isPlayerTurn = true;
            UIManagerRPG.instance.battleShortMenu.SetActive(true);
        }
    }
    public void UpdateMovePower(PartyMember member)
    {
        for (int i = 0; i < member.assignedMoves.Length; i++)
        {
            Move moveToChange = member.assignedMoves[i];
            moveToChange.power *= member.level * 2 * member.attack;
        }
    }
}

