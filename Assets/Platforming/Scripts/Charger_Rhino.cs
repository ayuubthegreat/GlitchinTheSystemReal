using UnityEngine;

public class Charger_Rhino : ComplexEnemy
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Vector2 impact;
    
    public float toroTimer;
    public int toroDuration;
 
    public bool isBouncing = false;
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
        if (rb.linearVelocity.y == 0 && isBouncing) {
            
            isBouncing = false;
            Flip();
        }
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
       

    }
    private void WallBounce()
    {
        isBouncing = true;
        canMove = false;
        anim.SetBool("wallBounce", true);
        rb.linearVelocity = new Vector2(impact.x * -facingDir, impact.y);


    }
    public void EndTheCharge()
    {
        Debug.Log("This is working.");
        anim.SetBool("wallBounce", false);
    }
    protected override void Flip()
    {
        if (isBouncing) // Prevent flipping while bouncing
        {
            return;
        }
        transform.Rotate(0, 180, 0);
        facingDir = facingDir * -1;
    }
}
