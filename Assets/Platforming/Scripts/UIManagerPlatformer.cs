using UnityEngine;
using TMPro;
using System.Collections;

public class UIManagerPlatformer : MonoBehaviour
{
    public static UIManagerPlatformer instance;
    public GameObject abdurahmanFaceUI;
    public GameObject coinsUI;
    public GameObject livesUI;
    
    public TextMeshProUGUI coinsText;
    public TextMeshProUGUI livesText;

    [Header("Victory Screen")]
    public GameObject victoryScreen;
    public TextMeshProUGUI levelNameText;
    public TextMeshProUGUI coinCountText;
    public float waitForCoinCount = 1f;
    public int coinCount = 0;
    public bool finishedCounting = false;
    public startHealthScriptt healthScript;

    void Awake()
    {
        instance = this;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        abdurahmanFaceUI.SetActive(false);
        coinsUI.SetActive(false);
        coinsText = coinsUI?.GetComponentInChildren<TextMeshProUGUI>();
        livesUI.SetActive(false);
        livesText = livesUI?.GetComponentInChildren<TextMeshProUGUI>();


    }

    // Update is called once per frame
    void Update()
    {
        coinsText.text = gameManagerPlatformer.instance.coinNumbers.ToString();
        livesText.text = gameManagerPlatformer.instance.playerLives.ToString();
        if (gameManagerPlatformer.instance.player.whoosh)
        {
            StartCoroutine(SetVictoryScreenActive(true, 3f));
        }
    }
    public void SetUIElementsActive(bool setActiveBool)
    {
        abdurahmanFaceUI.SetActive(setActiveBool);
        coinsUI.SetActive(setActiveBool);
        livesUI.SetActive(setActiveBool);
    }
    public IEnumerator SetVictoryScreenActive(bool setActiveBool, float duration = 1f)
    {
        yield return new WaitForSeconds(duration);
        victoryScreen.SetActive(setActiveBool);
        
        if (finishedCounting || !gameManagerPlatformer.instance.player.whoosh)
        {
            yield break;
        }
        gameManagerPlatformer.instance.source.clip = GameManager.instance.victoryMusic;
        gameManagerPlatformer.instance.source.Play();
        levelNameText.text = gameManagerPlatformer.instance.worldName + " - Level " + gameManagerPlatformer.instance.levelNumber;
        StartCoroutine(Victory());

        if (setActiveBool)
        {
            gameManagerPlatformer.instance.levelOver = true;
            
        }
        gameManagerPlatformer.instance.player.whoosh = false;
    }
    public IEnumerator Victory()
    {
        yield return new WaitForSeconds(waitForCoinCount);
        while (coinCount < gameManagerPlatformer.instance.coinNumbers)
        {
            finishedCounting = false;
            coinCount++;
            gameManagerPlatformer.instance.soundEffectSource.PlayOneShot(gameManagerPlatformer.instance.coinSound);
            coinCountText.text = coinCount.ToString();
            yield return new WaitForSeconds(0.05f);
        }
        coinCountText.text = coinCount.ToString();
        gameManagerPlatformer.instance.coinNumbers = 0;
        GameManager.instance.playerCoins += gameManagerPlatformer.instance.coinNumbers;
        finishedCounting = true;
    }
}
