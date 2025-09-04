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
    public int personNumber;
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
        if (GameManager.instance.DialogueProgression != 0 && DialogueProcessor.instance.isConversationActive)
        {
            mouthAnim.SetBool("isTalking", canTalk);
            headAnim.SetInteger("DP", GameManager.instance.DialogueProgression);
        }
    }
    public void ChangeLocationPositionandSprite(Vector2 newPosition = default, AnimatorOverrideController newHeadAnimatorController = null, AnimatorController newBodyAnimatorController = null, float scaleSize = 1f, string characterName = "", Action desiredFunction = null)
    {
        rectTransform.anchoredPosition = newPosition;
        rectTransform.localScale = new Vector3(scaleSize, scaleSize, scaleSize);
        if (newHeadAnimatorController != null)
        {
            headAnim.runtimeAnimatorController = newHeadAnimatorController;
        }
        if (newBodyAnimatorController != null)
        {
            bodyAnim.runtimeAnimatorController = newBodyAnimatorController;
        }
        Debug.Log("Changed location and sprite of " + characterName);
        desiredFunction?.Invoke();

    }

}
