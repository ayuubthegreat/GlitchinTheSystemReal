using UnityEngine;

public class startAbdurahmanFaceHealth : MonoBehaviour
{
    public startHealthScriptt healthScriptt;
    public Animator anim;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anim = GetComponent<Animator>();
        healthScriptt = FindFirstObjectByType<startHealthScriptt>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetBool("start", UIManager.instance.start);
        anim.SetBool("isDead", UIManager.instance.isDead);
        anim.SetBool("isAlive", UIManager.instance.isAlive);
    }
    public void SetIsDeadandIsAlive(bool isDeadinner) => UIManager.instance.SetIsDeadandIsAliveBool(isDeadinner);
   public void SetStartBool(int value) => UIManager.instance.SetStartBool(value);

    
}
