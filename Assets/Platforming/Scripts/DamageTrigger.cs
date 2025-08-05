using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class DamageTrigger : MonoBehaviour
{
    public float coolDownPeriod = 2f;
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
        
        player player = collision.gameObject.GetComponent<player>();
        if (player != null)
        {
            if (!gameManagerPlatformer.instance.canBeHit)
            {
                return;
            }
            if (player.playerHealth != 0)
            {
                
                healthScript.SetDestroyIndividualHealth(player.playerHealth);
                player.playerHealth--;
                Debug.Log("Player Health: " + player.playerHealth);
                
                player.Knockback(transform.position.x);
                StopAllCoroutines();

                gameManagerPlatformer.instance.soundEffectSource.PlayOneShot(gameManagerPlatformer.instance.playerHitSound);
                gameManagerPlatformer.instance.StartCooldownforHits(coolDownPeriod);

            }
            else
            {
                gameManagerPlatformer.instance.warningScreenandTeleport(5);
            }
            

        }


    }
    

}

