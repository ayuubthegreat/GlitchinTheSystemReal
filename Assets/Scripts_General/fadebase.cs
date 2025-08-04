using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


public class fadebase : MonoBehaviour
{
    
    
    public float moveDuration = 2f;
    public float logoDuration = 2f;
    public float logoNumLimit = 4;
    public Animator anim;
    public string sceneName;


    public void Start()
    {
        
    }

    public void Update()
    {
        anim.SetBool("canMove", UIManager.instance.canTransition);
        anim.SetBool("canStartMoving", UIManager.instance.startTransitions[2]);
        

    }


}

    
   

