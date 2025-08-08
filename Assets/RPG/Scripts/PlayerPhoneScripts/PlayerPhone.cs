using UnityEngine;

public class PlayerPhone : MonoBehaviour
{
    
    public Animator anim;
    public bool isMouth;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        bool canTalk = DialogueProcessor.instance.person1turn && DialogueManager.instance.isDialogueActive;
        if (GameManager.instance.DialogueProgression != 0 && DialogueProcessor.instance.isConversationActive)
        {
            if (isMouth)
            {
                anim.SetBool("isTalking", canTalk);
            }
            else
            {
                anim.SetInteger("DP", GameManager.instance.DialogueProgression);
            }
        }
    }

}
