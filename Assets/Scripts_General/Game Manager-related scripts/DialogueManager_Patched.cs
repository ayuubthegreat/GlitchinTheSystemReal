
using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;
    public TextMeshProUGUI rpgText;
    public int dialogueBounds;
    public string brokenSentence;
    public bool isDialogueActive = false;
    public float dialogueSpeed = .3f;
    void Awake()
    {
        if (instance != null)
        {
            instance = this;
        }
    }
    void Start()
    {
        StartCoroutine(dialogueRunner("kkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkk"));
    }
    void Update()
    {

    }
    public IEnumerator dialogueRunner(string sentence, bool killDialogueScreen = false)
    {
        Debug.Log("Dialogue started.");
        for (int i = 0; i <= (sentence.Length <= dialogueBounds ? dialogueBounds : sentence.Length); i++)
        {
            isDialogueActive = true;
            rpgText.text = sentence.Substring(0, i);
            yield return new WaitForSeconds(dialogueSpeed);
            if (i == dialogueBounds && sentence.Length > dialogueBounds)
            {
                brokenSentence = sentence.Substring(i, i + dialogueBounds < sentence.Length - i ? dialogueBounds : sentence.Length - i);
                Debug.Log("Breaking sentence: " + brokenSentence + " at index: " + i + " with length: " + brokenSentence.Length + " The original sentence's length was " + sentence.Length);
                break;
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
    

    
}
