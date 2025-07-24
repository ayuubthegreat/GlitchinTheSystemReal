using System.Collections;
using UnityEngine;

public class trunk : ComplexEnemy
{
    public float nextAttackTime;
    public float attackCooldown = 2f;
    public float attackRange = 5f;
    public bool canFlip = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();
        canMove = true;

        rb = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        bool canAttackAgain = Time.time > nextAttackTime + attackCooldown;
        bool ComeBack = (facingDir == -1 && transform.position.x < gameManagerPlatformer.instance.player.transform.position.x) ||
                        (facingDir == 1 && transform.position.x > gameManagerPlatformer.instance.player.transform.position.x);
        if (isWallDetected || !isGroundDetected || isEnemyDetected)
        {
            Flip();
        }
        if (ComeBack && canFlip)
        {
            Flip();
            StartCoroutine(CanFlipCooldown());
        }


        if (isPlayerDetected)
        {

            if (canAttackAgain && Vector2.Distance(transform.position, gameManagerPlatformer.instance.player.transform.position) <= attackRange)
            {
                Attack();
            }


        }

    }
    protected void Attack()
    {
        nextAttackTime = Time.time;
        StartCoroutine(CanMoveCooldown());
    }
    protected IEnumerator CanMoveCooldown()
    {
        canMove = false;
        anim.SetTrigger("attack");
        rb.linearVelocity = Vector2.zero;
        yield return new WaitForSeconds(1f);
        canMove = true;
    }
    protected IEnumerator CanFlipCooldown()
    {
        canFlip = false;
        rb.linearVelocity = Vector2.zero;
        yield return new WaitForSeconds(1f);
        canFlip = true;
    }
   
}
