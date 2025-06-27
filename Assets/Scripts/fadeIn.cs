using System.Collections;
using UnityEngine;

public class fadeIns : MonoBehaviour
{
    public Animator anim;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public int fadeNumPeriod;
    void Awake()
    {
        // fadeNums = GameManager.instance.fadeNums;
        anim = GetComponent<Animator>();
    }


    void Update()
    {

        anim.SetFloat("fadeMode", UIManager.instance.fadeNums);
    }

    public void starttheShowReal()
    {
        Destroy(gameObject);
    } 
    // public void changeFadeNumReal() => GameManager.instance.changeFadeNum();
    
}



