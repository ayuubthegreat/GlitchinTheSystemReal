using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LogoManager : MonoBehaviour
{
    public int spriteLogoNum = 0;
    public Sprite[] availableLogos;
    public Image image;
    public int changeDuration = 2;
    public int productionLogoLimit = 2;
    public MenuLoader mainMenu;
    public AudioSource mainSource;
    public bool endLogos;
    public int fadeSpeed = 3;
    void Start()
    {
        mainSource = Camera.main.GetComponent<AudioSource>();
        image = GetComponent<Image>();
        image.sprite = availableLogos[0];
        StartCoroutine(ChangeLogo(changeDuration));
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Escape key pressed, quitting application.");
            Application.Quit();
            
        }
        if (Input.GetKeyDown(KeyCode.Return) && !endLogos)
        {
            Debug.Log("Return key pressed, skipping logos.");
            StopCoroutine(ChangeLogo(changeDuration));
            UIManager.instance.fadeableGeneralObjects[0].Fader(true);
            DisplayMainMenu();
        }
    }
    public IEnumerator ChangeLogo(int duration)
    {
        
        while (spriteLogoNum <= availableLogos.Length)
        {
            yield return new WaitForSeconds(spriteLogoNum < availableLogos.Length - 1 ? duration * 2 : duration);
            UIManager.instance.fadeableGeneralObjects[0].StartFading(duration, fadeSpeed, false, "", spriteLogoNum < availableLogos.Length - 1 ? IncreaseIndex : DisplayMainMenu);
        }
        Debug.Log("All logos displayed, ending logo sequence.");
    }
    public void IncreaseIndex()
    {
        if (spriteLogoNum < availableLogos.Length - 1)
        {
            spriteLogoNum++;
        }
        else
        {
            Debug.LogWarning("No more logos available to change to. Resetting index.");
            spriteLogoNum = availableLogos.Length;
            return; // Exit if no more logos are available
        }
        Debug.Log("New logo number is:" + spriteLogoNum);
        image.sprite = availableLogos[spriteLogoNum];
    }
    public void DisplayMainMenu()
    {
        endLogos = true;
        spriteLogoNum = 0; // Reset the logo index
        image.sprite = availableLogos[0]; // Reset the image to the first logo
        Debug.Log("Displaying main menu.");
        gameObject.SetActive(false);
        mainMenu.gameObject.SetActive(true);  
        mainSource.clip = UIManager.instance.mainMenuMusic;
        mainSource.Play();
        
    }
}
