using System;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class fallingPlatforms : MonoBehaviour
{
    public BoxCollider2D[] boxcolliders;
    public Rigidbody2D rb;
    public Vector3[] positions;
    public Vector3 originalRBPosition;
    public float speed = .75f;
    public float travelDistance;
    public int positionindex;
    public int floatDelays;
    public bool canMove;
    public float randomDelay;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        boxcolliders = GetComponents<BoxCollider2D>();
        originalRBPosition = rb.position;
    }
    void Start()
    {
        randomDelay = Random.Range(0f, 1f); 
        SetUpWayPoints();
       

    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        player player = collision.gameObject.GetComponent<player>();
        if (player != null)
        {
            Invoke(nameof(SwitchOffPlatform), floatDelays);
        }
    }
    void Update()
    {
        Invoke(nameof(HandleAnimation), randomDelay);
    }
    private void SetUpWayPoints()
    {
        positions = new Vector3[2];
        float yOffset = travelDistance / 2;
        positions[0] = rb.position + new Vector2(0, yOffset);
        positions[1] = rb.position + new Vector2(0, -yOffset);
    }
    private void HandleAnimation()
    {

        if (!canMove)
        {
            return;
        }
        rb.position = Vector3.MoveTowards(rb.position, positions[positionindex], speed);
        if (Vector3.Distance(rb.position, positions[positionindex]) < .1f)
        {
            positionindex++;
            if (positionindex >= positions.Length)
            {
                positionindex = 0;
            }
        }
    }
    private void SwitchOffPlatform()
    {
        
        foreach (BoxCollider2D boxes in boxcolliders)
        {
            boxes.enabled = false;
        }
        canMove = false;
        StartCoroutine(deathtoYou());
        rb.bodyType = RigidbodyType2D.Dynamic;
        
        rb.gravityScale = 4f;
        
        rb.linearDamping = .5f;
    
        
    }
    private IEnumerator deathtoYou() {
        yield return new WaitForSeconds(2);
        GameObject newObject = Instantiate(GameManager.instance.fallingPlatforms, originalRBPosition, quaternion.identity);


        Destroy(gameObject);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
}
