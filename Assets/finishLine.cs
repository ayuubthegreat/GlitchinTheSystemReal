using UnityEngine;

public class finishLine : MonoBehaviour
{
    public player playerScript;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerScript = FindObjectOfType<player>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    void OnTriggerEnter2D(Collider2D collision)
    {
        player player = collision.gameObject.GetComponent<player>();
        if (player != null)
        {
            player.EndGame(transform);
        }
    }
    
}
