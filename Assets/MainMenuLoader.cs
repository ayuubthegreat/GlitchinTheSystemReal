using UnityEngine;
using UnityEngine.UI;

public class MainMenuLoader : fadebase
{
    public Button button;
    public bool buttonFaderStart;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    new
    void Start()
    {
        button = GetComponentInChildren<Button>();
        buttonFaderStart = false;
    }

    // Update is called once per frame
    new
    void Update()
    {
        anim.SetBool("canMove", UIManager.instance.canTransition);
        anim.SetBool("canStartMoving", UIManager.instance.startTransitions[2]);
        anim.SetBool("startButton", UIManager.instance.MainMenuTransitions[0]);
        anim.SetBool("saveFileFound", UIManager.instance.MainMenuTransitions[1]);
        anim.SetBool("warningScreen", UIManager.instance.MainMenuTransitions[2]);
    }
    public void FadeButton()
    {
        buttonFaderStart = true;
    }
}
