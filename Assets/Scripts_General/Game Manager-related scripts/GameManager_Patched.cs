
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public DialogueManager dialogueManager;
    public DialogueVault dialogueVault;

    [Header("Camera(s)")]
    public CameraControllerRPG cameraControllerRPG;
    public new Camera camera;

    [Header("Prefabs")]
    public GameObject arrows;
    public GameObject fallingPlatforms;
    [SerializeField] private GameObject playerPrefab;

    [Header("Scripts")]
    public player player;
    public playerpg playerpg;

    [Header("Spawn-Related Material")]
    public GameObject startSpawnPlatforming;
    public Vector3 spawnObject;
    public bool startSpawnBoolPlatforming;

    [Header("RPG Spawn Points")]
    public GameObject startSpawnRPG;
    public Vector3 outsideDoorSpawnObject;
    public GameObject doorSpawn;
    public Vector3 phoneBoothSpawn;
    public bool isDonewithPlatforming;
    [Header("RPG Maps")]
    public GameObject mainMap;
    public GameObject playerHouse;

    public bool iswalkingdoor;
    public bool canNotBeASpawnPoint;
    public bool startSpawnBool;

    [Header("Player Lives")]
    public int playerLives = 3;

    [Header("Misc")]
    public bool FruitsRandom;
    public Dead dead;
    public Canvas canvas;
    [Header("Game States")]
    public int platformerTimes;
    public int RPGTimes;
    [Header("Dialogue Progression")]
    public int DialogueProgression = 0;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        canvas = FindFirstObjectByType<Canvas>();
        isDonewithPlatforming = false;
        startSpawnBoolPlatforming = true;

        DefiningGameObjectsandScripts();

        if (UIManager.instance != null)
        {
            UIManager.instance.ScreenControls();
        }
    }

    void Start()
    {
        if (startSpawnBool && !isDonewithPlatforming && startSpawnRPG != null && DialogueProgression == 0)
        {
            outsideDoorSpawnObject = startSpawnRPG.transform.position;

        }
        else if (isDonewithPlatforming && startSpawnRPG != null)
        {
            phoneBoothSpawn = startSpawnRPG.transform.position;
            startSpawnBool = false;
            isDonewithPlatforming = false;
        }
        


        iswalkingdoor = false;
    }

    public bool FruitsAreRandom() => FruitsRandom;

    

    public void NewPlayeronTheBlock(player newPlayerScript)
    {
        if (player == null)
        {
            player = newPlayerScript;
        }
    }
    public void NewRPGPlayeronTheBlock(playerpg newPlayerScript)
    {
        if (playerpg == null)
        {
            playerpg = newPlayerScript;
        }
    }

    public void RespawnPlayerInCheckpoint(Vector3 newSpawnPoint, int index)
    {
        startSpawnBool = false;

        if (index == 1)
            spawnObject = newSpawnPoint;
        else if (index == 2)
            outsideDoorSpawnObject = newSpawnPoint;
        else
            Debug.Log("Invalid checkpoint index");
    }

    public void DefiningGameObjectsandScripts()
    {

        playerpg = FindFirstObjectByType<playerpg>();
        if (playerpg == null)
        {
            player = FindFirstObjectByType<player>();
        }
        else
        {
            playerpg = FindFirstObjectByType<playerpg>();
            if (mainMap == null) mainMap = GameObject.Find("mainMap");
            if (playerHouse == null) playerHouse = GameObject.Find("playerHouseMap");

        }

        startSpawnRPG = GameObject.Find("spawnPointRPG");

        camera = FindFirstObjectByType<Camera>();
        if (camera != null)
            cameraControllerRPG = camera.GetComponent<CameraControllerRPG>();

        startSpawnPlatforming = GameObject.Find("spawnPointPlatforming");
        dialogueManager = FindFirstObjectByType<DialogueManager>();
        dialogueVault = FindFirstObjectByType<DialogueVault>();
        if (dialogueManager != null && dialogueVault != null)
        {
            dialogueManager.dialogueVault = dialogueVault;
        }
    }

    

    public void CreateNewObjectReal(GameObject go, Transform position, float delay = 0)
    {
        StartCoroutine(CreateNewObject(go, position, delay));
    }

    public IEnumerator CreateNewObject(GameObject go, Transform position, float delay = 0)
    {
        Vector3 target = position.position;
        yield return new WaitForSeconds(delay);

        Instantiate(go, target, Quaternion.identity);
    }

    public void LoadNewSceneReal(int seconds, string scene)
    {
        StartCoroutine(LoadNewScene(seconds, scene));
    }

    public IEnumerator LoadNewScene(int seconds, string scene)

    {
        yield return new WaitForSeconds(seconds);
        SceneManager.LoadScene(scene);
    }
    public void StartCutscene(int cutsceneNum, int seconds, float moveSpeed) => StartCoroutine(StartingCutscene(cutsceneNum, seconds, moveSpeed));
    public IEnumerator StartingCutscene(int cutsceneNum, int seconds, float moveSpeed)
    {
        switch (cutsceneNum)
        {
            case 1:
                player.isMovable = false;
                camera.orthographicSize = 1.5f;
                gameManagerPlatformer.instance.targetCameraSize = 1.5f;
                yield return new WaitForSeconds(seconds);
                gameManagerPlatformer.instance.targetCameraSize = 5f;
                gameManagerPlatformer.instance.cameraSpeed = moveSpeed;
                yield return new WaitForSeconds(2);
                UIManagerPlatformer.instance.SetUIElementsActive(true);
                player.isMovable = true;
                break;
            case 2:
                break;
        }
        
    }
}
