using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DamageTrigger : MonoBehaviour
{
    public startHealthScriptt healthScript;
    public void Start()
    {
        healthScript = FindFirstObjectByType<startHealthScriptt>();
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        player player = collision.gameObject.GetComponent<player>();
        if (player != null)
        {
            if (player.playerHealth != 0)
            {
                player.Knockback(transform.position.x);
                player.playerHealth--;
                healthScript.SetDestroyIndividualHealth(player.playerHealth - 1);

            }
            else
            {
                GameManager.instance.warningScreenandTeleport(5);
            }

        }


    }

}

