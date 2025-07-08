
using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework.Constraints;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;
    public DialogueVault dialogueVault;
    public GameObject rpgTextObject;
    public GameObject personNameObject;
    
    public int startRange;
    public int dialogueNumber = 0;
    public int endDialogueRange;
    public int currentPage = 1;
    public int currentPageAuto;
    public DialogueVault.DialogueSet[] dialogueShells;
    public TextMeshProUGUI rpgText;
    public TextMeshProUGUI personNameText;
    public bool isEnabled = false;
    public bool isTalking = false;
    public bool[] playersTalking;
    public int startBrokenSentence = 0;
    public int endBrokenSentence = 0;
    public string brokenSentence;
    public float dialogueSpeed;
    public int dialogueBounds;

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


    }
    void Start()
    {
        dialogueVault = GetComponent<DialogueVault>();

        playersTalking = new bool[] {
            // Player 1 (usually Abdurahman)
            false,
            // Player 2 (the other person Abdurahman is speaking to)
            false,
        };
        // GameManager.instance.DialogueProgression = 0;

        if (rpgTextObject == null && UIManager.instance.currentScreen == MainScreens.RPG)
        {
            rpgTextObject = GameObject.Find("rpgText");
            rpgTextObject.SetActive(false);
        
        }
        if (personNameObject == null && UIManager.instance.currentScreen == MainScreens.RPG)
        {
            personNameObject = GameObject.Find("personNameText");
            personNameObject.SetActive(false);
        }

        if (rpgText == null && rpgTextObject != null)
        {
            rpgText = rpgTextObject.GetComponent<TextMeshProUGUI>();
            currentPageAuto = rpgText.pageToDisplay;
        }
        if (personNameText == null && personNameObject != null)
        {
            personNameText = personNameObject.GetComponent<TextMeshProUGUI>();
        }
        if (GameManager.instance.DialogueProgression == 0 && dialogueVault != null && GameManagerRPG.instance.playerpg != null)
        {
            StartTextBox(3, 0, 1, dialogueVault.dialogueSets[0]);
        }

        

    }

    void Update()
    {
        if (GameManagerRPG.instance.playerpg != null)
        {
          if (isEnabled)
        {
            if (rpgTextObject != null) rpgTextObject.SetActive(true);
            if (personNameObject != null) personNameObject.SetActive(true);
        }
        else
        {
            rpgTextObject.SetActive(false);
            personNameObject.SetActive(false);
        }  
        }
        
    }
    public void StartTextBox(int seconds, int startRangee, int endRange, DialogueVault.DialogueSet[] dialogueSet)
    {
        StartCoroutine(RPGTextBox(seconds, startRangee, endRange, dialogueSet));
    }
    public IEnumerator RPGTextBox(int seconds, int startRangee, int endRange, DialogueVault.DialogueSet[] dialogueSet)
    {
        yield return new WaitForSeconds(seconds);
        isEnabled = true;
        UIManager.instance.startTransitions[0] = true;
        startRange = startRangee;
        endDialogueRange = endRange;
        dialogueNumber = startRangee;
        dialogueShells = dialogueSet;
    }
    public void StartDialogueController()
    {
        StopAllCoroutines();
        StartCoroutine(DialogueController(dialogueShells));
    }
    public IEnumerator DialogueController(DialogueVault.DialogueSet[] sets)
    {
        isTalking = true;
        personNameText.text = sets[dialogueNumber].characterName;
        TalkingStick(sets[dialogueNumber].characterName);

        string sentence = sets[dialogueNumber].dialogueLine;
        rpgText.text = string.Empty;
        if (brokenSentence != string.Empty)
        {
            sentence = brokenSentence;
            brokenSentence = string.Empty;
        }

        GameManagerRPG.instance.playerpg.isMovable = false;
        if (!isTalking)
        {
            yield break;
        }
        for (int i = 0; i < sentence.Length + 1; i++)
        {



            if (Input.GetKeyDown(KeyCode.Space))
            {
                isTalking = false;
                ResetTalkingStick();
                Debug.Log(sentence.Length);

                if (sentence.Length < dialogueBounds)
                {
                    rpgText.text = sentence;
                    brokenSentence = string.Empty;
                }
                else
                {
                    rpgText.text = sentence.Substring(0, dialogueBounds);
                    int newLength = sentence.Length - dialogueBounds;


                    brokenSentence = sentence.Substring(dialogueBounds, newLength);


                }

                break;
            }
            if (rpgText.text.Length < dialogueBounds - 1)
            {
                Debug.Log(rpgText.text.Length);
                rpgText.text = sentence.Substring(0, i);
                yield return new WaitForSeconds(dialogueSpeed);
                continue;
            }
            else
            {
                isTalking = false;
                ResetTalkingStick();
                if (sentence.Length < dialogueBounds)
                {
                    rpgText.text = sentence;
                    brokenSentence = string.Empty;
                    break;
                }
                else
                {
                    rpgText.text = sentence.Substring(0, dialogueBounds);
                    int newLength = sentence.Length - dialogueBounds;


                    brokenSentence = sentence.Substring(dialogueBounds, newLength);

                    break;


                }
            }












        }




    }


    public void ResetDialogue()
    {
        GameManagerRPG.instance.playerpg.isMovable = true;
        GameManager.instance.DialogueProgression++;
        dialogueNumber = 0;
        dialogueShells = null;
        startRange = 0;
        endDialogueRange = 0;
        currentPage = 1;
        rpgText.text = string.Empty;
        UIManager.instance.ChangeStartTransitionsBoolArray(0, false);
        UIManager.instance.ChangeStartTransitionsBoolArray(1, false);
        ResetTalkingStick();

        DialogueProcessor.instance.DialogueProgressionFunction();
    }
    public void TalkingStick(string characterName)
    {
        switch (characterName)
        {
            case "Abdurahman":
                playersTalking[0] = true;
                break;
            case "Yasir":
                playersTalking[1] = true;
                break;
            // Add more cases as needed
            default:

                break;
        }
    }
    public void ResetTalkingStick()
    {
        for (int i = 0; i < playersTalking.Length; i++)
                {
                    playersTalking[i] = false;
                }
    }
    
}
