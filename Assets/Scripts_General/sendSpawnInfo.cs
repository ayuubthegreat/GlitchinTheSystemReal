using UnityEngine;

public class sendSpawnInfo : MonoBehaviour
{
    public bool RPGSpawn;
    public bool PlatformerSpawn;

    void Awake()
    {
        
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (RPGSpawn && GameManager.instance.startSpawnRPG == null)
        {
            GameManager.instance.startSpawnRPG = gameObject;
        }
        else if (PlatformerSpawn && GameManager.instance.startSpawnPlatforming == null)
        {
            GameManager.instance.startSpawnPlatforming = gameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
