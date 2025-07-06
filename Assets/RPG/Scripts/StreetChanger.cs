using UnityEngine;
using System.Collections.Generic;

public class StreetChanger : LocationChanger
{
    public bool useXInput;
    public bool useYInput;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        playerpg player = collision.gameObject.GetComponent<playerpg>();
        if (player != null)
        {
         UIManager.instance.streetName = (player.xInput == -1 || player.yInput == -1) ? backLocation : frontLocation;   
        }
        
    }
}