using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class gameManagerPlatformer : MonoBehaviour
{
    public static gameManagerPlatformer instance;
    public Camera camera;
    public player player;
    public GameObject playerPrefab;
    public CameraControllerRPG cameraController;
    public int playerLives;
    public int playerHealth;
    public GameObject startSpawnPlatforming;
    public Vector3 spawnObject;
    [Header("Camera-related Variables")]
    public float originalCameraSize;
    public float targetCameraSize;
    public float cameraSpeed;
    public AudioSource source;

    void Awake()
    {
        instance = this;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spawnObject = startSpawnPlatforming.transform.position;
        CheckObjectStates();
    }

    // Update is called once per frame
    void Update()
    {
        camera.orthographicSize = Mathf.Lerp(camera.orthographicSize, targetCameraSize, Time.deltaTime * cameraSpeed);
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
    public void RespawnPlayer() => StartCoroutine(DieandRespawner());

    public IEnumerator DieandRespawner()
    {
        playerLives--;

        if (player != null)
            player.Die();
        UIManagerPlatformer.instance.SetUIElementsActive(false);

        yield return new WaitForSeconds(1);
        startSpawnPlatforming = GameObject.Find("spawnPointPlatforming");
        source.time = 0f;
        GameObject newPlayer = Instantiate(playerPrefab, spawnObject, Quaternion.identity);
        player = newPlayer.GetComponent<player>();
        CheckObjectStates();

    }
    public void RespawnPlayerInCheckpoint(Vector3 newSpawnPoint, int index)
    {
        GameManager.instance.startSpawnBoolPlatforming = false;
        spawnObject = newSpawnPoint;

    }
    public void CheckObjectStates()
    {
        camera = FindFirstObjectByType<Camera>();
        cameraController = camera.GetComponent<CameraControllerRPG>();
    }
     public void StartCutscene(int cutsceneNum, int seconds, float moveSpeed) => StartCoroutine(StartingCutscene(cutsceneNum, seconds, moveSpeed));
    public IEnumerator StartingCutscene(int cutsceneNum, int seconds, float moveSpeed)
    {
        switch (cutsceneNum)
        {
            case 1:
                player.isMovable = false;
                camera.orthographicSize = 1.5f;
                targetCameraSize = 1.5f;
                yield return new WaitForSeconds(seconds);
                targetCameraSize = 5f;
                cameraSpeed = moveSpeed;
                yield return new WaitForSeconds(1);
                UIManagerPlatformer.instance.SetUIElementsActive(true);
                player.isMovable = true;
                break;
            case 2:
                break;
        }

    }

}
