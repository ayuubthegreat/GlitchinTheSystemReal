using UnityEngine;
using System;
using System.Collections;
using AOT;
using Unity.VisualScripting;

public class DialogueProcessor : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameManager gameManager;
    public GameManagerRPG.CutsceneAssembler currentCutscene;
    public Animator[] currentViableCutsceneAnimators;
    public GameObject[] currentCutsceneObjects;
    public DialogueVault dialogueVault;
    public static DialogueProcessor instance;
    public NPC franticTeenager;
    public teleport introductoryPhoneBooth;
    public GameObject playerPhoneDialogue;
    public GameObject recieverPhoneDialogue2;
    public int PhoneRingingSeconds;
    public bool isConversationActive = false;
    public bool person1turn = false;
    public bool person2turn = false;
    public bool twopeopletalking = false;
    public bool isPhoneActive = false;
    public bool isTalkingToHomelessMan = false;
    public float npcSpeed = 20f;

    public bool[] faces;
    public bool[] expressions;
    public BoxCollider2D npcDetector;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        gameManager = GetComponent<GameManager>();
        dialogueVault = GetComponent<DialogueVault>();



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
        switch (GameManager.instance.DialogueProgression)
        {
            case 1:
                StartCoroutine(DialogueProgression1());
                break;
            case 4:
            case 5:
            case 6:
            case 7:
            case 8:
                isConversationActive = true;
                break;

        }
        if (isConversationActive)
        {
            ConversationManager();
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
        yield return new WaitForSeconds(.1f);
        GameManagerRPG.instance.playerpg.isMovable = false;
        Debug.Log("Phone is ringing for " + seconds + " seconds.");
        playerPhoneDialogue.SetActive(true);
        yield return new WaitForSeconds(seconds);
        recieverPhoneDialogue2.SetActive(true);
        DialogueManager.instance.StartDialogueTexts(dialogueVault.dialogueSets[1], 0, dialogueVault.dialogueSets[1].Length - 1, 2f);
    }
    public IEnumerator DialogueProgression1()
    {
        int secondstoWait = UnityEngine.Random.Range(2, 5);
        Debug.Log(secondstoWait + " seconds to wait before starting the dialogue.");
        if (dialogueVault == null)
        {
            dialogueVault = GetComponent<DialogueVault>();

        }
        DialogueManager.instance.StartDialogueTexts(dialogueVault.dialogueSets[0], 1, 1, secondstoWait);
        yield return new WaitForSeconds(secondstoWait);
        UIManagerRPG.instance.phone.SetActive(true);
    }


    public void FranticTeenagerDialogue1()
    {
        DialogueManager.instance.StartDialogueTexts(dialogueVault.dialogueSets[2], 0, 4, .3f, franticTeenager);
    }

    public void ConversationManager(NPC npc = null)
    {
        person1turn = true;
        switch (GameManager.instance.DialogueProgression)
        {
            case 2:
                int secondstoWait = UnityEngine.Random.Range(5, 10);
                StartCoroutine(PhoneRinging(secondstoWait));
                break;
            case 3:
                GameManagerRPG.instance.CameraZoom(10f, 10f);
                Debug.Log("NPC Position: " + npc.transform.position + " Player Position: " + GameManagerRPG.instance.playerpg.transform.position);
                npc.StartMovingNPC(0, 20f, new Vector2[] { new Vector2(0f, npc.playerPosition.y - npc.transform.position.y - 5f), new Vector2(npc.playerPosition.x - npc.transform.position.x, 0f) }, FranticTeenagerDialogue1);
                break;
            case 4:
                fader.instance.Fader(false, UIManagerRPG.instance.cutsceneImageBackgrounds[0], UIManagerRPG.instance.cutsceneImageObject);
                CutsceneManager(GameManagerRPG.instance.cutsceneAssemblers[0], StartIDP);
                currentCutsceneObjects[4].GetComponent<dialogueObject>().bodyAnim.speed = 0f;
                DialogueManager.instance.StartDialogueTexts(dialogueVault.dialogueSets[2], 5, 10, 2f);
                UIManagerRPG.instance.phone.SetActive(false);
                break;
            case 5:
                fader.instance.Fader(true, UIManagerRPG.instance.cutsceneImageBackgrounds[0], UIManagerRPG.instance.cutsceneImageObject);
                DialogueManager.instance.StartDialogueTexts(dialogueVault.dialogueSets[2], 11, 20, 3f);
                break;
            case 6:
                GameManagerRPG.instance.MoveCamera(new Vector3(introductoryPhoneBooth.transform.position.x - Camera.main.transform.position.x, introductoryPhoneBooth.transform.position.y - Camera.main.transform.position.y, 0), 50f);
                DialogueManager.instance.StartDialogueTexts(dialogueVault.dialogueSets[2], 21, 27, 3f);
                break;
            case 7:
                GameManagerRPG.instance.MoveCamera(new Vector3(GameManagerRPG.instance.playerpg.transform.position.x - Camera.main.transform.position.x, GameManagerRPG.instance.playerpg.transform.position.y - Camera.main.transform.position.y, 0), 50f);
                DialogueManager.instance.StartDialogueTexts(dialogueVault.dialogueSets[2], 28, -1, 3f);
                break;
            case 8:
                franticTeenager.facingDir = 0;
                franticTeenager.StartMovingNPC(0, 30f, new Vector2[] { new Vector2(-30, 0) }, BeginAutonomousExploration);
                break;
            default:
                Debug.Log("No conversation detected.");
                return;
        }
    }
    public void BeginAutonomousExploration()
    {
        GameManagerRPG.instance.CameraZoom(8f, 5f);
        UIManagerRPG.instance.phone.SetActive(true);
        GameManagerRPG.instance.movingAutonomously = true;
        GameManagerRPG.instance.playerpg.isMovable = true;
    }
    public void StartIDP() => StartCoroutine(InternalDialogueProcessor());
    public IEnumerator InternalDialogueProcessor()
    {
        switch (GameManager.instance.DialogueProgression)
        {
            case 1:
                break;
            case 2:
                break;
            case 3:
                break;
            case 4:
                switch (DialogueManager.instance.dialogueIndex)
                {
                    case 1:
                        
                        currentViableCutsceneAnimators[1].SetTrigger("move");
                        break;
                    case 2:
                        currentViableCutsceneAnimators[1].SetTrigger("moveBack");
                        DialogueManager.instance.dialogueBox.SetActive(false);
                        DialogueManager.instance.isDialogueActive = false;
                        yield return new WaitForSeconds(1f);
                        currentCutsceneObjects[0].transform.localPosition = new Vector3(900, 0, 0);
                        currentCutsceneObjects[1].transform.localPosition = new Vector3(900, 0, 0);
                        currentCutsceneObjects[2].transform.localPosition = new Vector3(0, 0, 0);
                        currentCutsceneObjects[3].transform.localPosition = new Vector3(0, 88, 0);
                        currentCutsceneObjects[4].transform.localPosition = new Vector3(-4, 79, 0);
                        UIManagerRPG.instance.cutsceneImageObject.sprite = UIManagerRPG.instance.cutsceneImageBackgrounds[1];
                        yield return new WaitForSeconds(2f);
                        currentCutsceneObjects[4].GetComponent<dialogueObject>().bodyAnim.speed = 1f;
                        break;
                }
                break;
            case 5:

                break;
            case 6:
                break;
            case 7:
                break;
            case 8:
                break;
        }
    }
    public void CutsceneManager(GameManagerRPG.CutsceneAssembler cutscene, System.Action desiredFunction = null)
    {
        if (cutscene.cutsceneObjectSizes.Length < 0)
        {
            Debug.LogError("Invalid cutscene object number.");
            return;
        }
        currentCutscene = cutscene;
        currentCutsceneObjects = new GameObject[cutscene.cutsceneObjectSizes.Length];
        currentViableCutsceneAnimators = new Animator[cutscene.cutsceneObjectSizes.Length * 2];
        int i = 0;
        while (i < cutscene.cutsceneObjectSizes.Length)
        {
            GameObject cutsceneObject = Instantiate(UIManagerRPG.instance.cutsceneObjectPrefab, UIManagerRPG.instance.cutsceneParent.transform);
            if (cutscene.headAnims[i] == null)
            {
                cutsceneObject.GetComponent<dialogueObject>().headAnim.gameObject.SetActive(false);
            }
            else
            {
                cutsceneObject.GetComponent<dialogueObject>().headAnim.runtimeAnimatorController = cutscene.headAnims[i];
                currentViableCutsceneAnimators[i] = cutsceneObject.GetComponent<dialogueObject>().headAnim;
            }

            cutsceneObject.transform.localPosition = cutscene.cutsceneObjectPositions[i];
            if (cutscene.bodyAnims[i] == null)
            {
                cutsceneObject.GetComponent<dialogueObject>().bodyAnim.gameObject.SetActive(false);
            }
            else
            {
                cutsceneObject.GetComponent<dialogueObject>().bodyAnim.runtimeAnimatorController = cutscene.bodyAnims[i];
                currentViableCutsceneAnimators[i + 1] = cutsceneObject.GetComponent<dialogueObject>().bodyAnim;
            }
            cutsceneObject.transform.localScale = Vector3.one * cutscene.cutsceneObjectSizes[i];
            cutsceneObject.name = cutscene.characterNames[i];
            currentCutsceneObjects[i] = cutsceneObject;
            i++;
            
        }
        
        desiredFunction?.Invoke();
    }

}
