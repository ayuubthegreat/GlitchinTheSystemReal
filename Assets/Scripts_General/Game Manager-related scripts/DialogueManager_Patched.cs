
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;
    public TextMeshProUGUI rpgText;
    public GameObject dialogueBox;
    public Button nextButton;
    public int dialogueBounds;
    public int originalDialogueBounds;
    public string brokenSentence;
    public int dialogueIndex = 0;
    public bool isDialogueActive = false;
    public float dialogueSpeed = .3f;
    public string[] dialogueLines;
    public string testLine;
   
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
        originalDialogueBounds = dialogueBounds;
        if (GameManager.instance.DialogueProgression == 0)
        {
           StartDialogueTexts(DialogueVault.instance.dialogueSets[0], 0, 0, 2f); 
        }
        
    }
    void Update()
    {

    }
    public IEnumerator dialogueRunner(string sentence, bool killDialogueScreen = false)
    {
        rpgText.text = string.Empty;
        if (brokenSentence != string.Empty)
        {
            sentence = brokenSentence;
            brokenSentence = string.Empty;
        }
        isDialogueActive = true;
        int i = 0;
        dialogueBounds = originalDialogueBounds;
        while (i < sentence.Length && i < dialogueBounds)
        {
            nextButton.interactable = false;
            rpgText.text = sentence.Substring(0, i);
            if (CheckForSigns(sentence[i]))
            {
                dialogueBounds = i + 1;
                sentence.Remove(i, 1);
                Debug.Log("Detected a sign at index: " + i + " with length: " + sentence.Length);
            }
            yield return new WaitForSeconds(dialogueSpeed);
            i++;
        }
        if (sentence.Length > dialogueBounds)
        {
            brokenSentence = sentence.Substring(i, i + dialogueBounds < sentence.Length - i ? dialogueBounds : sentence.Length - i);
            Debug.Log("Breaking sentence: " + brokenSentence + " at index: " + i + " with length: " + brokenSentence.Length + " The original sentence's length was " + sentence.Length);
        }
        isDialogueActive = false;
        nextButton.interactable = true;

    }
    public bool CheckForSigns(char sign)
    {
        switch (sign)
        {
            case '<':
                return true;
            default:
                return false;
        }
    }
    public void StartDialogueTexts(DialogueVault.DialogueSet[] dialogueSets, int start, int end = -1, float duration = 0f)
    {
        StartCoroutine(DialogueTexts(dialogueSets, start, end, duration));
    }
    public IEnumerator DialogueTexts(DialogueVault.DialogueSet[] dialogueSets, int start, int end = -1, float duration = 0f)
    {
        if (end == -1)
        {
            end = dialogueSets.Length;
        }
        dialogueLines = new string[end - start + 1];
        for (int i = start; i <= end; i++)
        {
            dialogueLines[i - start] = dialogueSets[i].dialogueLine;
        }
        GameManagerRPG.instance.playerpg.isMovable = false;
        dialogueIndex = 0;
        yield return new WaitForSeconds(duration);
        dialogueBox.SetActive(true);
        StartCoroutine(dialogueRunner(dialogueLines[dialogueIndex]));
    }
    public void UpdateDialogueText()
    {
        if (brokenSentence == string.Empty)
        {
            dialogueIndex++;
            if (DialogueProcessor.instance.isConversationActive)
            {
            DialogueProcessor.instance.person1turn = !DialogueProcessor.instance.person1turn;
            DialogueProcessor.instance.person2turn = !DialogueProcessor.instance.person2turn;
            }
        }
        if (dialogueIndex >= dialogueLines.Length)
        {
            Debug.Log("Dialogue ended, resetting dialogue box.");
            if (dialogueBox == null)
            {
                Debug.LogError("Dialogue box is not assigned in the inspector.");
                return;
            }
            GameManagerRPG.instance.playerpg.isMovable = true;
            DialogueProcessor.instance.isConversationActive = false;
            UIManagerRPG.instance.ControlRPGUIElements(false);
            dialogueLines = new string[0];
            isDialogueActive = false;
            rpgText.text = string.Empty;
            dialogueBounds = originalDialogueBounds;
            dialogueIndex = 0;
            GameManager.instance.DialogueProgression++;
            DialogueProcessor.instance.DialogueProgressionFunction();
            Debug.Log("Dialogue ended.");
            dialogueBox.SetActive(false);
            return;
        }

        StopAllCoroutines();
        dialogueBounds = originalDialogueBounds;
        
        StartCoroutine(dialogueRunner(dialogueLines[dialogueIndex]));
    }
    public void StartConversation()
    {
        DialogueProcessor.instance.isConversationActive = true;
        DialogueProcessor.instance.ConversationManager();
    }
    

    
}
