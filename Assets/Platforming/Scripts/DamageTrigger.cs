using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class DamageTrigger : MonoBehaviour
{
    public int coolDownPeriod = 2;
    
    public startHealthScriptt healthScript;
    public void Start()
    {
        healthScript = FindFirstObjectByType<startHealthScriptt>();
    }
    public void OnEnable()
    {
        if (healthScript == null)
        {
            healthScript = FindFirstObjectByType<startHealthScriptt>();
        }
    }
    public void OnTriggerStay2D(Collider2D collision)
    {
        if (!gameManagerPlatformer.instance.canBeHit)
        {
            return;
        }
        player player = collision.gameObject.GetComponent<player>();
        if (player != null)
        {
            if (player.playerHealth != 0)
            {
                healthScript.SetDestroyIndividualHealth(player.playerHealth);
                gameManagerPlatformer.instance.soundEffectSource.PlayOneShot(gameManagerPlatformer.instance.playerHitSound);
                player.Knockback(transform.position.x);
                
                player.playerHealth--;
                Debug.Log("Player Health: " + player.playerHealth);
                StopAllCoroutines();
                gameManagerPlatformer.instance.StartCooldownforHits();

            }
            else
            {
                gameManagerPlatformer.instance.warningScreenandTeleport(5);
            }
            

        }


    }
    

}

