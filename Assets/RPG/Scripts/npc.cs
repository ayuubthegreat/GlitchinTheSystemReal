using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Animator), typeof(SpriteRenderer))]
public class NPC : MonoBehaviour
{
    public Animator npcAnimator;
    public DialogueVault.DialogueSet[] npcDialogueSets;
    public string[] npcLines;
    public Vector2[] npcWaypoints;
    public Vector2 originalTransformPosition;
    public float npcSpeed = 2.0f;
    public int waypointIndex = 1;
    public Collider2D playerDetector;
    public Rigidbody2D npcRigidbody;
    public Vector2 playerPosition;
    public float xVelocity;
    public float yVelocity;
    public int facingDir = 0; // 0: Default, 1: Right, 2: Left, 3: Up, 4: Down
    public int directionForFlippingMovement = 0; // 0: Up or Down, 1: Right, -1: Left;
    public SpriteRenderer sr;
    public bool movingAutonomously;
    public float waitTimeBetweenMoves = 2.0f;
    public int yRandomOffsetLimit = 20;
    public int xRandomOffsetLimit = 20;
    public bool isInDialogue = false;
    public bool canMove = true;

    private void Awake()
    {

    }
    protected virtual void OnEnable()
    {
        npcAnimator = GetComponent<Animator>();
        npcRigidbody = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        originalTransformPosition = transform.position;
        if (movingAutonomously && npcWaypoints.Length > 0)
        {
            RecalibrateWaypoints();
            StartMovingNPC(waitTimeBetweenMoves, npcSpeed, npcWaypoints, null, true);
        }
        npcDialogueSets = new DialogueVault.DialogueSet[npcLines.Length];
        for (int i = 0; i < npcLines.Length; i++)
        {
            npcDialogueSets[i] = new DialogueVault.DialogueSet
            {
                characterName = gameObject.name,
                dialogueLine = npcLines[i],
            };
        }
    }
    protected virtual void Update()
    {
        playerPosition = GameManagerRPG.instance.playerpg.transform.position;
        npcAnimator.SetFloat("xVelocity", xVelocity);
        npcAnimator.SetFloat("yVelocity", yVelocity);
        npcAnimator.SetInteger("facingDir", facingDir);

    }
    public void StartMovingNPC(float duration, float speed, Vector2[] waypoints, Action desiredFunction = null, bool loop = false)
    {
        if (waypoints.Length == 0) return;
        if (GameManagerRPG.instance == null)
        {
            Debug.LogWarning("GameManagerRPG instance is null. Cannot start moving NPC.");
            return;
        }
        GameManagerRPG.instance.playerpg.isMovable = false; // Disable player movement
        GameManagerRPG.instance.playerpg.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero; // Reset player velocity
        npcRigidbody.linearVelocity = Vector2.zero; // Reset velocity before starting movement
        movingAutonomously = loop;
        waypointIndex = 0; // Reset to the first waypoint
        StartCoroutine(MoveNPC(duration, speed, waypoints, desiredFunction, loop));
    }
    public IEnumerator MoveNPC(float duration, float speed, Vector2[] waypoints, System.Action desiredFunction = null, bool loop = false)
    {
        while (waypointIndex < waypoints.Length)
        {
            if (npcRigidbody == null) yield break; // Exit if Rigidbody2D is not assigned
            Vector2 targetPosition = new Vector2(transform.position.x + waypoints[waypointIndex].x, transform.position.y + waypoints[waypointIndex].y);
            Flip(targetPosition);
            while (Vector2.Distance(npcRigidbody.position, targetPosition) > 0.1f)
            {
                if (isInDialogue || !canMove)
                {
                    npcRigidbody.linearVelocity = Vector2.zero; // Stop the NPC
                    xVelocity = 0;
                    yVelocity = 0;
                    yield return null;
                    continue;
                }
                xVelocity = (targetPosition.x - npcRigidbody.position.x) > 0 ? 1 : (targetPosition.x - npcRigidbody.position.x) < 0 ? -1 : 0;
                yVelocity = (targetPosition.y - npcRigidbody.position.y) > 0 ? 1 : (targetPosition.y - npcRigidbody.position.y) < 0 ? -1 : 0;
                npcRigidbody.MovePosition(Vector2.MoveTowards(npcRigidbody.position, targetPosition, speed * Time.deltaTime));
                yield return null;
            }
            npcRigidbody.linearVelocity = Vector2.zero; // Stop the NPC
            xVelocity = 0;
            yVelocity = 0;
            if (movingAutonomously) duration = Random.Range(waitTimeBetweenMoves, waitTimeBetweenMoves + 2f);
            yield return new WaitForSeconds(duration);
            waypointIndex++;
            if (waypointIndex == waypoints.Length && loop)
            {
                RecalibrateWaypoints();
                waypoints = npcWaypoints;
                waypointIndex = 0;
                yield return null;
            }
        }
        if (loop)
        {

            waypointIndex = 0;
            yield return null;
        }
        else
        {
            // Once all waypoints are reached, reset the index
            npcRigidbody.linearVelocity = Vector2.zero; // Stop the NPC
            xVelocity = 0;
            yVelocity = 0;
            waypointIndex = 0;
            desiredFunction?.Invoke();
        }


        yield return null;


    }
    public void FacePlayer()
    {
        Debug.Log("This function was called.");
        int direction = Mathf.Abs(playerPosition.x - transform.position.x) > Mathf.Abs(playerPosition.y - transform.position.y) ? 0 : 1; // 0 for Vertical Turning, 1 for Horizontal Turning
        switch (direction)
        {
            case 0:
                Flip(playerPosition);
                if (playerPosition.x > transform.position.x)
                {
                    facingDir = 1; // Right
                    GameManagerRPG.instance.playerpg.startingPose = 2;
                }
                else if (playerPosition.x < transform.position.x)
                {
                    facingDir = 2; // Left
                    GameManagerRPG.instance.playerpg.startingPose = 1;
                }
                break;
            case 1:
                if (playerPosition.y > transform.position.y)
                {
                    facingDir = 3; // Up
                    GameManagerRPG.instance.playerpg.startingPose = 4;
                }
                else if (playerPosition.y < transform.position.y)
                {
                    facingDir = 4; // Down
                    GameManagerRPG.instance.playerpg.startingPose = 3;
                }
                break;
        }
        
        
        npcAnimator.SetInteger("facingDir", facingDir);
    }
    public void Flip(Vector2 targetPosition)
    {
        if (targetPosition.x > transform.position.x)
        {
            directionForFlippingMovement = 1; // Right
            sr.flipX = true;
        }
        else if (targetPosition.x < transform.position.x)
        {
            directionForFlippingMovement = -1; // Left
            sr.flipX = false;
        }
    }
    public void OnTriggerExit2D(Collider2D other)
    {
        changeSortingLayer csl = other.GetComponent<changeSortingLayer>();
        if (csl != null)
        {
            sr.sortingLayerName = csl.originalSortingLayer;
        }
    }
    public void RecalibrateWaypoints()
    {
        Debug.Log("Recalibrating waypoints...");
        int newLength = Random.Range(2, 10);
        Vector2[] newWaypoints = new Vector2[newLength];
        for (int i = 0; i < newLength - 1; i++)
        {
            int calibration = Random.Range(0, 2);
            switch (calibration)
            {
                case 0:
                    int yOffset = Random.Range(0, yRandomOffsetLimit);
                    newWaypoints[i] = new Vector2(0, yOffset);
                    break;
                case 1:
                    int xOffset = Random.Range(0, xRandomOffsetLimit);
                    newWaypoints[i] = new Vector2(xOffset, 0);
                    break;
            }
        }
        npcWaypoints = newWaypoints;
        Debug.Log($"Waypoints recalibrated. New length: {npcWaypoints.Length}");
    }
    public void OnTriggerStay2D(Collider2D other)
    {
        playerpg player = other.GetComponent<playerpg>();
        if (player != null)
        {
            if (Input.GetKeyDown(KeyCode.N))
            {
                DialogueManager.instance.StartDialogueTexts(npcDialogueSets, 0, npcDialogueSets.Length - 1, 0f, this);
            }
        }
    }

}