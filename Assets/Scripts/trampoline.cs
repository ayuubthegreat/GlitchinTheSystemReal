using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class trampolineNew : MonoBehaviour
{
    public int pushPower;
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
            GameManager.instance.player.Push(transform.up * pushPower, duration);
            anim.SetTrigger("active");
        }
    }
}
