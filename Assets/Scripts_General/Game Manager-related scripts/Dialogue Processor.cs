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
    public float npcSpeed = 20f;

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
        bool conversationEnded1 = GameManager.instance.DialogueProgression == 3 && isPhoneActive;
        bool conversationBegan1 = GameManager.instance.DialogueProgression == 2 && isPhoneActive;
        if (GameManager.instance.DialogueProgression == 1)
        {
            StartCoroutine(DialogueProgression1());

        }
        else if (conversationBegan1)
        {
            isPhoneActive = true;
            int secondsToWait = UnityEngine.Random.Range(5, 10);


            StartCoroutine(PhoneRinging(secondsToWait)); // Start the conversation with Abdurahman

        }
        else if (conversationEnded1)
        {
            isPhoneActive = false;
            recieverPhoneDialogue2SetActive();
            UIManagerRPG.instance.phone.SetActive(false);
        }
        else if (GameManager.instance.DialogueProgression > 1 && GameManager.instance.DialogueProgression < 4 && !isPhoneActive)
        {
            GameManagerRPG.instance.playerpg.isMovable = false;
            GameManagerRPG.instance.CameraZoom(10f, 10f);
            NPCManager.instance.StartMovingNPC(0, 20f, new Vector2[] { new Vector2(0, (NPCManager.instance.playerPosition.y - NPCManager.instance.transform.position.y) - 6), new Vector2(NPCManager.instance.playerPosition.x - NPCManager.instance.transform.position.x, 0) });
        }
        else if (GameManager.instance.DialogueProgression == 4)
        {
            FadeManager.instance.Fader(false, UIManagerRPG.instance.cutsceneImages[0], UIManagerRPG.instance.cutsceneImageObject);
        } else if (GameManager.instance.DialogueProgression == 5)
        {
            FadeManager.instance.Fader(true, UIManagerRPG.instance.cutsceneImages[0], UIManagerRPG.instance.cutsceneImageObject);
           

        }

    }
    public IEnumerator RestartCameraZoom(float waitForPlayerMovement)
    {
        GameManagerRPG.instance.playerpg.isMovable = false;
        yield return new WaitForSeconds(waitForPlayerMovement);
        GameManagerRPG.instance.CameraZoom(5f, 10f);
        yield return new WaitForSeconds(waitForPlayerMovement);
        GameManagerRPG.instance.playerpg.isMovable = true;
    }
    public IEnumerator PhoneRinging(int seconds)
    {
        UIManagerRPG.instance.dialogueAnimations.SetActive(true);
        recieverPhoneDialogue2.SetActive(false);
        yield return new WaitForSeconds(seconds);
        isConversationActive = true;
        recieverPhoneDialogue2SetActive();

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
       
    }
    public void recieverPhoneDialogue2SetActive() => recieverPhoneDialogue2.SetActive(isPhoneActive);
    
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

}
