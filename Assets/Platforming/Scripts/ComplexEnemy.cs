using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class ComplexEnemy : MonoBehaviour
{
    [SerializeField] protected Animator anim;
    protected Collider2D cd;
    protected Rigidbody2D rb;
    [SerializeField] protected SpriteRenderer sr;
    [SerializeField] protected int moveSpeed = 10;
    protected int coolDownTime;
    [SerializeField] protected int facingDir = -1;
    [SerializeField] protected float yInput;
    [SerializeField] protected float detectionRange = 20f;
    [SerializeField] protected bool isGroundDetected;
    [SerializeField] protected bool isWallDetected;
    [SerializeField] protected bool isEnemyDetected;
    [SerializeField] protected bool isPlayerDetected;
    [SerializeField] protected Transform groundDetection;
    [SerializeField] protected float groundCheckDistance = 1.1f;
    [SerializeField] protected float wallCheckDistance = 1.1f;
    [SerializeField] protected bool canMove = true;
    [SerializeField] protected LayerMask whatisGround;
    [SerializeField] protected LayerMask whatisEnemy;
    [SerializeField] protected LayerMask whatisPlayer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        if (sr.flipX == true && facingDir == -1)
        {
            sr.flipX = false;
            Flip();
        }

    }

    // Update is called once per frame
    protected virtual void Update()
    {
        HandleMovement();

        HandleCollision();
        anim.SetFloat("xVelocity", rb.linearVelocity.x);
    }

    protected virtual void HandleCollision()
    {
        isGroundDetected = Physics2D.Raycast(groundDetection.position, Vector2.down, groundCheckDistance, whatisGround);
        isWallDetected = Physics2D.Raycast(groundDetection.position, Vector2.right * facingDir, wallCheckDistance, whatisGround);
        isEnemyDetected = Physics2D.Raycast(groundDetection.position, Vector2.right * facingDir, wallCheckDistance, whatisEnemy);
         isPlayerDetected = Physics2D.Raycast(transform.position, Vector2.right * facingDir, detectionRange, whatisPlayer);

    }
    protected virtual void HandleMovement()
    {
        if (canMove)
        {
            rb.linearVelocity = new Vector2(moveSpeed * facingDir, rb.linearVelocity.y);
        }
        yInput = rb.linearVelocity.y;
    }
    protected virtual void Flip()
    {
        facingDir = facingDir * -1;
        transform.Rotate(0, 180, 0);
    }
    [ContextMenu("Flip")]
    protected virtual void FlipContext()
    {
        sr.flipX = !sr.flipX;
    }
    protected virtual void ComeBackHere()
    {
    if ((facingDir == -1 && gameManagerPlatformer.instance.player.transform.position.x > transform.position.x) || 
        (facingDir == 1 && gameManagerPlatformer.instance.player.transform.position.x < transform.position.x))
    {
        Flip();
    }
    


    }
   
}
