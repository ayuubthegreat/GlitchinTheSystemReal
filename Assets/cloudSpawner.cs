using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class cloudSpawner : MonoBehaviour
{
    public float yOffset = 30f;
    public float xLimit = 10f;
    public float randomYSpawn;
    public float spawnInterval = 2f;
    public bool shouldSpawnFillerClouds = true;
    public GameObject[] cloudPrefabs;
    public GameObject currentCloud;
    public Transform spawnArea;
    public BoxCollider2D spawnAreaCollider;
    public int playerXInput = 0; // Placeholder for player input, adjust as needed
    public bool spawnAreaActive = false;
    public bool endOfLevelClouds = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(SpawnClouds());
        spawnAreaCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        shouldSpawnFillerClouds = Vector2.Distance(transform.position, gameManagerPlatformer.instance.player.transform.position) > xLimit;
    }
    private IEnumerator SpawnClouds()
    {
        currentCloud = null;
        while (shouldSpawnFillerClouds)
        {
            PickCloud();

            randomYSpawn = Random.Range(0, yOffset);
            Vector3 spawnPosition = new Vector3(spawnArea.position.x, spawnArea.position.y + randomYSpawn, 0f);
            GameObject fillerCloud = Instantiate(currentCloud, spawnPosition, Quaternion.identity, transform);
            cloud cloudScript = fillerCloud.GetComponent<cloud>();
            cloudScript.shouldRespawn = false; // Disable respawn for filler clouds
            cloudScript.fillerCloud = true; // Mark as filler cloud
            cloudScript.canBeDestroyed = !endOfLevelClouds;
            cloudScript.xLimit = 15f;
            yield return new WaitForSeconds(spawnInterval);
        }
    }
    private void PickCloud()
    {
        int cloudPicker = Random.Range(0, cloudPrefabs.Length - 1);
        currentCloud = cloudPrefabs[cloudPicker];

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        player player = collision.gameObject.GetComponent<player>();
        if (player != null && (player.xInput == playerXInput || player.xInput == 0))
        {// If the player collides with the cloud, stop spawning filler clouds
            shouldSpawnFillerClouds = false;

        } else if (player != null && player.xInput == -playerXInput)
        {
            // If the player collides with the cloud but has a different input, do nothing
            shouldSpawnFillerClouds = true;
            Debug.Log("Player input does not match, continuing to spawn clouds.");
            return;
        }
    }
}
