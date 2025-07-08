
using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEditorInternal;
using System.Linq;
public enum MainScreens{
RPG,
Platforming,
mainMenu,
}
public class UIManager : MonoBehaviour
{

    public static UIManager instance;
    [Header("Audio Source(s)")]
    public AudioSource source;
    [Header("Audio")]
    public AudioClip mainMenuMusic;
    [Header("Texts")]
    public TextMeshProUGUI livesText;
    public TextMeshProUGUI coinsText;
    public TextMeshProUGUI warningText;
    [Header("Fade-related Material")]
    public string location;
    public float fadeNums;
    public bool canTransition;
    public bool startFader;
    public int fadeNumPeriod;
    public bool[] startTransitions;
    public int logoTransitions = 0;
    [Header("Screens")]
    public GameObject coinsScreen;
    public GameObject livesScreen;
    public GameObject abdurahmanHealthScreen;
    public GameObject phone;
    public GameObject dialogueScreen;
    public GameObject mainMenu;

    public GameObject fade;

    [Header("Player States")]
    public bool start = false;
    public bool isDead = false;
    public bool isAlive = true;
    [Header("Miscellanous States")]
    public bool logosAreDone = false;
    public bool startButton;
    public bool filefound;
    public bool[] MainMenuTransitions;
    public string streetName;


    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        if (coinsScreen == null)
            coinsScreen = GameObject.Find("coinUI");

        if (livesScreen == null)
            livesScreen = GameObject.Find("livesUI");
        if (abdurahmanHealthScreen == null)
            abdurahmanHealthScreen = GameObject.Find("abdurahmanplayerhealth");



    }

    void Start()
    {
        if (GameManager.instance != null && GameManager.instance.player != null && fade != null)
        {
            fade.transform.position = GameManager.instance.player.transform.position;
        }
        fadeNums = 2;
        location = "Abdurahman's House";
        MainMenuTransitions = new bool[] {
            // Start Button Bool
            false,
            // Save File Found
            false,
            // Warning Screen Enabled
            false,
        };
        startTransitions = new bool[] {
            // Start of RPG Text Box
            false,
            // Start of Person Name Text
            false,
            // Start of Location Announcer
            false,
        };

        CheckForGameState();
        ScreenControls();
        StartCoroutine(ChangeTransitionBools());
        source.clip = null;
    }

    void Update()
    {
        CheckForGameState();

        if (MainScreens.Platforming == currentScreen)
        {
            if (coinsText != null)
                coinsText.text = GameManager.instance.player.coinNumbers.ToString();
            if (livesText != null)
                livesText.text = GameManager.instance.playerLives.ToString();
        }
    }

    public void ScreenControls()
    {

        if (currentScreen == MainScreens.Platforming)
        {
            if (coinsScreen != null) coinsScreen.SetActive(true);
            if (livesScreen != null) livesScreen.SetActive(true);
            if (abdurahmanHealthScreen != null) abdurahmanHealthScreen.SetActive(true);
            Debug.Log("The Screen Controller works.");
        }
        else if (currentScreen == MainScreens.RPG)
        {
            phone.SetActive(true);
        }
        else if (currentScreen == MainScreens.mainMenu)
        {
            phone.SetActive(false);
            dialogueScreen.SetActive(false);
        }
        else
        {

        }

    }

    public void FadeController(int seconds)
    {
        Debug.Log("This parent fade function was called.");
        StartCoroutine(Fade(seconds));
    }

    public IEnumerator Fade(int seconds)
    {
        fadeNums = 1;

        if (fade == null || GameManager.instance?.camera == null)
        {
            Debug.Log("Fade or camera is null.");
            yield break;
        }

        GameObject fadeInObject = Instantiate(fade, GameManager.instance.camera.transform.position, Quaternion.identity);

        if (fadeInObject != null)
        {
            Debug.Log("This worked.");
        }
        else
        {
            Debug.Log("This doesn't work.");
        }

        yield return new WaitForSeconds(seconds);

        if (GameManager.instance.player != null)
        {
            fadeInObject.transform.position = GameManager.instance.camera.transform.position;
            fadeNums = 2;
        }
        else
        {
            Debug.Log("This fader is broken.");
        }
    }

    public IEnumerator TimetoDie(float duration)
    {
        Debug.Log("This function was called.");

        

        if (GameManager.instance != null)
        {
            GameManager.instance.isDonewithPlatforming = true;
            GameManager.instance.startSpawnBoolPlatforming = true;
        }

        yield return new WaitForSeconds(duration);

        SceneManager.LoadScene("ManicMinnesotaRPGScene");
    }
    public void SetStartBool()
    {
        start = true;
    }
    public void SetIsDeadandIsAliveBool(bool isDeadinner)
    {
        isDead = isDeadinner;
        isAlive = !isDeadinner;
    }
    public void SetStartBool(int value) => start = value == 1 ? true : false;
    public void SetCanTransitionBool(bool value)
    {
        canTransition = value;
        if (canTransition)
        {
            Debug.Log("Transitioning is enabled.");
        }
        else
        {
            Debug.Log("Transitioning is disabled.");
        }
    }
    public void SetLocation(string newLocation) => location = newLocation;
    public void ChangeStartTransitionsBool(int initialValue)
    {
        startTransitions[initialValue] = true;
    }
    public void ChangeStartTransitionsBoolArray(int initialValue, bool boolValue)
    {
        for (int i = 0; i < startTransitions.Length; i++)
        {
            if (initialValue == i)
            {
                startTransitions[i] = boolValue;
            }
        }
    }
    public IEnumerator ChangeTransitionBools()
    {


        startTransitions[2] = true;
        canTransition = true;
        yield return new WaitForSeconds(.3f);
        startTransitions[2] = false;
        yield break;



    }
    public void StartChangeTransitionBools() => StartCoroutine(ChangeTransitionBools());
    public MainScreens currentScreen;

    public void CheckForGameState()
    {
        if (GameManager.instance.player == null && GameManager.instance.playerpg != null)
        {
            currentScreen = MainScreens.RPG;
            phone = FindAnyObjectByType<PlayerPhonePhysical>().gameObject;
        }
        else if (GameManager.instance.player != null && GameManager.instance.playerpg == null)
        {
            currentScreen = MainScreens.Platforming;
            coinsScreen = GameObject.Find("coinsScreen");
            livesScreen = GameObject.Find("livesScreen");
            abdurahmanHealthScreen = GameObject.Find("abdurahmanHealthScreen");
            coinsScreen.SetActive(true);
            livesScreen.SetActive(true);
            abdurahmanHealthScreen.SetActive(true);
        }
        else if (GameManager.instance.player == null && GameManager.instance.playerpg == null)
        {
            currentScreen = MainScreens.mainMenu;
            
        }
    }
    public void toPlayMainMenu() => StartCoroutine(PlayMainMenu());
    public IEnumerator PlayMainMenu()
    {
        GameObject logoScreen = GameObject.Find("LogoImages");
        source.clip = mainMenuMusic;
        source.Play();
        yield return new WaitForSeconds(2);
        
        logoScreen.SetActive(false);
        mainMenu.SetActive(true);
        yield return new WaitForSeconds(3);
        StartChangeTransitionBools();
    }
    
}
