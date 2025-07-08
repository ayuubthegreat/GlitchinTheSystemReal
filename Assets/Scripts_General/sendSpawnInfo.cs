using UnityEngine;

public class sendSpawnInfo : MonoBehaviour
{
    public bool RPGSpawn;
    public bool PlatformerSpawn;

    void Awake()
    {
        if (RPGSpawn)
        {
            GameManager.instance.startSpawnRPG = gameObject;
        }
        else if (PlatformerSpawn)
        {
            GameManager.instance.startSpawnPlatforming = gameObject;
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
