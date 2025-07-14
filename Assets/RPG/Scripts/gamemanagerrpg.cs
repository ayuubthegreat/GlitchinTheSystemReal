using System.Collections;
using UnityEngine;

public class GameManagerRPG : MonoBehaviour
{
    public static GameManagerRPG instance;
    public CameraControllerRPG cameraController;
    public AudioSource source;
    public AudioClip[] audioClips;
    public playerpg playerpg;
    public GameObject startSpawnRPG;
    public Vector3 spawnObject;
    public GameObject mainMap;
    public GameObject playerHouse;
    public GameObject doorSpawn;
    public bool isDonewithPlatforming;
    public bool decreaseVolume;
    public bool increaseVolume;
    public float audioSourceVolume = 1;
    public float fadeSpeed = 0.5f;
    public float moveSpeed = 5f;
    public bool iswalkingdoor = false;
    public bool movingAutonomously = false;


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
            source.clip = audioClips[0];
            source.Play();
        }

    }

    // Update is called once per frame
    void Update()
    {
        source.volume = audioSourceVolume;
        if (decreaseVolume)
        {
            GradualVolumeDecrease();
        }
        else if (increaseVolume)
        {
            GradualVolumeIncrease();
        }
        if (movingAutonomously)
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
        audioSourceVolume = Mathf.MoveTowards(audioSourceVolume, 0f, fadeSpeed * Time.deltaTime);
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
        audioSourceVolume = Mathf.MoveTowards(audioSourceVolume, 1f, fadeSpeed * Time.deltaTime);
        if (audioSourceVolume >= 1)
        {
            audioSourceVolume = 1;
            increaseVolume = false;
            return;
        }
    }
    public void StartFadingVolume(AudioClip clipNew, int waitDuration)
    {
        StopAllCoroutines();
        decreaseVolume = false;
        audioSourceVolume = 1;
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

    public void StartMovingAutonomously(Vector2 target)
    {
        movingAutonomously = true;
        MovingAutonomously(target);
    }
    public void MovingAutonomously(Vector2 targetPosition)
    {
        if (!movingAutonomously)
        {
            return;
        }
        playerpg.isMovable = false;
        Vector2.MoveTowards(playerpg.transform.position, targetPosition, moveSpeed * Time.deltaTime);
        if (Vector2.Distance(playerpg.transform.position, targetPosition) < 0.01f)
        {
            movingAutonomously = false;
            playerpg.isMovable = true;
        }
    }
}
