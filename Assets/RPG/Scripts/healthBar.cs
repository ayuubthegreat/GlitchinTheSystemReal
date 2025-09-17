using UnityEngine;

public class healthBar : MonoBehaviour
{
    public float minWidthPosition;
    public float maxWidthPosition;
    public float minWidthValue;
    public float maxWidthValue;
    public int health = 20;
    public int originalHealth = 20;
    public bool isPlayerorEnemy = true; // true for player, false for enemy
    
    public RectTransform barTransform;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        barTransform = GetComponent<RectTransform>();
        maxWidthValue = barTransform.sizeDelta.x;
        maxWidthPosition = barTransform.anchoredPosition.x;
    }

    // Update is called once per frame
    void Update()
    {
        health = isPlayerorEnemy ? PartyManager.instance.partyMembers[GameManagerRPG.instance.currentAllyIndex].health : GameManagerRPG.instance.enemiesInBattle[GameManagerRPG.instance.currentEnemyIndex].GetComponent<battleStats>().health;
        originalHealth = isPlayerorEnemy ? PartyManager.instance.partyMembers[GameManagerRPG.instance.currentAllyIndex].originalHealth : GameManagerRPG.instance.enemiesInBattle[GameManagerRPG.instance.currentEnemyIndex].GetComponent<battleStats>().originalHealth;
        float healthPercentage = health / (float)originalHealth;
        barTransform.sizeDelta = new Vector2(Mathf.Lerp(minWidthValue, maxWidthValue, healthPercentage), barTransform.sizeDelta.y);
        barTransform.anchoredPosition = new Vector2(Mathf.Lerp(minWidthPosition, maxWidthPosition, healthPercentage), barTransform.anchoredPosition.y);
    }
}
