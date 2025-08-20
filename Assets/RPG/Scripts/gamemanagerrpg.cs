using System.Collections;
using UnityEngine;

public class GameManagerRPG : MonoBehaviour
{
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
    public bool isPaused = false;


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
            main.orthographicSize = Mathf.Lerp(main.orthographicSize, targetSize, Time.deltaTime * cameraSpeed);
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
}
