using UnityEngine;

public class hammerhead : MonoBehaviour
{
    public plant_hammerbros plantHammerBrosScript;
    public SpriteRenderer hammerHeadSprite;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float speed = 5f;
    public float rotationSpeed = 200f;
    public int facingDir;
    private int index = 1;
    public Rigidbody2D rb;
    public Transform originalPosition;
    public bool canBeDestroyed = false;
    public bool canMove = true;
    public Vector3[] waypoints;
    public DamageTrigger damageTrigger;

    public void Start()
    {
        hammerHeadSprite = GetComponent<SpriteRenderer>();
        if (facingDir == 1)
        {
            hammerHeadSprite.flipX = true;
        }
        else
        {
            hammerHeadSprite.flipX = false;
        }
        damageTrigger = GetComponentInChildren<DamageTrigger>();
    }
    public void Update()
    {
        HandleMovement();
        if (index > 1)
        {
            canBeDestroyed = true;
        }
        if (plantHammerBrosScript.gameObject.activeSelf == false)
        {
            DropDown();
            
        }
    }
    private void HandleMovement()
    {
        if (!canMove || waypoints.Length == 0)
            return;


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
    private void DropDown()
    {
        canMove = false;
        rb.linearVelocity = new Vector2(0, -5f);
        rb.angularVelocity = 0f;
        rb.gravityScale = 1f;
        damageTrigger.gameObject.SetActive(false);
        Destroy(gameObject, 15f);
    }
    
}
