
using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;
    public TextMeshProUGUI rpgText;
    public GameObject dialogueBox;
    public int dialogueBounds;
    public string brokenSentence;
    public int dialogueIndex = 0;
    public bool isDialogueActive = false;
    public float dialogueSpeed = .3f;
    public string[] dialogueLines;
    public string testLine;
    void Awake()
    {
        if (instance != null)
        {
            instance = this;
        }
    }
    void Start()
    {
        StartDialogueTexts(DialogueVault.instance.dialogueSets[0], 0, 1);
    }
    void Update()
    {

    }
    public IEnumerator dialogueRunner(string sentence, bool killDialogueScreen = false)
    {
        Debug.Log("Dialogue started.");
        rpgText.text = "";
        if (brokenSentence != string.Empty)
        {
            sentence = brokenSentence;
            brokenSentence = string.Empty;
        }
        isDialogueActive = true;
        for (int i = 0; i <= (sentence.Length <= dialogueBounds ? dialogueBounds : sentence.Length); i++)
        {
            
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            {
                i = sentence.Length > dialogueBounds ? dialogueBounds : sentence.Length; 
            }
            rpgText.text = sentence.Substring(0, i);
            if (CheckForSigns(sentence[i]))
            {
                Debug.Log("Sign detected: " + sentence[i] + " at index: " + i);
                yield return new WaitForSeconds(dialogueSpeed * 2);
            }
            else
            {
              yield return new WaitForSeconds(dialogueSpeed);  
            }
            if (i == dialogueBounds || i == sentence.Length)
            {
                if (sentence.Length > dialogueBounds)
                {
                    brokenSentence = sentence.Substring(i, i + dialogueBounds < sentence.Length - i ? dialogueBounds : sentence.Length - i);
                    Debug.Log("Breaking sentence: " + brokenSentence + " at index: " + i + " with length: " + brokenSentence.Length + " The original sentence's length was " + sentence.Length);
                    break;
                }
            }
            else
            {
                Debug.Log("Current dialogue text: " + rpgText.text);
            }
        }
        isDialogueActive = false;

    }
    public bool CheckForSigns(char sign)
    {
        switch (sign)
        {
            case '.':
            case ',':
            case '?':
            case '!':
                return true;
            default:
                Debug.Log("Unknown sign detected: " + sign);
                return false;
        }
    }
    public void StartDialogueTexts(DialogueVault.DialogueSet[] dialogueSets, int start, int end = 0)
    {
        if (end == 0)
        {
            end = dialogueSets.Length;
        }
        dialogueLines = new string[end - start + 1];
        for (int i = start; i <= end; i++)
        {
            dialogueLines[i - start] = dialogueSets[i].dialogueLine;
        }
        dialogueBox.SetActive(true);
        dialogueIndex = 0;
        StartCoroutine(dialogueRunner(dialogueLines[dialogueIndex]));
    }
    public void UpdateDialogueText()
    {
        if (dialogueIndex >= dialogueLines.Length - 1)
        {
            dialogueBox.SetActive(false);
            dialogueLines = new string[0];
            isDialogueActive = false;
            Debug.Log("Dialogue ended.");
            return;
        }
        if (brokenSentence == string.Empty)
        {
            dialogueIndex++;
        }
        StopAllCoroutines();
        StartCoroutine(dialogueRunner(dialogueLines[dialogueIndex]));
    }
    

    
}
