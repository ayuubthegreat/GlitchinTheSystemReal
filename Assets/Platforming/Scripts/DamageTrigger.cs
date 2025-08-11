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
            gmp = FindObjectOfType<gameManagerPlatformer>();
        }
        boxCollider = GetComponent<BoxCollider2D>();
        healthScript = FindFirstObjectByType<startHealthScriptt>();
    }
    public void OnTriggerStay2D(Collider2D collision)
    {
        player player = collision.gameObject.GetComponent<player>();
        if (player != null)
        {
            if (!gmp.canBeHit)
            {
                return;
            }
            if (player.playerHealth != 0)
            {
                gmp.StartCooldownforHits(coolDownPeriod);
                healthScript.SetDestroyIndividualHealth(player.playerHealth);
                player.playerHealth--;
                Debug.Log("Player Health: " + player.playerHealth);
                player.Knockback(transform.position.x);


            }
            else
            {
                gmp.warningScreenandTeleport(5);
            }


        }


    }
    

}

