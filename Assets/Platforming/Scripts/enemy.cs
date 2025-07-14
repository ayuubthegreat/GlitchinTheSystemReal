using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    public SpriteRenderer sr;
    public float moveSpeed = 3;
    public Transform[] waypoints;
    public Vector3[] waypointPositions;
    public int waypointCurrent = 1;
    public bool canMove;
    public float coolDown;
    public float beginningDelay;


    void Awake()
    {
        
    }
    void Start()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
        canMove = true;
        List<waypointScript> waypointScripts = new List<waypointScript>(GetComponentsInChildren<waypointScript>());
        if (waypointScripts.Count != waypointPositions.Length)
        {
            foreach (waypointScript waypoint in waypointScripts)
            {
                if (waypoint != null && waypoint.transform != null)
                {
                    waypoints = new Transform[waypointScripts.Count];
                    for (int i = 0; i < waypointScripts.Count; i++)
                    {
                        waypoints[i] = waypointScripts[i].transform;
                    }
                }
            }
        }
        waypointPositions = new Vector3[waypoints.Length];
        for (int i = 0; i < waypoints.Length; i++)
        {
            waypointPositions[i] = waypoints[i].position;
        }
        transform.position = waypointPositions[0];
        beginningDelay = Random.Range(0, 3);
        StartCoroutine(BeginEnemyMovement());
        coolDown = Random.Range(0, .5f);
        
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (waypoints == null || waypoints.Length == 0)
        {
            return;
        }
        GameManager.instance.player.landedonEnemy = true;
        Die();
        
    }
    void Update()
    {
        EnemyMovement();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Die()
    {
        StartCoroutine(DieTime());
    }
    public IEnumerator DieTime()
    {
        if (GameManager.instance != null && GameManager.instance.player != null)
        {
        GameManager.instance.player.landedonEnemy = false;
        }
        
        yield return new WaitForSeconds(0.2f);
        Destroy(gameObject);
    }
    public IEnumerator CooldownMan(float delay) {
        canMove = false;
        yield return new WaitForSeconds(delay);
        canMove = true;
        sr.flipX = !sr.flipX;
    }
    public IEnumerator BeginEnemyMovement()
    {
        canMove = false;
        yield return new WaitForSeconds(beginningDelay);
        canMove = true;

    }
    public void EnemyMovement()
    {
        if (!canMove)
        {
            return;
        }
        transform.position = Vector2.MoveTowards(transform.position, waypointPositions[waypointCurrent], moveSpeed * Time.deltaTime);
        if (Vector2.Distance(transform.position, waypointPositions[waypointCurrent]) < .1f)
    {

            waypointCurrent++;

            if (waypointCurrent < waypoints.Length)
            {
               StartCoroutine(CooldownMan(coolDown));
                return;
            }
            waypointCurrent = 0;
            StartCoroutine(CooldownMan(coolDown));


        }
    }
}
