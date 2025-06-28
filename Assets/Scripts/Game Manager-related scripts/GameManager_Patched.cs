
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
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

    public bool iswalkingdoor;
    public bool canNotBeASpawnPoint;
    public bool startSpawnBool;

    [Header("Player Lives")]
    public int playerLives = 3;

    [Header("Misc")]
    public bool FruitsRandom;
    public Dead dead;
    public Canvas canvas;

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
        if (startSpawnBool && !isDonewithPlatforming && startSpawnRPG != null && dialogueManager.DialogueProgression == 0)
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

    public void RespawnPlayer() => StartCoroutine(DieandRespawner());

    public IEnumerator DieandRespawner()
    {
        playerLives--;

        if (player != null)
            player.Die();

        yield return new WaitForSeconds(1);

        if (UIManager.instance != null)
        {
            UIManager.instance.coinsScreen?.SetActive(false);
            UIManager.instance.livesScreen?.SetActive(false);
            UIManager.instance.Fade(5);
            UIManager.instance.SetIsDeadandIsAliveBool(true);
            UIManager.instance.SetStartBool(0);
        }

        yield return new WaitForSeconds(1);

        GameObject newPlayer = Instantiate(playerPrefab, spawnObject, Quaternion.identity);
        player = newPlayer.GetComponent<player>();
        UIManager.instance.SetIsDeadandIsAliveBool(false);
        

        startSpawnPlatforming = GameObject.Find("spawnPointPlatforming");

        if (UIManager.instance != null)
        {
            UIManager.instance.coinsScreen?.SetActive(true);
            UIManager.instance.livesScreen?.SetActive(true);
        }
    }

public void NewPlayeronTheBlock(player newPlayerScript) {
if (player == null) {
player = newPlayerScript; 
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

    public void warningScreenandTeleport(float duration)
    {
        if (playerLives == 0)
        {
            if (UIManager.instance != null)
                StartCoroutine(UIManager.instance.TimetoDie(duration));
        }
        else
        {
            RespawnPlayer();
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
}
