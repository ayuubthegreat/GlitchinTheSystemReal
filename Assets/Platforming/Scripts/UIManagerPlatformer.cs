using UnityEngine;
using TMPro;

public class UIManagerPlatformer : MonoBehaviour
{
    public static UIManagerPlatformer instance;
    public GameObject abdurahmanFaceUI;
    public GameObject coinsUI;
    public GameObject livesUI;
    public TextMeshProUGUI coinsText;
    public TextMeshProUGUI livesText;

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
    }
    public void SetUIElementsActive(bool setActiveBool)
    {
        abdurahmanFaceUI.SetActive(setActiveBool);
        coinsUI.SetActive(setActiveBool);
        livesUI.SetActive(setActiveBool);
    }
}
