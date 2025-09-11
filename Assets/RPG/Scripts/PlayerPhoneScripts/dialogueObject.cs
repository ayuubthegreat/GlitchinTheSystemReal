using System;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.UI;

public class dialogueObject : MonoBehaviour
{
    public RectTransform rectTransform;
    public Animator headAnim;
    public Animator mouthAnim;
    public Animator bodyAnim;
    public bool isMouth;
    public int personNumber = 1;
    public bool canTalk;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        canTalk = (personNumber == 1 && DialogueProcessor.instance.person1turn || personNumber == 2 && DialogueProcessor.instance.person2turn) && DialogueManager.instance.isDialogueActive;
        if (DialogueProcessor.instance.isConversationActive)
        {
            mouthAnim.SetBool("isTalking", canTalk);
            headAnim.SetInteger("DP", GameManager.instance.DialogueProgression);
        }
    }
    

}
