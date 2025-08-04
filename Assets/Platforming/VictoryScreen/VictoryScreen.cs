using UnityEngine;
using UnityEngine.UI;


public class VictoryScreen : MonoBehaviour
{
    public Animator anim;
    public bool finishedCounting = false;


    public void Start()
    {
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        anim.SetBool("finished", UIManagerPlatformer.instance.finishedCounting);
    }
}