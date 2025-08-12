
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
    public TextMeshProUGUI personName;
    public TextMeshProUGUI yesButtonText;
    public TextMeshProUGUI noButtonText;
    public GameObject dialogueBox;
    public GameObject personNameBox;
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
    public string[] speakerNames;
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
    public IEnumerator dialogueRunner(string sentence, string speaker = "")
    {
        rpgText.text = string.Empty;
        personName.text = speaker;

        if (brokenSentence != string.Empty)
        {
            sentence = brokenSentence;
            brokenSentence = string.Empty;
        }
        isDialogueActive = true;
        int i = 0;

        while (i < sentence.Length && i < dialogueBounds)
        {
            GameManagerRPG.instance.soundEffectSource.PlayOneShot(GameManagerRPG.instance.dialogueBlips[0]);
            nextButton.interactable = false;
            rpgText.text = sentence[..i];
            if (CheckForSigns(sentence[i]))
            {
                sentence.Remove(i, 1);
                dialogueBounds = i + 1;
                Debug.Log("Detected a sign at index: " + i + " with length: " + sentence.Length);
                yield return new WaitForSeconds(0.1f);
            }
            yield return new WaitForSeconds(dialogueSpeed);
            i++;
        }
        if (sentence.Length > dialogueBounds)
        {
            brokenSentence = sentence.Substring(i, sentence.Length - i);
            Debug.Log("Breaking sentence: " + brokenSentence + " at index: " + i + " with length: " + brokenSentence.Length + " The original sentence's length was " + sentence.Length);
        }
        rpgText.text = sentence.Length <= dialogueBounds ? sentence : sentence[..dialogueBounds]; // Display the full sentence after the loop
        isDialogueActive = false;
        nextButton.interactable = true;
        dialogueBounds = originalDialogueBounds;
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
        StopAllCoroutines();
        StartCoroutine(DialogueTexts(dialogueSets, start, end, duration));
    }
    public IEnumerator DialogueTexts(DialogueVault.DialogueSet[] dialogueSets, int start, int end = -1, float duration = 0f)
    {
        if (end == -1)
        {
            end = dialogueSets.Length;
        }

        dialogueLines = new string[end - start + 1];
        speakerNames = new string[end - start + 1];
        for (int i = start; i <= end; i++)
        {
            dialogueLines[i - start] = dialogueSets[i].dialogueLine;
            speakerNames[i - start] = dialogueSets[i].characterName;
        }
        GameManagerRPG.instance.playerpg.isMovable = false;
        dialogueIndex = 0;
        currentDialogueSet = dialogueSets;
        yield return new WaitForSeconds(duration);
        dialogueBox.SetActive(true);
        personNameBox.SetActive(true);
        personName.text = currentDialogueSet[dialogueIndex].characterName;
        StartCoroutine(dialogueRunner(dialogueLines[dialogueIndex], speakerNames[dialogueIndex]));
    }
    public void UpdateDialogueText()
    {
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
            if (!GameManagerRPG.instance.movingAutonomously)
            {
                GameManager.instance.DialogueProgression++;
            }

            DialogueProcessor.instance.DialogueProgressionFunction();

            Debug.Log("Dialogue ended.");
            dialogueBox.SetActive(false);
            return;
        }
        if (currentDialogueSet[dialogueIndex].dialogueAction != null && dialogueIndex < dialogueLines.Length)
        {
            currentDialogueSet[dialogueIndex].dialogueAction.Invoke();
            return;
        }
        
        if (brokenSentence == string.Empty)
        {
            dialogueIndex++;
            if (dialogueIndex >= dialogueLines.Length)
            {
                Debug.LogWarning("Dialogue index exceeds the length of dialogue lines. Resetting dialogue.");
                return;
            }
                personName.text = currentDialogueSet[dialogueIndex].characterName;
            
            if (DialogueProcessor.instance.isConversationActive)
            {
                DialogueProcessor.instance.person1turn = currentDialogueSet[dialogueIndex].characterName == "Abdurahman";
                DialogueProcessor.instance.person2turn = dialogueIndex % 2 == 0 && currentDialogueSet[dialogueIndex].characterName != "Narrator";
            }
        }
        
        
        StopAllCoroutines();
        dialogueBounds = originalDialogueBounds;

        StartCoroutine(dialogueRunner(dialogueLines[dialogueIndex], speakerNames[dialogueIndex]));
    }
    public void StartConversation()
    {
        DialogueProcessor.instance.isConversationActive = true;
        DialogueProcessor.instance.ConversationManager();
    }
    public void DisplayChoices(string choiceName, string choice1, string choice2)
    {
        rpgText.text = choiceName;
        nextButton.gameObject.SetActive(false);
        yesButton.gameObject.SetActive(true);
        noButton.gameObject.SetActive(true);
        yesButtonText.text = choice1;
        noButtonText.text = choice2;
    }
    public void ShowResultsYes()
    {
        GameManager.instance.choicesBools[GameManager.instance.currentChoiceIndex] = true;
        GameManager.instance.currentChoiceIndex++;
        Debug.Log("Choice 1 selected.");
        hitYesButton = true;
        hitNoButton = false;
        yesButton.gameObject.SetActive(false);
        noButton.gameObject.SetActive(false);
        nextButton.gameObject.SetActive(true);
        dialogueIndex = 0;
        switch (GameManager.instance.DialogueProgression)
        {
            
            case 2:
                Debug.Log("Player has chosen to invite Yasir to the revolution.");
                StartDialogueTexts(DialogueProcessor.instance.dialogueVault.dialogueSetsYes[0], 0, DialogueProcessor.instance.dialogueVault.dialogueSetsYes[0].Length - 1, 0f);
                break;
            case 3:
                Debug.Log("Player has chosen to support the revolution.");
                break;
        }

    }
    public void ShowResultsNo()
    {
        GameManager.instance.choicesBools[GameManager.instance.currentChoiceIndex] = false;
        GameManager.instance.currentChoiceIndex++;
        Debug.Log("Choice 2 selected.");
        hitYesButton = false;
        hitNoButton = true;
        yesButton.gameObject.SetActive(false);
        noButton.gameObject.SetActive(false);
        nextButton.gameObject.SetActive(true);
        dialogueIndex = 0;
        switch (GameManager.instance.DialogueProgression)
        {
            case 2:
                Debug.Log("Player has chosen not to invite Yasir to the revolution.");
                StartDialogueTexts(DialogueProcessor.instance.dialogueVault.dialogueSetsNo[0], 0, DialogueProcessor.instance.dialogueVault.dialogueSetsNo[0].Length - 1, 0f);
                break;
            case 3:
                Debug.Log("Player has chosen not to support the revolution.");
                break;
        }
    }
    
    #endregion



}
