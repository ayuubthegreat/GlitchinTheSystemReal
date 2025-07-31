using UnityEngine;

public class Parallax_Effect : MonoBehaviour
{
    public float parallaxMultiplier = 0.5f;
    private Transform cameraTransform;
    public Transform sharedTransform;
    public bool isSharedTransform = true;
    public float yOffset;
    public bool canMoveY;
    public float initialYPosition;
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
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManagerPlatformer.instance.player == null)
        {
            transform.position = new Vector3(transform.position.x, initialYPosition, transform.position.z);
            return;
        }
        canMoveY = transform.position.y < initialYPosition + yOffset && transform.position.y >= initialYPosition;
        Vector3 deltaPosition = cameraTransform.position - previousCameraPosition;
        transform.position += new Vector3(deltaPosition.x * parallaxMultiplier, deltaPosition.y * parallaxMultiplier * (canMoveY ? 1 : 0), 0f);

        previousCameraPosition = cameraTransform.position;
        
    }
}
