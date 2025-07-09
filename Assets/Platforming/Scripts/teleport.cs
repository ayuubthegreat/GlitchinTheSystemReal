using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class teleport : MonoBehaviour
{
    [SerializeField] private string scene;
    [SerializeField] private float PlayerCheckDistance;
    [SerializeField] private LayerMask whatisPlayer;
    [SerializeField] private int facingDir = 1;
    private float xInput;
    private float yInput;
    public int xInputNum;
    public int yInputNum;
    public bool isTouchingPlayer;
    public Vector3 spawnDistance;
    public int seconds;
    public SceneManager sm;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        xInput = Input.GetAxisRaw("Horizontal");
        yInput = Input.GetAxisRaw("Vertical");


    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        isTouchingPlayer = xInput == xInputNum && yInput == yInputNum;
        playerpg playerpg = collision.gameObject.GetComponent<playerpg>();
        if (playerpg != null)
        {
            Debug.Log("This is working.");
            if (xInput == xInputNum && yInput == yInputNum)
            {
                playerpg.isMovable = false;
                GameManager.instance.phoneBoothSpawn = transform.position + spawnDistance;
                GameManager.instance.LoadNewSceneReal(seconds, scene);
                
            }
            
            


        }
    }
}
