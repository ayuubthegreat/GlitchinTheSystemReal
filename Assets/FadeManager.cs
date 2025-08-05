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
                fadeImage = null;
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
        fading = true;
        faderImage.color = new Color(0, 0, 0, fader ? 1 : 0);
        instance.targetColor = new Color(0, 0, 0, fader ? 0 : 1);
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
        StopAllCoroutines();
        Debug.Log("Fading started with duration: " + duration + ", speed: " + speed + ", loadScene: " + loadScene + ", sceneName: " + sceneName);
        StartCoroutine(instance.Fading(duration, speed, loadScene, sceneName, desiredFunction));


    }
}
