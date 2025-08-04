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
            }
            
            if (fadeImage.color.a == 0 && fadeImage.color.a == targetColor.a && fadeImage.gameObject.activeSelf)
            {
                fading = false;
                fadeImage.gameObject.SetActive(false);
            }
            if (loadScene)
            {
                fading = false;
                SceneManager.LoadScene(sceneName);
                loadScene = false;
            }

            
        }
        


    }
    public void Fader(bool fader)
    {
        instance.fadeImage.color = new Color(0, 0, 0, fader ? 1 : 0);
        instance.targetColor = new Color(0, 0, 0, fader ? 0 : 1);
        instance.fading = true;
    }
    public IEnumerator Fading(float duration = 1f, float speed = .1f, bool loadScene = false, string sceneName = "", Action desiredFunction = null)
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
    public void StartFading(float duration = 3f, float speed = .1f, bool loadScene = false, string sceneName = "", Action desiredFunction = null)
    {
        if (instance == null)
        {
            Debug.LogError("FadeManager instance not found in the scene.");
            return;
        }
        StopAllCoroutines();
        Debug.Log("Fading started with duration: " + duration + ", speed: " + speed + ", loadScene: " + loadScene + ", sceneName: " + sceneName);
        instance.gameObject.SetActive(true);
        instance.fadeImage.gameObject.SetActive(true);
        instance.StartCoroutine(instance.Fading(duration, speed, loadScene, sceneName, desiredFunction));
    }
    
   
}
