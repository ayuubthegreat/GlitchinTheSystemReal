using UnityEngine;

public class battleStats : MonoBehaviour
{
    public int health = 10;
    public int level = 1;
    public int attack = 5;
    public int defense = 5;
    public int speed = 5;
    public bool isFallenMuslim = false;
    public bool isGirlorBoy = false;

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<playerpg>())
        {
            GameManagerRPG.instance.BeginBattle();
        }
    }
    
}
