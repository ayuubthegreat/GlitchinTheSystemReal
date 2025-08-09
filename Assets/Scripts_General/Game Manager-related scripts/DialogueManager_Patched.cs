
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    #region Values and Variables
    public static DialogueManager instance;
    public DialogueVault.DialogueSet[] currentDialogueSet;
    public TextMeshProUGUI rpgText;
    public TextMeshProUGUI yesButtonText;
    public TextMeshProUGUI noButtonText;
    public GameObject dialogueBox;
    public Button nextButton;
    public Button yesButton;
    public Button noButton;
    public bool hitYesButton = false;
    public bool hitNoButton = false;
    public int dialogueBounds;
    public int originalDialogueBounds;
    public string brokenSentence;
    public int dialogueIndex = 0;
    public bool isDialogueActive = false;
    public float dialogueSpeed = .3f;
    public string[] dialogueLines;
    public string testLine;
    #endregion

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
    #region Dialogue Functions
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
        currentDialogueSet = dialogueSets;
        yield return new WaitForSeconds(duration);
        dialogueBox.SetActive(true);
        StartCoroutine(dialogueRunner(dialogueLines[dialogueIndex]));
    }
    public void UpdateDialogueText()
    {
        if (currentDialogueSet[dialogueIndex].dialogueAction != null)
        {
            currentDialogueSet[dialogueIndex].dialogueAction.Invoke();
            return;
        }
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
            
            
            DialogueProcessor.instance.DialogueProgressionFunction();
            if (!GameManagerRPG.instance.movingAutonomously)
            {
                GameManager.instance.DialogueProgression++;
            }
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
    public void DisplayChoices(string choiceName, string choice1, string choice2)
    {
        rpgText.text = choiceName;
        yesButton.gameObject.SetActive(true);
        noButton.gameObject.SetActive(true);
        yesButtonText.text = choice1;
        noButtonText.text = choice2;
    }
    public void ShowResults(int choice)
    {
        switch (choice)
        {
            case 1:
                Debug.Log("Choice 1 selected.");
                hitYesButton = true;
                hitNoButton = false;
                switch (GameManager.instance.DialogueProgression)
                {
                    case 2:
                        Debug.Log("Player has chosen to invite Yasir to the revolution.");
                        break;
                    case 3:
                        Debug.Log("Player has chosen to support the revolution.");
                        break;
                }
                break;
            case 2:
                Debug.Log("Choice 2 selected.");
                hitYesButton = false;
                hitNoButton = true;
                switch (GameManager.instance.DialogueProgression)
                {
                    case 2:
                        Debug.Log("Player has chosen not to invite Yasir to the revolution.");
                        break;
                    case 3:
                        Debug.Log("Player has chosen not to support the revolution.");
                        break;
                }
                break;
            default:
                Debug.LogWarning("Invalid choice selected.");
                return;
        }
    }
    #endregion
    

    
}
