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
            sprite.sortingLayerName = "playeranim";
        }

        if (!canBeaSpawnPoint)
        {
            return;
        }
        
        GameManager.instance.RespawnPlayerInCheckpoint(transform.position, 1);
    }
    public void sortingLayerAdjust()
    {
        sprite.sortingLayerName = "backgroundtop";
        sprite.sortingOrder = 3;
    }
}
