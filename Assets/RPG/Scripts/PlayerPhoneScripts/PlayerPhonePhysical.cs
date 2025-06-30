using UnityEngine;

public class PlayerPhonePhysical : MonoBehaviour
{


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (DialogueProcessor.instance.playerPhoneDialogue == null)
        {
            DialogueProcessor.instance.playerPhoneDialogue = GameObject.Find("AbdurahmanPhoneDialogue");
        }
        if (DialogueProcessor.instance.recieverPhoneDialogue2 == null)
        {
            DialogueProcessor.instance.recieverPhoneDialogue2 = GameObject.Find("YasirPhoneDialogue");
        }
        DialogueProcessor.instance.playerPhoneDialogue.SetActive(false);
        DialogueProcessor.instance.recieverPhoneDialogue2.SetActive(false);
        DialogueProcessor.instance.isPhoneActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (DialogueProcessor.instance.playerPhoneDialogue != null)
        {
            DialogueProcessor.instance.playerPhoneDialogue.SetActive(DialogueProcessor.instance.isPhoneActive);
        }
    }
    void OnTriggerStay2D(Collider2D collision)
    {
        
    }
}
