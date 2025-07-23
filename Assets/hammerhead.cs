using UnityEngine;

public class hammerhead : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float speed = 5f;
    public float rotationSpeed = 200f;
    public int facingDir = -1;
    private int index = 1;
    private Rigidbody2D rb;
    public Transform originalPosition;
    public bool canBeDestroyed = false;
    public Vector3[] waypoints;

    public void Awake()
    {
        if (facingDir == 0)
        {
            facingDir = -1; // Default to left if not set
        }
    
    
        rb = GetComponent<Rigidbody2D>();
        if (originalPosition == null)
        {
            originalPosition = transform;
        }
        waypoints = new Vector3[3];
        waypoints[0] = originalPosition.position;
        waypoints[1] = originalPosition.position + new Vector3((5 * facingDir), 0);
        waypoints[2] = waypoints[1] + new Vector3((2 * facingDir), -0.5f, 0);
    }
    public void Update()
    {
        HandleMovement();
        if (index > 1)
        {
            canBeDestroyed = true;
        }
    }
    private void HandleMovement()
    {


            Vector2 targetPosition = waypoints[index];
            Vector2 newPosition = Vector2.MoveTowards(rb.position, targetPosition, speed * Time.deltaTime);
            rb.MovePosition(newPosition);
            if (Vector2.Distance(rb.position, targetPosition) < 0.1f)
            {
                index++;
            if (index >= waypoints.Length)
            {
                index = 0;
            }
            }
        


    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        ComplexEnemy enemy = collision.GetComponent<ComplexEnemy>();
        if (enemy != null && canBeDestroyed)
        {
            Destroy(gameObject);
        }
       
    }
    
}
