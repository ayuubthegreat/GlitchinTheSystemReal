using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum ButtonType
{
    None,
    Start,
    SaveFile,
    Options,
    Back,
    Warning,
    Next,
    Phone,
    Bool,

}

public class buttonLoad : MonoBehaviour
{
    public Button myButton;
    public GameObject saveFileScreen;
    public GameObject optionsScreen;
    public GameObject warningScreen;
    public bool SaveFileFound;
    public string sceneName;
    public SceneManager sceneManager;
    public ButtonType buttonType = ButtonType.None;

    public void Start()
    {
        myButton = GetComponent<Button>();
    }

    public void buttonLoader()
    {
        for (int i = 0; i < UIManager.instance.MainMenuTransitions.Length; i++)
        {
            UIManager.instance.MainMenuTransitions[i] = false;
        }
        switch (buttonType)
        {
            case ButtonType.SaveFile:
                if (SaveFileFound)
                    UIManager.instance.MainMenuTransitions[1] = true;
                else
                    UIManager.instance.MainMenuTransitions[2] = true;
                break;
            case ButtonType.Options:
                optionsScreen.SetActive(true);
                break;
            case ButtonType.Start:
                UIManager.instance.MainMenuTransitions[0] = true;
                break;
            case ButtonType.Back:
                saveFileScreen.SetActive(false);
                optionsScreen.SetActive(false);
                break;
            case ButtonType.Warning:
                warningScreen.SetActive(false);
                break;
            case ButtonType.Next:
                Debug.Log(DialogueManager.instance.brokenSentence);
                if (DialogueManager.instance.brokenSentence != string.Empty)
                {
                    DialogueManager.instance.StartDialogueController();

                }
                else
                {
                    OnWithTheShow();
                }
                break;



            case ButtonType.Phone:

                HandlePhoneInteraction();
                break;
            default:
                Debug.Log("What are you again? I forgot.");
                break;
        }
    }
    public void HandlePhoneInteraction()
    {
        playerpg playerpg = GameManager.instance.playerpg;
        if (playerpg != null)
        {
            bool DialogueProgress = GameManager.instance.DialogueProgression < 3 && GameManager.instance.DialogueProgression > 0;
            if (!DialogueProcessor.instance.isConversationActive && !DialogueProcessor.instance.isPhoneActive && DialogueProgress)
            {
                playerpg.isMovable = false;
                DialogueProcessor.instance.isPhoneActive = true;
                Debug.Log("Player is interacting with the phone.");
                GameManager.instance.DialogueProgression = 2;
                DialogueProcessor.instance.DialogueProgressionFunction();

            }

        }
    }
    public void OnWithTheShow()
    {
        DialogueManager.instance.currentPage = 1;
        DialogueManager.instance.rpgText.pageToDisplay = DialogueManager.instance.currentPage;
        if (DialogueManager.instance.dialogueNumber < DialogueManager.instance.endDialogueRange - 1)
        {
            DialogueManager.instance.dialogueNumber++;
            DialogueManager.instance.StartDialogueController();

        }
        else
        {
            DialogueManager.instance.isEnabled = false;
            DialogueManager.instance.ResetDialogue();
        }
        
    }
    
}
