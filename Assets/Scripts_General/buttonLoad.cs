using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class buttonLoad : MonoBehaviour
{
  public Button myButton;
  public GameObject saveFileScreen;
  public GameObject optionsScreen;
  public GameObject warningScreen;
  public bool isStartButton;
  public bool isSaveFile;
  public bool SaveFileFound;
  public bool isOptions;
  public bool isBackButton;
  public bool isWarningButton;
  public bool isNextButton;
    public string sceneName;
    public SceneManager sceneManager;
  public void Start()
  {
    myButton = GetComponent<Button>();
    
    }
  public void buttonLoader()
  {
    if (isSaveFile)
    {
      if (SaveFileFound)
      {
        SceneManager.LoadScene(sceneName);
      }
      else
      {
        warningScreen.SetActive(true);
      }
    }
    if (isOptions)
    {
      optionsScreen.SetActive(true);
    }
    if (isStartButton)
    {
      saveFileScreen.SetActive(true);
      
    }
    if (isBackButton)
    {
      saveFileScreen.SetActive(false);
      optionsScreen.SetActive(false);
    }
    if (isWarningButton)
    {
      warningScreen.SetActive(false);
    }
    if (isNextButton)
    {
      DialogueManager.instance.dialogueNumber++;
      
    }
    else
    {
      Debug.Log("What are you again? I forgot.");
    }

  } 
}
