using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Standardchargerenemy : ComplexEnemy
{
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
        if (isWallDetected || !isGroundDetected || isEnemyDetected)
        {
            Flip();
        }
        if (isPlayerDetected)
        {
            canMove = true;
            toroTimer = toroDuration;
        }
        if (toroTimer <= 0)
        {
            canMove = false;
            toroTimer = 0;
        }
    }
    protected override void HandleCollision()
    {
        base.HandleCollision();
        isPlayerDetected = Physics2D.Raycast(transform.position, Vector2.right * facingDir, detectionRange, whatisPlayer);

    }
}
