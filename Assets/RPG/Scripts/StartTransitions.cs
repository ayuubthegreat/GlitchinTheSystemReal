using UnityEngine;

public class StartTransitions : MonoBehaviour
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
        anim.SetBool("canRPGTextMove", UIManager.instance.startTransitions[0]);
        anim.SetBool("canPersonNameTextMove", UIManager.instance.startTransitions[1]);
    }
    public void ChangeStartTransitionsBool(int initVal) => UIManager.instance.ChangeStartTransitionsBool(initVal);
    public void ChangeIsTalkingBool(int initVal) => DialogueManager.instance.isDialogueActive = initVal == 0;
}
