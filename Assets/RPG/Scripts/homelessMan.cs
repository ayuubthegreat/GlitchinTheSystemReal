using UnityEngine;


public class HomelessMan : MonoBehaviour
{
    public Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<playerpg>() != null)
        {
            anim.SetTrigger("playerisHere");
        }

    }
    void OnTriggerStay2D(Collider2D collision)
    {
        playerpg playerpg = collision.gameObject.GetComponent<playerpg>();
       if (playerpg != null)
        {
            if (Input.GetKey(KeyCode.M))
            {
                DialogueProcessor.instance.npcDetector = GetComponent<BoxCollider2D>();
                DialogueProcessor.instance.isTalkingToHomelessMan = true;
                DialogueProcessor.instance.DialogueProgressionFunction();
                DialogueProcessor.instance.MovetoTarget(transform);
                
                
            }
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<playerpg>() != null)
        {
            anim.SetTrigger("playerLeft");
        }
    }
}