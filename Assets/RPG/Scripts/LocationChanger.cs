using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LocationChanger : MonoBehaviour
{
    public BoxCollider2D b2d;
    public string backLocation;
    public string frontLocation;
    public bool showLocationSlider = true;
    public AudioClip clipBehind;
    public AudioClip clipFront;
    public bool musicChanges;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        b2d = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision) {
        playerpg player = collision.gameObject.GetComponent<playerpg>();
        if (player && showLocationSlider) {
            if (player.yInput == 1)
            {
                UIManagerRPG.instance.ChangeText(backLocation);
                if (musicChanges)
                {
                    GameManagerRPG.instance.StartFadingVolume(clipBehind, 3);
                      
                }
                
            }
            else if (player.yInput == -1)
            {
                UIManagerRPG.instance.ChangeText(frontLocation);
                if (musicChanges)
                {
                    GameManagerRPG.instance.StartFadingVolume(clipFront, 3); 
                }
                
            }
            UIManagerRPG.instance.StartMoveLocationAnnouncer(3f);
        }
    }
    
}
