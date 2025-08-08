using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FadeManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public static FadeManager instance;
    public bool fadeInOnStart = true;
    public bool fading = false;
    public Image fadeImage;
    public float fadeDuration = 1f;
    public float fadeSpeed = 3f;
    public Color targetColor;
    public bool loadScene = false;
    public string sceneName = "";
    public float targetAlpha = 0f;
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
        fadeImage = GetComponent<Image>();
        if (fadeInOnStart)
        {
            fadeImage.color = new Color(0, 0, 0, 1);
            Fader(true);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (fadeImage.color != targetColor)
        {
            fadeImage.color = Color.Lerp(fadeImage.color, targetColor, Time.deltaTime / fadeSpeed);
            targetAlpha = fadeImage.color.a;
        }
        else
        {
            if (Vector4.Distance(fadeImage.color, targetColor) < 0.01f)
            {
                fadeImage.color = targetColor;
                fadeImage = GetComponent<Image>();
                fading = false;
            }
            if (loadScene)
            {
                fading = false;
                SceneManager.LoadScene(sceneName);
                loadScene = false;
            }


        }



    }
    public void Fader(bool fader, Sprite imageSprite = null, Image faderImage = null)
    {
        if (faderImage == null)
        {
            faderImage = GetComponent<Image>();
        }
        
        fadeImage = faderImage;
        bool isOriginalImage = (fadeImage == GetComponent<Image>());
        fading = true;
        if (isOriginalImage)
        {
            faderImage.color = new Color(0, 0, 0, fader ? 1 : 0);
            instance.targetColor = new Color(0, 0, 0, fader ? 0 : 1);
        } 
        else
        {
            if (imageSprite == null)
            {
                Debug.LogError("Image sprite is null. Please provide a valid sprite for the fader image.");
                return;
            }
            faderImage.color = new Color(1, 1, 1, fader ? 1 : 0);
            instance.targetColor = new Color(1, 1, 1, fader ? 0 : 1);
        }
        
        instance.fading = true;
    }
    public IEnumerator Fading(float duration = 1f, float speed = .1f, bool loadScene = false, string sceneName = "", Action desiredFunction = null, Sprite imageSprite = null)
    {
        this.loadScene = loadScene;
        this.sceneName = sceneName;
        fadeDuration = duration;
        fadeSpeed = speed;

        Fader(false);
        yield return new WaitForSeconds(fadeDuration);
        if (desiredFunction != null)
        {
            Debug.Log("Invoking desired function after fade out."); 
        }
        desiredFunction?.Invoke();
        Fader(true);
    }
    public void StartFading(float duration = 3f, float speed = .1f, bool loadScene = false, string sceneName = "", Action desiredFunction = null, Sprite imageSprite = null)
    {
        if (instance == null)
        {
            Debug.LogError("FadeManager instance not found in the scene.");
            return;
        }
        Debug.Log("Fading started with duration: " + duration + ", speed: " + speed + ", loadScene: " + loadScene + ", sceneName: " + sceneName);
        StartCoroutine(instance.Fading(duration, speed, loadScene, sceneName, desiredFunction));


    }
}
