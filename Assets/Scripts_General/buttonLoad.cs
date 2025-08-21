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
    SceneLoader,
    Settings,

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
        UIManager.instance.ChangeValueOfEntireArray(UIManager.instance.MainMenuTransitions, false);
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
                
                UIManager.instance.MainMenuTransitions[3] = true;
                break;
            case ButtonType.Warning:
                warningScreen.SetActive(false);
                break;
            case ButtonType.Next:
                
                break;



            case ButtonType.Phone:

                HandlePhoneInteraction();
                break;
            case ButtonType.SceneLoader:
            Camera.main.GetComponent<AudioSource>().Stop();
             FadeManager.instance.StartFading(2f, .1f, true, sceneName);
                break;
            case ButtonType.Settings:
                MenuLoader.instance.MoveToNewPosition(200f);
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
    
    
    
}
