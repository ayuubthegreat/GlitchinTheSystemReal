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
    public bool canBeDestroyed = true; // Whether the cloud can be destroyed or not
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
        rb.linearVelocity = new Vector2(cloudSpeed, 0f);
        if (fillerCloud)
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


    }
    private IEnumerator returnToInitialPosition()
    {
        yield return new WaitForSeconds(.5f);
        Destroy(gameObject);

    }
    
}
