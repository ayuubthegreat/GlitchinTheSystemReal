using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class door : MonoBehaviour
{
    [SerializeField] private string scene;
    private string tagName;
    private float xInput;
    private float yInput;
    public GameObject teleportationPoint;
    public GameObject mainMap;
    public GameObject houseMap;
    public string newLocationName;
    public int yInputNum;
    public int xInputNum;
    public int facingDir;
    public bool isTouchingPlayer;
    public SceneManager sm;
    public Animator anim;
    public int seconds2Wait;
    public bool canNotBeASpawnPoint;
    public AudioSource audioSource;
    public AudioClip clipNew;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anim = GetComponent<Animator>();
        audioSource = FindFirstObjectByType<Camera>().GetComponent<AudioSource>();
        if (mainMap == null)
        {
            mainMap = GameObject.Find("mainMap");
        }
    }

    // Update is called once per frame
    void Update()
    {
        xInput = Input.GetAxisRaw("Horizontal");
        yInput = Input.GetAxisRaw("Vertical");
        anim.SetBool("isHere", isTouchingPlayer);
        
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        isTouchingPlayer = xInput == xInputNum && yInput == yInputNum;
        playerpg playerpg = collision.gameObject.GetComponent<playerpg>();
        if (playerpg != null)
        {
            Debug.Log("This is working.");
            if (xInput == xInputNum && yInput == yInputNum)
            {
                playerpg.isMovable = false;
                
                StartCoroutine(LoadandRespawnPlayer());
            }
            
            


        }
    }
    public IEnumerator LoadandRespawnPlayer()
    {
        GameManager.instance.iswalkingdoor = GetComponentInChildren<doorSpawner>() ? true : false;
        GameManager.instance.startSpawnBool = false;
        yield return new WaitForSeconds(seconds2Wait);
        TeleportationPoint();
        GameManagerRPG.instance.playerpg.isMovable = true;

    }
    public void TeleportationPoint()
    {
        if (GameManager.instance.iswalkingdoor)
        {
            mainMap.SetActive(false);
            houseMap.SetActive(true);
            GameManagerRPG.instance.playerpg.transform.position = teleportationPoint.transform.position;
            audioSource.clip = clipNew;
            audioSource.Play();
            UIManager.instance.location = newLocationName;
            UIManager.instance.canTransition = true;
        }
        else
        {
            
            
            houseMap.SetActive(false);
            mainMap.SetActive(true);
            if (GameManagerRPG.instance.doorSpawn == null)
            {
                GameManagerRPG.instance.doorSpawn = GameObject.Find("startoutsideplayerdoor");
            }
            if (GameManagerRPG.instance.spawnObject == Vector3.zero)
            {
                GameManagerRPG.instance.spawnObject = GameManagerRPG.instance.doorSpawn.transform.position;
            }
            GameManagerRPG.instance.playerpg.transform.position = GameManagerRPG.instance.spawnObject;
            audioSource.clip = GameManagerRPG.instance.cameraController.clipOld;
            audioSource.Play();
            UIManager.instance.location = newLocationName;
            StopAllCoroutines();
            UIManager.instance.StartChangeTransitionBools();

        }
        GameManager.instance.iswalkingdoor = false;

        
        
        
    }
}
