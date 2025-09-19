using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class fader : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public bool fadeInOnStart = true;
    public bool fading = false;
    public Image fadeImage;
    public float fadeDuration = 1f;
    public float fadeSpeed = 3f;
    public Color targetColor;
    public bool loadScene = false;
    public string sceneName = "";
    public float targetAlpha = 0f;
    
    void Start()
    {
        fadeImage = GetComponent<Image>();
        if (fadeInOnStart)
        {
            fadeImage.color = new Color(0, 0, 0, 1);
            StartCoroutine(Fader(true));
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (fadeImage.color != targetColor && fading)
        {
            fadeImage.color = Vector4.MoveTowards(fadeImage.color, targetColor, Time.unscaledDeltaTime * fadeSpeed);
            targetAlpha = fadeImage.color.a;
        }
        else
        {
            if (Vector4.Distance(fadeImage.color, targetColor) < 0.01f)
            {
                fadeImage.color = targetColor;
                targetAlpha = fadeImage.color.a;
                fading = false;
            }
            


        }



    }
    public IEnumerator Fader(bool fader, Sprite imageSprite = null, float duration = 1f, float speed = 1f)
    {
        float time = 0f;
        if (imageSprite != fadeImage.sprite)
        {
            fadeImage.sprite = imageSprite;
        }
        int imageOrNoImage = fadeImage.sprite != null ? 1 : 0;
        float transitionalAlpha;
        int targetAlpha = fader ? 0 : 1;
        targetColor = new Color(imageOrNoImage, imageOrNoImage, imageOrNoImage, targetAlpha);
        while (time < duration)
        {
            time += Time.unscaledDeltaTime * speed;
            transitionalAlpha = Mathf.Lerp(fadeImage.color.a, targetAlpha, time / duration);
            fadeImage.color = new Color(imageOrNoImage, imageOrNoImage, imageOrNoImage, transitionalAlpha);
            yield return null;
        }
        fadeImage.color = new Color(imageOrNoImage, imageOrNoImage, imageOrNoImage, targetAlpha);
        
    }
    public IEnumerator Fading(float duration = 1f, float speed = 10f, bool loadScene = false, string sceneName = "", Action desiredFunction = null)
    {
        this.loadScene = loadScene;
        this.sceneName = sceneName;
        fadeDuration = duration;
        fadeSpeed = speed;
        yield return StartCoroutine(Fader(false));
        yield return new WaitForSecondsRealtime(fadeDuration);
        if (desiredFunction != null)
        {
            Debug.Log("Invoking desired function after fade out."); 
        }
        if (loadScene)
        {
        fading = false;
        loadScene = false;
        SceneManager.LoadScene(sceneName);
                
        }
        desiredFunction?.Invoke();
        yield return new WaitForSecondsRealtime(0.1f); // Small delay to ensure the function has time to execute before fading back in
        StartCoroutine(Fader(true));
    }
    public void StartFading(float duration = 3f, float speed = 10f, bool loadScene = false, string sceneName = "", Action desiredFunction = null)
    {
        Debug.Log("Fading started with duration: " + duration + ", speed: " + speed + ", loadScene: " + loadScene + ", sceneName: " + sceneName);
        StopAllCoroutines();
        StartCoroutine(Fading(duration, speed, loadScene, sceneName, desiredFunction));
    }
}
