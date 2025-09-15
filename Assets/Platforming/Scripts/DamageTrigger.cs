using UnityEngine;



public class DamageTrigger : MonoBehaviour
{
    public gameManagerPlatformer gmp;
    public BoxCollider2D boxCollider;
    public float coolDownPeriod = 2f;
    public startHealthScriptt healthScript;
    public void Start()
    {
        if (gmp == null)
        {
            gmp = FindFirstObjectByType<gameManagerPlatformer>();
        }
        boxCollider = GetComponent<BoxCollider2D>();
        healthScript = FindFirstObjectByType<startHealthScriptt>();
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        gameManagerPlatformer.instance.player.Ouch(collision, healthScript);
    }
    

}

