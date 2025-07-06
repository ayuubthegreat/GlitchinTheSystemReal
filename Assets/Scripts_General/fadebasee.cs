using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


public class fadebase : MonoBehaviour
{
    
    
    public float moveDuration = 2f;
    public float logoDuration = 2f;
    public float logoNumLimit = 4;
    public Animator anim;
    public string sceneName;


    public void Start()
    {
        anim = GetComponent<Animator>();
    }
    public IEnumerator ChangeTransitionBoolTimer()
    {
        if (MainScreens.mainMenu == UIManager.instance.currentScreen && UIManager.instance.logoTransitions >= logoNumLimit)
        {
            Destroy(gameObject);

        }
        
        UIManager.instance.canTransition = false;
        yield return new WaitForSeconds(moveDuration);
        UIManager.instance.canTransition = true;
    }
    
    public void ChangeTransitionBool(int value)
    {
        if (value == 1)
        {
            UIManager.instance.canTransition = true;
        }
        else
        {
            UIManager.instance.canTransition = false;
            UIManager.instance.startTransitions[2] = false;
        }
    }
    // public void StartTransitionCoroutine() => StartCoroutine(ChangeStartTransitionFaderBool());
    // public IEnumerator ChangeStartTransitionFaderBool() {
    //     yield return new WaitForSeconds(logoDuration);
    //     UIManager.instance.StartChangeTransitionBools();

    //     }
    public void LogoDurations()
    {
        if (UIManager.instance.currentScreen == MainScreens.mainMenu && UIManager.instance.logoTransitions < logoNumLimit)
        {
            UIManager.instance.logoTransitions++;
        }
        else
        {
            
            return;
        }
    }
    public void SceneLoader()
    {
        SceneManager.LoadScene(sceneName);
    }

    public void Update()
    {
        anim.SetBool("canMove", UIManager.instance.canTransition);
        anim.SetBool("canStartMoving", UIManager.instance.startTransitions[2]);
        anim.SetBool("startButton", UIManager.instance.MainMenuTransitions[0]);
        anim.SetBool("saveFileFound", UIManager.instance.MainMenuTransitions[1]);
        anim.SetBool("warningScreen", UIManager.instance.MainMenuTransitions[2]);

    }


}

    
   

