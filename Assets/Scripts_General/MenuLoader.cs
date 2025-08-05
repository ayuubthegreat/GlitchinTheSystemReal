using UnityEngine;
using UnityEngine.UI;

public class MenuLoader : MonoBehaviour
{
    public static MenuLoader instance;
    public Slider volumeSlider;
    public RectTransform mainMenu;
    public RectTransform settingsMenu;
    public Camera mainCamera;
    public Vector3 targetPosition;
    public float moveSpeed = 200f;
    public Animator anim;
    public bool moving = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
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

    }
    void Start()
    {
        mainCamera = Camera.main;
        targetPosition = mainMenu.anchoredPosition;
        moving = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(mainMenu.anchoredPosition, targetPosition) < 0.1f)
        {
            moving = false;
            return;
        }
        if (moving)
        {
            mainMenu.anchoredPosition = Vector2.MoveTowards(mainMenu.anchoredPosition, targetPosition, moveSpeed * Time.deltaTime);
        }



    }
    public void MoveToNewPosition(float newYPosition)
    {
        targetPosition.y = newYPosition;
        moving = true;
    }
    public void ToggleMusicVolume()
    {
        GameManager.instance.musicVolume = volumeSlider.value;
        mainCamera.GetComponent<AudioSource>().volume = GameManager.instance.musicVolume;
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
