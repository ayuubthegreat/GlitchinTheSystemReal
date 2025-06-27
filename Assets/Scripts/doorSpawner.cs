using UnityEngine;

public class doorSpawner : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnTriggerEnter2D(Collider2D collision)
    {
        GameManager.instance.RespawnPlayerInCheckpoint(transform.position, 2);
        DontDestroyOnLoad(gameObject);
    }
}
