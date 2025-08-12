using System.Collections;
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
        player player = collision.gameObject.GetComponent<player>();
        if (player != null)
        {

            if (!gameManagerPlatformer.instance.canBeHit) return;
            if (player.playerHealth != 0)
            {
                gameManagerPlatformer.instance.StartCooldownforHits(coolDownPeriod);
                healthScript.SetDestroyIndividualHealth(player.playerHealth);
                player.playerHealth--;
                Debug.Log("Player Health: " + player.playerHealth);
                player.Knockback(transform.position.x);


            }
            else
            {
                gameManagerPlatformer.instance.warningScreenandTeleport(5);
            }


        }


    }
    

}

