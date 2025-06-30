using System.Collections;
using UnityEngine;


public class fadebase : MonoBehaviour
{
    
    
    public float moveDuration = 2f;
    public Animator anim;


    public void Start()
    {
        anim = GetComponent<Animator>();
    }
    public IEnumerator ChangeTransitionBoolTimer()
    {
        UIManager.instance.canTransition = false;
        yield return new WaitForSeconds(moveDuration);
        UIManager.instance.canTransition = true;
    }
    public void ChangeTransitionBool(int value)
    {
        if (value == 1)
        {
            UIManager.instance.canTransition = true;
        }
        else
        {
            UIManager.instance.canTransition = false;
        }
    }
    
    public void Update()
    {
        anim.SetBool("canMove", UIManager.instance.canTransition);
    }


}

    
   

