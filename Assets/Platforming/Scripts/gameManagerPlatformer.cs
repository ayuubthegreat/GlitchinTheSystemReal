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

    void Awake()
    {
        instance = this;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = FindFirstObjectByType<player>();
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

}
