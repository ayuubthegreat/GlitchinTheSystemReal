
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;
    public DialogueVault dialogueVault;
    public GameObject rpgTextObject;
    public GameObject personNameObject;
    public int DialogueProgression = 0;
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
    public int startBrokenSentence = 0;
    public int endBrokenSentence = 0;
    public string brokenSentence;
    public int dialogueSpeed;
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


        // DialogueProgression = 0;

        if (rpgTextObject == null)
        {
            rpgTextObject = GameObject.Find("rpgText");
        }
        if (personNameObject == null)
        {
            personNameObject = GameObject.Find("personNameText");
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
        if (DialogueProgression == 0 && dialogueVault != null && GameManager.instance.playerpg != null)
        {
            StartTextBox(3, 0, 1, dialogueVault.dialogueSets[0]);
        }

    }

    void Update()
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
        personNameText.text = sets[dialogueNumber].characterName;
        string sentence = sets[dialogueNumber].dialogueLine;
        if (brokenSentence != string.Empty)
        {
            sentence = brokenSentence;
            brokenSentence = string.Empty;
        }
        isTalking = true;
        GameManager.instance.playerpg.isMovable = false;
        if (!isTalking)
        {
            yield break;
        }
        for (int i = 0; i < sentence.Length + 1; i++)
        {



            if (Input.GetKeyDown(KeyCode.Space))
            {
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
                isTalking = false;
                continue;
            }
            if (rpgText.text.Length < dialogueBounds - 1)
            {
                Debug.Log(rpgText.text.Length);
                rpgText.text = sentence.Substring(0, i + 1);
                yield return new WaitForSeconds(dialogueSpeed);
                continue;
            }
                



            if (sentence.Length < dialogueBounds)
            {
                rpgText.text = sentence;
                brokenSentence = string.Empty;
                isTalking = false;
                break;
            }
            else
            {
                rpgText.text = sentence.Substring(0, dialogueBounds);
                int newLength = sentence.Length - dialogueBounds;


                brokenSentence = sentence.Substring(dialogueBounds, newLength);
                isTalking = false;
                break;


            }
                
            





        }
        
        
        

    }


    public void ResetDialogue()
    {
        GameManager.instance.playerpg.isMovable = true;
        DialogueProgression++;
        dialogueNumber = 0;
        dialogueShells = null;
        startRange = 0;
        endDialogueRange = 0;
        currentPage = 1;
        rpgText.text = string.Empty;
        UIManager.instance.ChangeStartTransitionsBoolArray(0, false);
        UIManager.instance.ChangeStartTransitionsBoolArray(1, false);

        DialogueProcessor.instance.DialogueProgressionFunction();
    }
    
    
}
