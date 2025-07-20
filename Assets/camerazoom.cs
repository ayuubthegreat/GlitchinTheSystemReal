using System.Collections;
using UnityEngine;

public class camerazoom : MonoBehaviour
{
    public CameraControllerRPG cameraController;
    public float zoomSpeed = 2f;
    public float zoomValue = 2f;
    public float backZoomValue;
    private float normalCameraSize;
    private int playerDirection = 1; // 1 for right, -1 for left

    void Start()
    {
        cameraController = gameManagerPlatformer.instance.cameraController;
        normalCameraSize = gameManagerPlatformer.instance.originalCameraSize;
        if (backZoomValue == 0)
        {
            backZoomValue = normalCameraSize;
        }
    }
    void Update()
    {

    }
    void OnTriggerStay2D(Collider2D other)
    {
        player player = other.gameObject.GetComponent<player>();
        if (player != null)
        {
            playerDirection = (player.rb.linearVelocity.x >= 0) ? 1 : -1;

            gameManagerPlatformer.instance.targetCameraSize = (playerDirection == 1) ? zoomValue : backZoomValue;
            gameManagerPlatformer.instance.cameraSpeed = zoomSpeed;
        }
    }
}
