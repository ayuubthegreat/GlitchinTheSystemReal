using UnityEngine;

public class doorbarrierforgamestart : MonoBehaviour
{
    public BoxCollider2D bd;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bd = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (DialogueManager.instance.DialogueProgression < 3)
        {
            bd.enabled = true;

        }
        else
        {
            bd.enabled = false;
            Debug.Log("Barrier is disabled, game can start.");

        }
    }
}
