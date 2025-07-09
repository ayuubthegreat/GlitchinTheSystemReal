using UnityEngine;

public class GameManagerRPG : MonoBehaviour
{
    public static GameManagerRPG instance;
    public CameraControllerRPG cameraController;
    public playerpg playerpg;
    public GameObject startSpawnRPG;
    public Vector3 spawnObject;
    public GameObject mainMap;
    public GameObject playerHouse;
    public GameObject doorSpawn;
    public bool isDonewithPlatforming;

    void Awake()
    {
        instance = this;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerpg = FindFirstObjectByType<playerpg>();
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
            mainMap.SetActive(false);
            playerHouse.SetActive(true);
        }

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void RespawnPlayerInCheckpoint(Vector3 newSpawnPoint, int index)
    {
        GameManager.instance.startSpawnBool = false;
        spawnObject = newSpawnPoint;
    }
}
