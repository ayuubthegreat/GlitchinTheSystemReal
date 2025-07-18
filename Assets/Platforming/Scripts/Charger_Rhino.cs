using UnityEngine;

public class Charger_Rhino : ComplexEnemy
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Vector2 impact;
    public float detectionRange;
    public float toroTimer;
    public int toroDuration;
    public bool isPlayerDetected;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        canMove = false;
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        toroTimer -= Time.deltaTime;
        if (!isGroundDetected || isEnemyDetected)
        {
            Flip();
        }
        if (isPlayerDetected && !isWallDetected && rb.linearVelocity.x == 0)
        {
            canMove = true;
            toroTimer = toroDuration;
        }
        if (toroTimer <= 0)
        {
            canMove = false;
            toroTimer = 0;
        }
        if (isWallDetected)
        {
            WallBounce();
        }
    }
    protected override void HandleCollision()
    {
        base.HandleCollision();
        isPlayerDetected = Physics2D.Raycast(transform.position, Vector2.right * facingDir, detectionRange, whatisPlayer);

    }
    private void WallBounce()
    {
        canMove = false;
        anim.SetBool("wallBounce", true);
        rb.linearVelocity = new Vector2(impact.x * -facingDir, impact.y);
        

    }
    public void EndTheCharge()
    {
        rb.linearVelocity = Vector2.zero;
        anim.SetBool("wallBounce", false);
        Invoke(nameof(Flip), .3f);
    }
}
