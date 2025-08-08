using UnityEngine;

public class StoryProgressor : MonoBehaviour
{
    public NPC npc;
    public BoxCollider2D playerDetector;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerDetector = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        playerpg player = collision.gameObject.GetComponent<playerpg>();
        if (player != null)
        {
            DialogueProcessor.instance.ConversationManager(npc);
            playerDetector.enabled = false; // Disable the collider to prevent repeated triggers
        }
    }
}
