using UnityEngine;

public class PlayerPhone : MonoBehaviour
{
    
    public Animator anim;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (DialogueManager.instance.DialogueProgression != 0 && DialogueProcessor.instance.isPhoneActive)
        {
            anim.SetInteger("DP", DialogueManager.instance.DialogueProgression);
            anim.SetBool("player1talking", DialogueProcessor.instance.person1turn);
            anim.SetBool("isThinking", DialogueProcessor.instance.isThinking);
            anim.SetBool("isSurprised", DialogueProcessor.instance.isSurprised);
        }
        else
        {
            anim.SetInteger("DP", 0);
            anim.SetBool("player1talking", false);

        }
    }

}
