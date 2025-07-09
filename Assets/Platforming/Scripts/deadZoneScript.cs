using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class deadZoneScript : MonoBehaviour
{

    public SceneManager sm;
    public string scene;
    public int WaitForSeconds;
    void OnTriggerEnter2D(Collider2D collision)
    {
        player player = collision.GetComponent<player>();
        if (player != null)
        {
            GameManager.instance.startSpawnBool = false;
            gameManagerPlatformer.instance.warningScreenandTeleport(5);
        }
    }
}
