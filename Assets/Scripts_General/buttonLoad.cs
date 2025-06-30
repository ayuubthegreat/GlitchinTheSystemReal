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
    Phone
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
        switch (buttonType)
        {
            case ButtonType.SaveFile:
                if (SaveFileFound)
                    SceneManager.LoadScene(sceneName);
                else
                    warningScreen.SetActive(true);
                break;
            case ButtonType.Options:
                optionsScreen.SetActive(true);
                break;
            case ButtonType.Start:
                saveFileScreen.SetActive(true);
                break;
            case ButtonType.Back:
                saveFileScreen.SetActive(false);
                optionsScreen.SetActive(false);
                break;
            case ButtonType.Warning:
                warningScreen.SetActive(false);
                break;
            case ButtonType.Next:
                DialogueManager.instance.dialogueNumber++;
                break;
            case ButtonType.Phone:

                HandlePhoneInteraction();
                break;
            default:
                Debug.Log("What are you again? I forgot.");
                break;
        }
    }
    public void HandlePhoneInteraction() {
      playerpg playerpg = GameManager.instance.playerpg;
        if (playerpg != null)
        {
            bool DialogueProgress = DialogueManager.instance.DialogueProgression < 3 && DialogueManager.instance.DialogueProgression > 0;
            if (!DialogueProcessor.instance.isConversationActive && !DialogueProcessor.instance.isPhoneActive && DialogueProgress)
            {
                playerpg.isMovable = false;
                DialogueProcessor.instance.isPhoneActive = true;
                Debug.Log("Player is interacting with the phone.");
                DialogueManager.instance.DialogueProgression = 2;
                DialogueProcessor.instance.DialogueProgressionFunction();
                
            }
            
        }
    }
    
}
