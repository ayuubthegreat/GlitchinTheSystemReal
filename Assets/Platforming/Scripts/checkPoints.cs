using UnityEngine;

public class checkPoints : MonoBehaviour
{
    public SpriteRenderer sprite;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private Animator anim;
    public bool active;
    public bool canBeaSpawnPoint;
    public bool lightcheck;

    void Start()
    {
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (active)
            return;
        player player = collision.GetComponent<player>();
        if (player != null)
        {
            ActivateCheckPoint();
        }
    }

    void Update()
    {

    }

    public void ActivateCheckPoint()
    {
        GameManager.instance.startSpawnBoolPlatforming = false;
        active = true;
        anim.SetBool("active", active);
        if (lightcheck)
        {
            sprite.sortingLayerName = "darklight";
        }

        if (!canBeaSpawnPoint)
        {
            return;
        }
        
        gameManagerPlatformer.instance.RespawnPlayerInCheckpoint(transform.position, 1);
    }
    public void sortingLayerAdjust()
    {
        if (!lightcheck)
        {
            sprite.sortingLayerName = "door";
        }
        else
        {
            sprite.sortingLayerName = "lights/gradients";
        }
        
        sprite.sortingOrder = 3;
    }
}
