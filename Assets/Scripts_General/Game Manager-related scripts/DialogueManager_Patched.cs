
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
    public NPC currentNPC;
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
            instance = this;
        else
            Destroy(gameObject);
    }
    void Start()
    {
        if (dialogueBox == null)
        {
            Debug.LogError("Dialogue box is not assigned in the inspector.");
            return;
        }
        originalDialogueBounds = dialogueBounds;
        DialogueProcessor.instance.DialogueProgressionFunction();
    }
    #region Dialogue Functions
    public IEnumerator dialogueRunner(string sentence, string speaker = "")
    {
        if (currentNPC != null)
        {
            currentNPC.isInDialogue = true;
        }
        rpgText.text = string.Empty;
        if (personName.text == string.Empty)
        {
            personNameBox.SetActive(false);
        }
        personName.text = speaker;

        if (brokenSentence != string.Empty)
        {
            sentence = brokenSentence;
            brokenSentence = string.Empty;
        }
        isDialogueActive = true;
        if (!dialogueBox.activeSelf)
        {
            Debug.LogWarning("The dialogue box is not active in the hierarchy. Cannot start dialogue.");
            isDialogueActive = false;
            yield break;
        }
        int i = 0;

        while (i < sentence.Length && i < dialogueBounds)
        {
            if (!isDialogueActive)
            {
                yield break;
            }
            GameManagerRPG.instance.soundEffectSource.PlayOneShot(GameManagerRPG.instance.dialogueBlips[0]);
            nextButton.interactable = false;
            rpgText.text = sentence[..i];
            if (CheckForSigns(sentence[i]))
            {
                sentence.Remove(i, 1);
                dialogueBounds = i;
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
        rpgText.text = sentence.Length <= dialogueBounds ? CheckSentenceForSigns(sentence) : CheckSentenceForSigns(sentence)[..dialogueBounds]; // Display the full sentence after the loop
        isDialogueActive = false;
        nextButton.interactable = true;
        dialogueBounds = originalDialogueBounds;
    }
    public string CheckSentenceForSigns(string sentence)
    {
        if (string.IsNullOrEmpty(sentence))
        {
            Debug.LogWarning("The sentence is empty or null.");
            return string.Empty;
        }
        for (int i = 0; i < sentence.Length; i++)
        {
            if (CheckForSigns(sentence[i]))
            {
                Debug.Log("Sign detected at index: " + i + " with character: " + sentence[i]);
                sentence = sentence.Remove(i, 1);
                return sentence; // Return the modified sentence without the sign
            }
        }
        Debug.Log("No signs detected in the sentence.");
        return sentence;
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
    public void StartDialogueTexts(DialogueVault.DialogueSet[] dialogueSets, int start, int end = -1, float duration = 0f, NPC npc = null, bool isAutonomus = false, int autonomusDuration = 2)
    {
        StopAllCoroutines();
        StartCoroutine(DialogueTexts(dialogueSets, start, end, duration, npc, isAutonomus, autonomusDuration));
    }
    public IEnumerator DialogueTexts(DialogueVault.DialogueSet[] dialogueSets, int start, int end = -1, float duration = 0f, NPC npc = null, bool isAutonomus = false, int autonomusDuration = 2)
    {

        if (end == -1)
        {
            end = dialogueSets.Length - 1;
        }
        if (isAutonomus)
        {
            nextButton.gameObject.SetActive(false);
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
        GameManagerRPG.instance.playerpg.rb.linearVelocity = Vector2.zero;
        dialogueBox.SetActive(true);
        personNameBox.SetActive(true);
        if (npc != null)
        {
            currentNPC = npc;
            currentNPC.FacePlayer();
        }
        GameManagerRPG.instance.isCutsceneActive = true;

        personName.text = currentDialogueSet[dialogueIndex].characterName;
        StartCoroutine(dialogueRunner(dialogueLines[dialogueIndex], speakerNames[dialogueIndex]));
        if (isAutonomus)
        {
            while (dialogueIndex < dialogueLines.Length)
            {
                yield return new WaitForSeconds(autonomusDuration);
                UpdateDialogueText();
            }
        }
        
    }
    public void UpdateDialogueText()
    {
        if (dialogueIndex < dialogueLines.Length - 1)
        {
            if (brokenSentence == string.Empty)
            {
                dialogueIndex++;
                if (dialogueIndex >= dialogueLines.Length)
                {
                    Debug.LogWarning("Dialogue index exceeds the length of dialogue lines. Resetting dialogue.");
                    return;
                }
                personName.text = currentDialogueSet[dialogueIndex].characterName;
                DialogueProcessor.instance.StartIDP();

                if (DialogueProcessor.instance.isConversationActive)
                {
                    if (currentDialogueSet[dialogueIndex].characterName == "Narrator")
                    {
                        DialogueProcessor.instance.person1turn = false;
                        DialogueProcessor.instance.person2turn = false;
                    }
                    DialogueProcessor.instance.person1turn = !DialogueProcessor.instance.person1turn;
                    DialogueProcessor.instance.person2turn = !DialogueProcessor.instance.person2turn;

                }
            }
        }
        else
        {
             Debug.Log("Dialogue ended, resetting dialogue box.");
            if (dialogueBox == null)
            {
                Debug.LogError("Dialogue box is not assigned in the inspector.");
                return;
            }
            GameManagerRPG.instance.isCutsceneActive = false;
            GameManagerRPG.instance.playerpg.isMovable = true;
            DialogueProcessor.instance.isConversationActive = false;
            if (currentNPC != null)
            {
                currentNPC.isInDialogue = false;
                currentNPC.facingDir = 0;
                currentNPC = null;
            }
            UIManagerRPG.instance.ControlRPGUIElements(false);
            dialogueLines = new string[0];
            isDialogueActive = false;
            rpgText.text = string.Empty;
            dialogueBounds = originalDialogueBounds;
            dialogueIndex = 0;
            if (!GameManagerRPG.instance.movingAutonomously && !GameManagerRPG.instance.isInBattle)
            {
                GameManager.instance.DialogueProgression++;
            }

            DialogueProcessor.instance.DialogueProgressionFunction();

            Debug.Log("Dialogue ended.");
            dialogueBox.SetActive(false);
            return;
        }
        StopCoroutine(dialogueRunner(dialogueLines[dialogueIndex - 1], speakerNames[dialogueIndex - 1]));
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
        StopAllCoroutines();
        isDialogueActive = false;
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
        isDialogueActive = false;
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
        isDialogueActive = false;
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
    public void ContinueDialogue()
    {
        if (dialogueIndex < dialogueLines.Length - 1 && dialogueBox.activeSelf)
        {
            StartCoroutine(dialogueRunner(dialogueLines[dialogueIndex], speakerNames[dialogueIndex]));
        }
        else
        {
            Debug.Log("Dialogue has reached the end or dialogue box is inactive.");
        }
    }
    #endregion



}
