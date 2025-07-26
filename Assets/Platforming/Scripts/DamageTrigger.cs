using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class DamageTrigger : MonoBehaviour
{
    public int coolDownPeriod = 2;
    public bool canBeHit = true;
    public startHealthScriptt healthScript;
    public void Start()
    {
        healthScript = FindFirstObjectByType<startHealthScriptt>();
    }
    public void OnEnable()
    {
        canBeHit = true;
        if (healthScript == null)
        {
            healthScript = FindFirstObjectByType<startHealthScriptt>();
        }
    }
    public void OnTriggerStay2D(Collider2D collision)
    {
        if (!canBeHit)
        {
            return;
        }
        player player = collision.gameObject.GetComponent<player>();
        if (player != null)
        {
            if (player.playerHealth != 0)
            {
                gameManagerPlatformer.instance.source.PlayOneShot(gameManagerPlatformer.instance.playerHurtSound);
                player.Knockback(transform.position.x);
                player.playerHealth--;
                healthScript.SetDestroyIndividualHealth(player.playerHealth);
                Debug.Log("Player Health: " + player.playerHealth);
                StartCoroutine(CooldownforHits());

            }
            else
            {
                gameManagerPlatformer.instance.warningScreenandTeleport(5);
            }
            

        }


    }
    public IEnumerator CooldownforHits()
    {
        canBeHit = false;
        yield return new WaitForSeconds(coolDownPeriod);
        canBeHit = true;
    }

}

