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
    public bool isTalkingToHomelessMan = false;

    public bool[] faces;
    public bool[] expressions;
    public BoxCollider2D npcDetector;

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
            faces = new bool[] {
            // isThinking
            false,
            // isSurprised
            false
        };
            expressions = new bool[] {
            // isThinkingFace
            false,
            // isSurprisedFace
            false
        };

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
            if (GameManager.instance.DialogueProgression == 0)
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
        bool conversationEnded = GameManager.instance.DialogueProgression == 3 && isPhoneActive;
        bool conversationBegan = GameManager.instance.DialogueProgression == 2 && isPhoneActive;
        if (GameManager.instance.DialogueProgression == 1)
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
            UIManagerRPG.instance.phone.SetActive(false);
        }
        else if (isTalkingToHomelessMan && GameManager.instance.DialogueProgression > 2)
        {
            DialogueManager.instance.StartTextBox(0, 0, dialogueVault.dialogueSets[2].Length, dialogueVault.dialogueSets[2]);

        }

    }
    public IEnumerator PhoneRinging(int seconds)
    {
        UIManagerRPG.instance.dialogueAnimations.SetActive(true);
        recieverPhoneDialogue2.SetActive(false);
        yield return new WaitForSeconds(seconds);
        isConversationActive = true;
        recieverPhoneDialogue2SetActive();

        DialogueManager.instance.StartTextBox(0, 0, dialogueVault.dialogueSets[1].Length, dialogueVault.dialogueSets[1]);
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
    public void DialogueProgression1Func()
    {
        if (isPhoneActive)
        {
            return;
        }
        UIManagerRPG.instance.phone.SetActive(true);
        DialogueManager.instance.StartTextBox(2, 1, 2, dialogueVault.dialogueSets[0]);
    }
    public void recieverPhoneDialogue2SetActive() => recieverPhoneDialogue2.SetActive(isPhoneActive);
    public void ConversationManager()
    {
        if (GameManager.instance.DialogueProgression < 3)
        {
            PhoneConversationManager1();
        }
        else
        {
            return;
        }

    }
    public void PhoneConversationManager1()
    {
        if (!isPhoneActive && GameManager.instance.DialogueProgression > 3)
        {
            return;
        }
        if (DialogueManager.instance.dialogueNumber == 4 || DialogueManager.instance.dialogueNumber == 5)
        {
            ChangeExpressionBools(0);
            ChangeFaceExpressionBools(0);
        }
        else if (DialogueManager.instance.dialogueNumber == 10 || DialogueManager.instance.dialogueNumber == 11)
        {
            ChangeExpressionBools(1);
            ChangeFaceExpressionBools(1);
        }
        else
        {
            ChangeExpressionBools(expressions.Length + 5);
            ChangeFaceExpressionBools(faces.Length + 1);
        }
    }
    public void ChangeExpressionBools(int value)
    {

        for (int i = 0; i < expressions.Length; i++)
        {
            if (value == i && value <= expressions.Length)
            {
                Debug.Log(expressions[i]);
                expressions[i] = true;
            }
            else
            {
                expressions[i] = false;
            }
        }


    }
    public void ChangeFaceExpressionBools(int value)
    {
        for (int i = 0; i < faces.Length; i++)
        {
            if (value == i && value <= expressions.Length)
            {
                faces[i] = true;
            }
            else
            {
                faces[i] = false;
            }

        }

    }
    public void PoseChanger(Transform targetTransform)
    {
        playerpg playerpg = GameManagerRPG.instance.playerpg;
        if (targetTransform.position.x > playerpg.transform.position.x)
        {
            playerpg.startingPose = 2;
        }
        else
        {
            playerpg.startingPose = 3;
        }


    }
    public void MovetoTarget(Transform Target)
    {
        if (Vector2.Distance(Target.position, GameManagerRPG.instance.playerpg.transform.position) < 0.01f)
        {
            return;
        }
        npcDetector.enabled = false;
        GameManagerRPG.instance.StartMovingAutonomously(Target.position);
    }
}
    
