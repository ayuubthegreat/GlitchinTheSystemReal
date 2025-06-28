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
        GameManager.instance.playerpg.isMovable = true;

    }
    public void TeleportationPoint()
    {
        if (GameManager.instance.iswalkingdoor)
        {
            UIManager.instance.ScreenControls();
            GameManager.instance.DefiningGameObjectsandScripts();
            mainMap.SetActive(false);
            houseMap.SetActive(true);
            GameManager.instance.playerpg.transform.position = teleportationPoint.transform.position;
            audioSource.clip = clipNew;
            audioSource.Play();
            UIManager.instance.location = newLocationName;
            UIManager.instance.locationAnnouncerBool = true;
        }
        else
        {
            UIManager.instance.ScreenControls();
            GameManager.instance.DefiningGameObjectsandScripts();
            houseMap.SetActive(false);
            mainMap.SetActive(true);
            if (GameManager.instance.doorSpawn == null)
            {
                GameManager.instance.doorSpawn = GameObject.Find("startoutsideplayerdoor");
            }
            if (GameManager.instance.outsideDoorSpawnObject == Vector3.zero)
            {
                GameManager.instance.outsideDoorSpawnObject = GameManager.instance.doorSpawn.transform.position;
            }
            GameManager.instance.playerpg.transform.position = GameManager.instance.outsideDoorSpawnObject;
            audioSource.clip = GameManager.instance.cameraControllerRPG.clipOld;
            audioSource.Play();
            UIManager.instance.location = newLocationName;
            UIManager.instance.locationAnnouncerBool = true;

        }
        GameManager.instance.iswalkingdoor = false;

        
        
        
    }
}
