using System;
using System.Collections;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public static NPC instance;
    public Animator npcAnimator;
    public Vector2[] npcWaypoints;
    public float npcSpeed = 2.0f;
    public int waypointIndex = 1;
    public Collider2D npcDetector;
    public Rigidbody2D npcRigidbody;
    public Vector2 playerPosition;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void Start()
    {
        npcAnimator = GetComponent<Animator>();
        npcRigidbody = GetComponent<Rigidbody2D>();
    }
    public void Update()
    {
        playerPosition = GameManagerRPG.instance.playerpg.transform.position;
    }
    public void StartMovingNPC(int duration, float speed, Vector2[] waypoints, Action desiredFunction = null)
    {
        if (waypoints.Length == 0) return;
        GameManagerRPG.instance.playerpg.isMovable = false; // Disable player movement
        GameManagerRPG.instance.playerpg.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero; // Reset player velocity
        npcRigidbody.linearVelocity = Vector2.zero; // Reset velocity before starting movement
        waypointIndex = 0; // Reset to the first waypoint
        StartCoroutine(MoveNPC(duration, speed, waypoints, desiredFunction));
    }
    public IEnumerator MoveNPC(int duration, float speed, Vector2[] waypoints, System.Action desiredFunction = null)
    {
        while (waypointIndex < waypoints.Length)
        {
            if (npcRigidbody == null) yield break; // Exit if Rigidbody2D is not assigned
            Vector2 targetPosition = new Vector2(transform.position.x + waypoints[waypointIndex].x, transform.position.y + waypoints[waypointIndex].y);
            while (Vector2.Distance(npcRigidbody.position, targetPosition) > 0.1f)
            {
                npcRigidbody.MovePosition(Vector2.MoveTowards(npcRigidbody.position, targetPosition, speed * Time.deltaTime));

                yield return null;
            }
            npcRigidbody.linearVelocity = Vector2.zero; // Stop the NPC
            yield return new WaitForSeconds(duration);
            waypointIndex++;
        }
        // Once all waypoints are reached, reset the index
        npcRigidbody.linearVelocity = Vector2.zero; // Stop the NPC
        waypointIndex = 0;
        desiredFunction?.Invoke();
        

        yield return null;


    }
    public void OnWaypointReached()
    {
        
        
    }
   
}