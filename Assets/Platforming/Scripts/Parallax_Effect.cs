using UnityEngine;

public class Parallax_Effect : MonoBehaviour
{
    public float parallaxMultiplier = 0.5f;
    public player playerScript;
    public bool isPlayerScript = true;
    private Transform cameraTransform;
    public Transform sharedTransform;
    public bool isSharedTransform = true;
    public float yOffset;
    public bool canMoveY;
    public float initialYPosition;
    public float currentYPosition;
    public Vector3 previousCameraPosition;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (Camera.main == null)
        {
            Debug.LogError("No main camera found. Please assign a camera with the 'MainCamera' tag.");
            return;
        }
        cameraTransform = Camera.main.transform;
        previousCameraPosition = cameraTransform.position;
        initialYPosition = transform.position.y;
        if (playerScript == null)
        {
            playerScript = gameManagerPlatformer.instance.player;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 deltaPosition = cameraTransform.position - previousCameraPosition;
        float deltaY = deltaPosition.y * parallaxMultiplier;
        transform.position += new Vector3(deltaPosition.x * parallaxMultiplier, 0f, 0f);
        playerScript = gameManagerPlatformer.instance.player;
        currentYPosition = transform.position.y;
        isPlayerScript = playerScript != null;
        previousCameraPosition = cameraTransform.position;
    }
}
