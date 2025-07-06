using UnityEngine;
using UnityEngine.UI;

public class MainMenuLoader : fadebase
{
    public Button button;
    public bool buttonFaderStart;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        button = GetComponentInChildren<Button>();
        buttonFaderStart = false;
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetBool("buttonFaderStart", buttonFaderStart);
    }
    public void FadeButton()
    {
        buttonFaderStart = true;
    }
}
