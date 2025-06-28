using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerpg : MonoBehaviour
{
    public Rigidbody2D rb;
    public SceneManager sm;
    public float xInput;
    public float yInput;
    public bool isMovable = true;
    public Animator anim;
    public bool isTurningLeft;
    public bool isFacingForwards;
    public bool isFacingBehind;
    public bool facingLeft;
    public int startingPose;

    
    [SerializeField] private float moveSpeed;
    [SerializeField] private float ymoveSpeed;
    [SerializeField] private float wallCheckDistance;
    [SerializeField] private LayerMask whatisGround;
    [SerializeField] public int facingDir = 1;
    private bool isTouchingWall;

void Awake() {
if (GameManager.instance != null) {
 GameManager.instance.NewRPGPlayeronTheBlock(playerpg);
}

} 
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        isMovable = true;
        if (GameManager.instance.isDonewithPlatforming)
        {
            transform.position = GameManager.instance.phoneBoothSpawn;
        }
        else
        {
           transform.position = GameManager.instance.startSpawnRPG.transform.position; 
        }
        
        

        if (startingPose == 3)
        {
            transform.Rotate(0, 180, 0);
        }
        

    }

    public void HandleMovement()
    {
        if (isMovable)
        {
            rb.linearVelocity = new Vector2(xInput * moveSpeed, yInput * ymoveSpeed);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!isMovable)
        {
            rb.linearVelocity = Vector2.zero;
            xInput = 0;
            yInput = 0;
            return;
        }
        xInput = Input.GetAxisRaw("Horizontal");
        yInput = Input.GetAxisRaw("Vertical");
        HandleMovement();
        if (xInput != 0 || yInput != 0)
        {
            startingPose = 0;
        }

        

        anim.SetInteger("PoseNum", startingPose);
        anim.SetFloat("xInput", xInput);
        anim.SetFloat("yInput", yInput);

        isFacingBehind = (yInput < 0) && isMovable;
        isTurningLeft = (xInput != 0) && isMovable;
        isFacingForwards = (yInput > 0) && isMovable;
        anim.SetBool("Right", isTurningLeft);
        anim.SetBool("Front", isFacingForwards);
        anim.SetBool("Bottom", isFacingBehind);

        if (facingLeft && xInput < 0 || !facingLeft && xInput > 0)
        {
            facingDir = facingDir * -1;
            transform.Rotate(0, 180, 0);
            facingLeft = !facingLeft;
        }
        if (isFacingForwards || isFacingBehind && xInput != 0)
        {
            transform.Rotate(0, 0, 0);
        }

        
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, new Vector2(transform.position.x + (wallCheckDistance * facingDir), transform.position.y) );
    }
}
