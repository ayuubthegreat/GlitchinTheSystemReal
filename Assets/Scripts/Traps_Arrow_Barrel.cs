using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Traps_Arrow_Barrel : trampolineNew
{
    public int rotateSpeed;
    public bool rotationRight;
    public int direction = 1;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        direction = rotationRight ? 1 : -1;
        transform.Rotate(0, 0, (rotateSpeed * direction) * Time.deltaTime);

    }
    public void DeathtoYou()
    {
        
        GameManager.instance.CreateNewObjectReal(GameManager.instance.arrows, transform, 2);
        Destroy(gameObject);
  }
    
    
}
