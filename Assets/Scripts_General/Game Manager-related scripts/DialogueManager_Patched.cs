
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
    public int dialogueNumber = 0;
    public int endDialogueRange;
    public int currentPage = 1;
    public DialogueVault.DialogueSet[] dialogueShells;
    public TextMeshProUGUI rpgText;
    public TextMeshProUGUI personNameText;
    public bool isEnabled = false;

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

        DialogueProgression = 0;

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
        }
        if (personNameText == null && personNameObject != null)
        {
            personNameText = personNameObject.GetComponent<TextMeshProUGUI>();
        }
        if (DialogueProgression == 0 && dialogueVault != null && GameManager.instance.playerpg != null)
        {
            StartRPGTextBox(3, 0, 1, dialogueVault.dialogueSets[0]);
        }
        
    }

    void Update()
    {
        
        if (isEnabled && rpgText != null && rpgTextObject != null)
            DialogueController(dialogueShells);


    }

    public void DialogueController(DialogueVault.DialogueSet[] dialogueArr)
    {
        if (dialogueArr == null || rpgText == null || rpgTextObject == null) return;

        if (dialogueNumber < endDialogueRange)
        {
            if (DialogueProcessor.instance.isPhoneActive) {
                DialogueProcessor.instance.ConversationManager();
            }
            // Replace 'dialogueArr.Length' with the correct property or field, e.g., 'dialogueArr.dialogues.Length'
            if (dialogueArr != null && dialogueNumber >= dialogueArr.Length)
            {
                Debug.LogWarning("Dialogue number exceeds the length of the dialogue array." + endDialogueRange);
                return;
            }
            if (dialogueArr[dialogueNumber].dialogueLine == null)
            {
                Debug.LogWarning("Dialogue at index " + dialogueNumber + " is null.");
                return;
            }
            if (rpgText.textInfo.pageCount <= 1) {
                rpgText.text = dialogueArr[dialogueNumber].dialogueLine;
            } else {
                rpgText.pageToDisplay = currentPage;
                rpgText.text = dialogueArr[dialogueNumber].dialogueLine;
            }
            personNameText.text = dialogueArr[dialogueNumber].characterName;
            
            Debug.Log(dialogueArr[dialogueNumber].characterName + ": " + dialogueArr[dialogueNumber].dialogueLine);
        }
        else
        {

            if (!isEnabled && rpgTextObject != null)
            {
                return;
            }
            if (rpgTextObject != null)
            {
                rpgTextObject.SetActive(false);
            }
            if (personNameObject != null)
            {
                personNameObject.SetActive(false);
            }
            Debug.Log("Dialogue ended at number: " + dialogueNumber);
            if (GameManager.instance != null && GameManager.instance.playerpg != null)
            {
                GameManager.instance.playerpg.isMovable = true;
            }
            dialogueShells = null;
            endDialogueRange = 0;
            dialogueArr = null;
            dialogueVault = null;
            dialogueNumber = 0;
            if (rpgText != null)
            {
                rpgText.text = "";
            }
            Debug.Log("Dialogue ended.");
            if (isEnabled)
            {
                DialogueProgression++;
            }
            DialogueProcessor.instance.DialogueProgressionFunction();
            isEnabled = false;

        }
    }

    public void StartRPGTextBox(int seconds, int startRange, int endRange, DialogueVault.DialogueSet[] dialogueArr)
    {
        StartCoroutine(generateRPGTextBox(seconds, startRange, endRange, dialogueArr));
    }

    public IEnumerator generateRPGTextBox(int seconds, int startRange, int endRange, DialogueVault.DialogueSet[] dialogueArr)
    {
        int originaldialogueNumber = DialogueProgression;
        yield return new WaitForSeconds(seconds);
        if (DialogueProgression == originaldialogueNumber)
        {
            Debug.Log("Starting RPG Text Box after " + seconds + " seconds.");
        }
        else
        {
            Debug.Log("Dialogue progression changed during wait, not starting RPG Text Box.");
            yield break;
        }
        RPGTextBox(startRange, endRange, dialogueArr);
    }

    public void RPGTextBox(int startRange, int endRange, DialogueVault.DialogueSet[] dialogues)
    {
        isEnabled = true;
        if (GameManager.instance?.playerpg != null)
        {
            GameManager.instance.playerpg.isMovable = false;
        }
        dialogueNumber = startRange;
        endDialogueRange = endRange;
        dialogueShells = dialogues;
        if (rpgTextObject != null) rpgTextObject.SetActive(true);
        if (personNameObject != null) personNameObject.SetActive(true);
        


    }
    
}
