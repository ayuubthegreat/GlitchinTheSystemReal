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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        b2d = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerEnter2D(Collider2D collision) {
        playerpg player = collision.gameObject.GetComponent<playerpg>();
        if (player) {
            if (player.yInput == 1) {
                UIManager.instance.location = backLocation;
            } else if (player.yInput == -1) {
                UIManager.instance.location = frontLocation;
            }
            UIManager.instance.StartChangeTransitionBools();
        }
    }
    
}
