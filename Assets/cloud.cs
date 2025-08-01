using System.Collections;

using UnityEngine;

public class cloud : MonoBehaviour
{
    private Rigidbody2D rb;
    public float cloudSpeed = .3f;
    public Vector2 initialPosition;
    public bool shouldRespawn = true;
    public bool canMove = false;
    public bool fillerCloud = false;
    public float xLimit = 10f;
    public float destroyTime = 5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody2D component not found on the cloud object.");
        }
        initialPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        canMove = Vector2.Distance(transform.position, gameManagerPlatformer.instance.player.transform.position) <= xLimit || fillerCloud;
        if (fillerCloud && Vector2.Distance(transform.position, gameManagerPlatformer.instance.player.transform.position) > xLimit)
        {
            // If the cloud is a filler cloud and the player is far away, destroy it after a certain time
            if (destroyTime > 0)
            {
                destroyTime -= Time.deltaTime;
            }
            else
            {
                Destroy(gameObject);
            }
        }
        else
        {
            destroyTime = 5f; // Reset destroy time if the player is close
        }
        
        if (gameManagerPlatformer.instance.gameOver)
        {
            // Stop the cloud movement if the game is over
            rb.linearVelocity = Vector2.zero;
            StartCoroutine(returnToInitialPosition());
            Debug.Log("Game Over, stopping cloud movement.");
            return;
        }
        else
        {
            if (canMove)
            {
                rb.linearVelocity = new Vector2(cloudSpeed, 0f);
            }

        }


    }
    private IEnumerator returnToInitialPosition()
    {
        yield return new WaitForSeconds(.5f);
        if (shouldRespawn)
        {
            transform.position = initialPosition;
            rb.linearVelocity = Vector2.zero;
        }
        else
        {
            Destroy(gameObject);
        }

    }
    
}
