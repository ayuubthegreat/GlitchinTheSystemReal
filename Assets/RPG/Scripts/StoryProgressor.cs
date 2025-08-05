using UnityEngine;

public class StoryProgressor : MonoBehaviour
{

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

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
            DialogueProcessor.instance.DialogueProgressionFunction();
        }
    }
}
