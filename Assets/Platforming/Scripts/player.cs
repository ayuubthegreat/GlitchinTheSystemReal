using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class player : MonoBehaviour
{
    #region Values & Variables
    [Header("Main Objects")]
    public Rigidbody2D rb;

    public Animator anim;

    public string scene;
    [Header("Lives and Health")]
    public int playerHealth = 3;
    [Header("VFX's")]
    public GameObject deathVFX;
    [Header("Inputs")]
    private float xInput;
    private float yInput;
    [Header("Conditions")]
    private bool isGrounded;
    private bool isAirbone;
    private bool coinCollect = true;
    private bool isTouchingWall;
    public bool isMovable = false;
    private bool isDead;
    private bool facingRight = true;
    public bool landedonEnemy = false;
    public bool crouched = false;
    [Header("Speeds and Forces")]
    public float moveSpeed;
    [SerializeField] private float jumpForce = 10;
    [Header("Buffer and Coyote Jump")]
    public float bufferjumpPressed;
    public float bufferJumpWindow;
    public float coyoteJumpWindow = .5f;
    public float coyoteJumpActivated = -1;
    [Header("Wall Jump")]
    [SerializeField] private float wallJumpDuration = .6f;
    [SerializeField] private Vector2 wallJumpForce;
    private bool isWallMoving = false;
    public bool canWallSlide;
    [Header("Double Jump")]
    public bool canDoubleJump;
    [SerializeField] private float doubleJumpForce = 20;
    [Header("Coins")]

    public int coinNumbers;

    [Header("Check Distances")]

    [SerializeField] private float groundCheckDistance;
    [SerializeField] private float wallCheckDistance;
    [SerializeField] private float deadZoneCheckDistance;
    [Header("Knockback")]
    public float knockbackDuration = 1;
    public Vector2 knockbackPower;
    public bool isKnocked;
    public bool canBeKnocked;
    [SerializeField] private float coinCheckDistance;
    [Header("Dash")]
    public float dashDuration = 0.5f;
    public Vector2 dashPower;
    public bool canDash = true;
    public bool isDashing;
    [Header("Layer Masks")]
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private LayerMask whatIsDeadZone;
    [SerializeField] private LayerMask whatisCoins;
    [Header("Other Values")]
    public float TargetTime = 0.01f;
    private int facingDir = 1;
    #endregion


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();

        
        
    }
    void Start()
    {
        if (GameManager.instance.player == null && GameManager.instance.playerpg == null)
        {
            GameManager.instance.player = gameObject.GetComponent<player>();
        } 
        if (GameManager.instance.startSpawnBoolPlatforming)
        {
            transform.position = GameManager.instance.startSpawnPlatforming.transform.position;

        }
        else
        {
            transform.position = GameManager.instance.spawnObject;
        }
       
    }
    private void HandleMovement()
    {
        xInput = Input.GetAxisRaw("Horizontal");
        yInput = Input.GetAxisRaw("Vertical");
        if (isTouchingWall)
        {
            return;
        }
        if (isWallMoving)
        {
            return;
        }
        isMovable = true;
        if (isMovable && !crouched)
        {
            rb.linearVelocity = new Vector2(xInput * moveSpeed, rb.linearVelocity.y);
        }
        if (yInput < 0 && isGrounded)
        {
            crouched = true;
        }
        else
        {
            crouched = false;
        }

    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            Knockback(0);
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            Dash(0);
        }
        UpdateAirbornStatus();
        if (isKnocked || isDashing)
        {
            return;
        }
        HandleMovement();

        WallSlide();
        HandleFlip();
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, whatIsGround);
        isTouchingWall = Physics2D.Raycast(transform.position, Vector2.right * facingDir, wallCheckDistance, whatIsGround);
        isDead = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, whatIsDeadZone);
        canWallSlide = isTouchingWall && rb.linearVelocity.y < 0;

        bool coyoteJumpAvailable = Time.time < coyoteJumpActivated + coyoteJumpWindow;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded || coyoteJumpAvailable)
            {
                if (coyoteJumpAvailable)
                {
                    Debug.Log("I'm using coyote!");
                }
                Jump();
            }
            else if (isTouchingWall && !isGrounded)
            {
                WallJump();

            }
            else if (canDoubleJump)
            {
                DoubleJump();
                canDoubleJump = false;
            }

            CancelCoyoteJump();
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            Flip();
        }


        anim.SetFloat("xVelocity", rb.linearVelocity.x);
        anim.SetFloat("yVelocity", rb.linearVelocity.y);
        anim.SetBool("isGrounded", isGrounded);
        anim.SetBool("isTouchingWall", isTouchingWall);
        anim.SetBool("landonenemy", landedonEnemy);
        anim.SetBool("crouched", crouched);


    }
    #region Functions that are not Update
    private void requestBufferJump()
    {
        if (isAirbone)
            bufferjumpPressed = Time.time;
    }
    private void AttemptBufferJump()
    {
        if (Time.time < bufferjumpPressed + bufferJumpWindow)
        {
            bufferjumpPressed = 0;
            Jump();
        }
    }
    private void ActivateCoyoteJump()
    {
        coyoteJumpActivated = Time.time;
    }
    private void CancelCoyoteJump()
    {
        coyoteJumpActivated = Time.time - 1;
    }


    public void Knockback(float currentenemyXPosition)
    {
        float knockbackDirection = 1;
        if (transform.position.x < currentenemyXPosition)
        {
            knockbackDirection = -1;
        }
        if (isKnocked)
        {
            return;
        }
        StartCoroutine(KnockbackandDashRoutine(0));
        anim.SetTrigger("knockback");
        rb.linearVelocity = new Vector2(knockbackPower.x * knockbackDirection, knockbackPower.y);
    }
    public void Dash(float currentenemyXPosition)
    {
        if (isDashing)
        {
            return;
        }
        StartCoroutine(KnockbackandDashRoutine(1));
        rb.linearVelocity = new Vector2(dashPower.x * facingDir, 0f);
    }

    private void WallJump()
    {

        rb.linearVelocity = new Vector2(wallJumpForce.x * -facingDir, wallJumpForce.y);
        Flip();
        StopAllCoroutines();
        StartCoroutine(WallJumpRoutine());
    }
    private IEnumerator WallJumpRoutine()
    {
        isWallMoving = true;
        yield return new WaitForSeconds(wallJumpDuration);
        isWallMoving = false;
    }
    private IEnumerator KnockbackandDashRoutine(int value)
    {
        if (isKnocked || isDashing)
        {
            yield break;
        }
        if (value == 0)
        {
            canBeKnocked = false;
            isKnocked = true;
        }
        else
        {
            canDash = false;
            isDashing = true;
        }

        yield return new WaitForSeconds(value == 0 ? knockbackDuration : dashDuration);
        if (value == 0)
        {
            canBeKnocked = true;
            isKnocked = false;
        }
        else
        {
            canDash = true;
            isDashing = false;
        }
    }

    private void Jump()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
    }
    private void DoubleJump()
    {
        isWallMoving = false;
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, doubleJumpForce);
    }
    private void WallSlide()
    {
        if (!canWallSlide)
        {
            return;
        }
        float yModifier = yInput < 0 ? .15f : .3f;
        
        
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * yModifier);
        
    }
    private void UpdateAirbornStatus()
    {
        if (isGrounded && isAirbone)
        {
            isAirbone = false;
            isWallMoving = false;
            canDoubleJump = true;
            isMovable = true;
            AttemptBufferJump();
        }
        if (!isGrounded && !isAirbone)
        {
            isAirbone = true;
            if (rb.linearVelocity.y < 0)
            {
                Debug.Log("Coyote Jump activated!");
                ActivateCoyoteJump();
            }

        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, new Vector2(transform.position.x, transform.position.y - groundCheckDistance));
        Gizmos.DrawLine(transform.position, new Vector2(transform.position.x + (wallCheckDistance * facingDir), transform.position.y));
    }
    private void HandleFlip()
    {
        if (xInput < 0 && facingRight || xInput > 0 && !facingRight)
        {
            Flip();
        }
    }
    public void Push(Vector2 direction, float duration)
    {
        StartCoroutine(PushCoroutine(direction, duration));
    }
    public IEnumerator PushCoroutine(Vector2 direction, float duration)
    {
        isMovable = false;
        rb.linearVelocity = Vector2.zero;
        rb.AddForce(direction, ForceMode2D.Impulse);
        yield return new WaitForSeconds(duration);
        isMovable = true;
    }

    private void Flip()
    {
        facingDir = facingDir * -1;
        transform.Rotate(0, 180, 0);
        facingRight = !facingRight;
    }
    public void Die()
    {
        GameObject newDeathVFX = Instantiate(deathVFX, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
    #endregion

} 
