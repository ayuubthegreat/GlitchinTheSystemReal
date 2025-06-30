using UnityEngine;
using System;
using System.Collections;

public class DialogueProcessor : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameManager gameManager;
    public DialogueVault dialogueVault;
    public static DialogueProcessor instance;
    public GameObject playerPhoneDialogue;
    public GameObject recieverPhoneDialogue2;
    public int PhoneRingingSeconds;
    public bool isConversationActive = false;
    public bool person1turn = false;
    public bool person2turn = false;
    public bool mouthMovement1 = false;
    public bool mouthMovement2 = false;
    public bool twopeopletalking = false;
    public bool isPhoneActive = false;
    public bool isThinking = false;
    public bool isSurprised = false;

    void Awake()
    {
        gameManager = GetComponent<GameManager>();
        dialogueVault = GetComponent<DialogueVault>();
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

    }
    void Start()
    {


        if (gameManager == null)
        {
            Debug.LogError("GameManager instance is not found in the scene.");
            return;
        }

        if (DialogueManager.instance == null)
        {
            Debug.LogError("DialogueManager instance is not set.");
        }
        if (GameManager.instance.playerpg == null)
        {
            return;
        }
        if (DialogueManager.instance.DialogueProgression == 0)
        {
            GameManager.instance.iswalkingdoor = false;
            
            GameManager.instance.outsideDoorSpawnObject = GameManager.instance.doorSpawn.transform.position;

        }
    }
    public void Update()
    {

       

    }
    public void DialogueProgressionFunction()
    {
        bool conversationEnded =  DialogueManager.instance.DialogueProgression == 3 && isPhoneActive;
        bool conversationBegan = DialogueManager.instance.DialogueProgression == 2 && isPhoneActive;
        if (DialogueManager.instance.DialogueProgression == 1)
        {
            StartCoroutine(DialogueProgression1());

        }
        else if (conversationBegan)
        {
            isPhoneActive = true;
            int secondsToWait = UnityEngine.Random.Range(5, 10);


            StartCoroutine(PhoneRinging(secondsToWait)); // Start the conversation with Abdurahman

        }
        else if (conversationEnded)
        {
            isPhoneActive = false;
            recieverPhoneDialogue2SetActive();
        }

    }
    public IEnumerator PhoneRinging(int seconds)
    {
        yield return new WaitForSeconds(seconds);
        isConversationActive = true;
        recieverPhoneDialogue2SetActive();
        
        DialogueManager.instance.StartRPGTextBox(1, 0, dialogueVault.dialogueSets[1].Length, dialogueVault.dialogueSets[1]);
    }
    public IEnumerator DialogueProgression1()
    {
        int secondstoWait = UnityEngine.Random.Range(2, 5);
        Debug.Log(secondstoWait + " seconds to wait before starting the dialogue.");
        if (dialogueVault == null)
        {
            dialogueVault = GetComponent<DialogueVault>();

        }
        yield return new WaitForSeconds(secondstoWait);
        DialogueProgression1Func();
            
    }
    public void DialogueProgression1Func() {
        if (isPhoneActive)
            {
                return;
            }
            DialogueManager.instance.StartRPGTextBox(0, 1, 2, dialogueVault.dialogueSets[0]);
    }
    public void recieverPhoneDialogue2SetActive() => recieverPhoneDialogue2.SetActive(DialogueProcessor.instance.isPhoneActive);
    public void ConversationManager() {
        if (isPhoneActive && DialogueManager.instance.DialogueProgression < 3) {
            PhoneConversationManager1();
        } else {
            return;
        }

    }
    public void PhoneConversationManager1() {
        if (!isPhoneActive && DialogueManager.instance.DialogueProgression > 3) {
            return;
        }
        if (DialogueManager.instance.dialogueNumber == 4 || DialogueManager.instance.dialogueNumber == 5) {
            isThinking = true;
            isSurprised = false;
        } else if (DialogueManager.instance.dialogueNumber == 10 || DialogueManager.instance.dialogueNumber == 11) {
            isThinking = false;
            isSurprised = true;
        } else {
            isThinking = false;
            isSurprised = false;
        }
    }
    
    
}
