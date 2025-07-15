using UnityEngine;

public class ComplexEnemy : MonoBehaviour
{
   [SerializeField] protected Animator anim;
    protected Rigidbody2D rb;
    [SerializeField] protected int moveSpeed = 10;
    protected int coolDownTime;
    [SerializeField] protected int facingDir = -1;
   [SerializeField] protected bool isGroundDetected;
    [SerializeField]protected bool isWallDetected;
    [SerializeField] protected Transform groundDetection;
   [SerializeField] protected float groundCheckDistance = 1.1f;
   [SerializeField] protected float wallCheckDistance = 1.1f;
    [SerializeField] protected bool canMove = true;
   [SerializeField] protected LayerMask whatisGround;
   [SerializeField] protected LayerMask whatisWall;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

    }

    // Update is called once per frame
   protected virtual void Update()
    {
        HandleMovement();
        anim.SetFloat("xVelocity", rb.linearVelocity.x);
        HandleCollision();
    }

    protected virtual void HandleCollision()
    {
        isGroundDetected = Physics2D.Raycast(groundDetection.position, Vector2.down, groundCheckDistance, whatisGround);
        isWallDetected = Physics2D.Raycast(groundDetection.position, Vector2.right * facingDir, wallCheckDistance, whatisWall);
    }
    protected virtual void HandleMovement()
    {
        if (canMove)
        {
            rb.linearVelocity = new Vector2(moveSpeed * facingDir, rb.linearVelocity.y);
        }
    }
    protected virtual void Flip()
    {
        facingDir = facingDir * -1;
        transform.Rotate(0, 180, 0);
    }
   
}
