using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;


public class plant_hammerbros : ComplexEnemy
{
    public float yVelocity;
    public float attackCooldown = 1f;
    private float nextAttackTime = 0f;
    public bool isHeadLaunched = false;
    public GameObject hammerHeadPrefab;
    public Transform headLaunchPoint;
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
        yVelocity = rb.linearVelocity.y;
        bool canAttack = Time.time > nextAttackTime + attackCooldown;
        if (isPlayerDetected && canAttack)
        {
            Attack();
        }
    }
    private void Attack()
    {
        nextAttackTime = Time.time;
        anim.SetTrigger("attack");
        canMove = false;
    }
    private void LaunchHead()
    {
        if (isHeadLaunched)
        {
            Debug.Log("Head already launched!");
            return;
        }
        Debug.Log("Launching head at player!");
        Instantiate(hammerHeadPrefab, headLaunchPoint.position, Quaternion.identity);
        StartCoroutine(HandleHeadLaunchBool());

    }
    private IEnumerator HandleHeadLaunchBool()
    {
        isHeadLaunched = true;
        yield return new WaitForSeconds(.1f);
        isHeadLaunched = false;
    }

}

    


