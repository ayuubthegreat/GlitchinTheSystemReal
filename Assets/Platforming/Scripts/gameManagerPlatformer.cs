using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class gameManagerPlatformer : MonoBehaviour
{
    public static gameManagerPlatformer instance;
    public new Camera camera;
    public player player;
    public GameObject playerPrefab;
    public CameraControllerRPG cameraController;
    public int playerLives;
    public int playerHealth;
    public GameObject startSpawnPlatforming;
    public Vector3 spawnObject;
    public bool startSpawnBool = true;
    [Header("Camera-related Variables")]
    public bool isTrampolining = false;
    public bool isCutscene = false;
    public bool canBeHit = true;
    public bool gameOver = false;
    public float originalCameraSize;
    public float targetCameraSize;
    public float cameraSpeed;
    public AudioSource source;
    public AudioSource soundEffectSource;
    public AudioClip coinSound;
    public AudioClip playerDieSound;
    public AudioClip playerHitSound;
    public AudioClip playerJumpSound;
    public AudioClip playerLandSound;
    public AudioClip extraLifeSound;
    public AudioClip playerDashSound;
    public AudioClip pbotdestroyedSound;
    public AudioClip playerWallSlideSound;
    public AudioClip musicClip;
    public AudioClip springSound;
    public int coinNumbers = 0;
    public int invincibilityPeriod = 2;
    public coins[] coins;
    public ComplexEnemy[] enemies;

    void Awake()
    {
        instance = this;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CheckObjectStates();
        spawnObject = startSpawnPlatforming.transform.position;

        coins = FindObjectsByType<coins>(FindObjectsSortMode.None);
        enemies = FindObjectsByType<ComplexEnemy>(FindObjectsSortMode.None);

        ReviveCoins();
        ReviveEnemy();

    }

    // Update is called once per frame
    void Update()
    {
        camera.orthographicSize = Mathf.Lerp(camera.orthographicSize, targetCameraSize, Time.deltaTime * cameraSpeed);
        FiftyCoins();
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
            gameOver = true;
        if (soundEffectSource != null)
            soundEffectSource.PlayOneShot(playerDieSound);
        UIManagerPlatformer.instance.SetUIElementsActive(false);

        yield return new WaitForSeconds(1);
        CheckObjectStates();

        source.time = 0f;
        GameObject newPlayer = Instantiate(playerPrefab, spawnObject, Quaternion.identity);
        player = newPlayer.GetComponent<player>();
        gameOver = false;

        ReviveCoins();
        ReviveEnemy();

    }
    public void RespawnPlayerInCheckpoint(Vector3 newSpawnPoint, int index)
    {
        startSpawnBool = false;
        spawnObject = newSpawnPoint;

    }
    public void CheckObjectStates()
    {
        camera = FindFirstObjectByType<Camera>();
        cameraController = camera.GetComponent<CameraControllerRPG>();
        soundEffectSource = GetComponent<AudioSource>();
        startSpawnPlatforming = GameObject.Find("startSpawnPlatforming");
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
    public void ReviveCoins()
    {
        coinNumbers = 0;
        foreach (coins coin in coins)
        {
            coin.gameObject.SetActive(true);
        }
    }
    public void ReviveEnemy()
    {
        foreach (ComplexEnemy enemy in enemies)
        {
            enemy.gameObject.SetActive(true);

        }
    }
    public void FiftyCoins()
    {
        if (coinNumbers == 50)
        {
            Debug.Log("You have collected 50 coins! Extra life granted.");
            Debug.Log("Audio Clip: " + extraLifeSound.name);
            StartCoroutine(RevertOriginalSoundClip(extraLifeSound));
            source.loop = false;
            source.pitch = 1f;
            playerLives++;
            coinNumbers = 0; // Reset coin count after granting life
        }
    }
    public IEnumerator RevertOriginalSoundClip(AudioClip clip)
    {
        float musicTime = source.time;
        AudioClip musicClip = source.clip; // Store the original music clip
        if (source != null)
        {
            source.clip = clip;
            source.pitch = 1f; // Reset pitch after playing
            source.Play();
        }
        yield return new WaitForSeconds(clip.length);
        source.clip = musicClip; // Revert to original music clip
        source.time = musicTime; // Restore the original time
        source.pitch = 1f; // Reset pitch to normal
        source.Play();

    }
    public void StartCooldownforHits()
    {
        if (canBeHit)
        {
            StartCoroutine(CooldownforHits());
        }
    }
    public IEnumerator CooldownforHits()
    {
        canBeHit = false;
        yield return new WaitForSeconds(invincibilityPeriod);
        canBeHit = true;
    }
    
}


