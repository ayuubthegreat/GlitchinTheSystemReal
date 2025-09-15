using UnityEngine;

public class battleStats : MonoBehaviour
{
    public int health = 10;
    public int originalHealth = 10;
    public int level = 1;
    public int attack = 1;
    public int defense = 1;
    public int speed = 1;
    public bool isFallenMuslim = false;
    public bool isGirlorBoy = false;
    public int experiencePointsGained = 10;
    public int coinsGained = 5; // Coins gained from defeating this enemy

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<playerpg>())
        {
            GameManagerRPG.instance.BeginBattle(gameObject);
        }
    }
    
}
