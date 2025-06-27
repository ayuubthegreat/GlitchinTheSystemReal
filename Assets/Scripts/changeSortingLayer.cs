using UnityEngine;

public class changeSortingLayer : MonoBehaviour
{
    public SpriteRenderer sprite;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sprite = GetComponentInParent<SpriteRenderer>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        playerpg playerpg = collision.GetComponent<playerpg>();
        player player = collision.GetComponent<player>();
        if (playerpg != null || player != null)
        {
            sprite.sortingLayerName = "houseBackground";
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        playerpg player = collision.gameObject.GetComponent<playerpg>();
        player playerReal = collision.GetComponent<player>();
        if (player != null || playerReal != null)
        {
            sprite.sortingLayerName = "house";
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }
    
}
