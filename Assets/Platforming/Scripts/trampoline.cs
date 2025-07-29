using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class trampolineNew : MonoBehaviour
{
    public int pushPowerY;
    public float duration;
    public Animator anim;
    void Awake()
    {
        anim = GetComponent<Animator>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void OnTriggerEnter2D(Collider2D collision)
    {
        player player = collision.gameObject.GetComponent<player>();
        if (player != null)
        {
            Vector3 newPosition = player.transform.position;
            newPosition.x = transform.position.x;
            player.transform.position = newPosition;
            gameManagerPlatformer.instance.soundEffectSource.PlayOneShot(gameManagerPlatformer.instance.springSound);
            gameManagerPlatformer.instance.player.Push(transform.up * pushPowerY, duration);
            gameManagerPlatformer.instance.isTrampolining = true;
            anim.SetTrigger("active");
        }
    }
}
