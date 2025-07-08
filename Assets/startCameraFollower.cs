using Unity.VisualScripting;
using UnityEngine;

public class startCameraFollower : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void OnTriggerStay2D(Collider2D collision) {
        player player = collision.gameObject.GetComponent<player>();
        if (player != null)
        {
            if (player.xInput < 0)
            {
                gameManagerPlatformer.instance.cameraController.startFollowingPlayer = false;
            }
            else
            {
                gameManagerPlatformer.instance.cameraController.startFollowingPlayer = true;
            }
            
        }
    }
}
