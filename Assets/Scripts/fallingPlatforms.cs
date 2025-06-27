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
    public Vector3 originalTransformPosition;
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
        originalTransformPosition = transform.position;
    }
    void Start()
    {
        randomDelay = Random.Range(0, 1); 
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
        Invoke(nameof(handleAnimation), randomDelay);
    }
    private void SetUpWayPoints()
    {
        positions = new Vector3[2];
        float yOffset = travelDistance / 2;
        positions[0] = transform.position + new Vector3(0, yOffset, 0);
        positions[1] = transform.position + new Vector3(0, -yOffset, 0);
    }
    private void handleAnimation()
    {

        if (!canMove)
        {
            return;
        }
        transform.position = Vector3.MoveTowards(transform.position, positions[positionindex], speed);
        if (Vector3.Distance(transform.position, positions[positionindex]) < .1f)
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
        GameObject newObject = Instantiate(GameManager.instance.fallingPlatforms, originalTransformPosition, quaternion.identity);
        
        
        Destroy(gameObject);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
}
