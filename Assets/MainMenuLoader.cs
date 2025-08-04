using UnityEngine;
using UnityEngine.UI;

public class MainMenuLoader : MonoBehaviour
{
    public static MainMenuLoader instance;
    public Camera mainCamera;
    public Vector3 targetPosition;
    public float moveSpeed = 3f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void MoveToNewPosition(Vector3 newPosition, float speed = 3f)
    {
        targetPosition = mainCamera.transform.position + newPosition;
        moveSpeed = speed;
        while (Vector3.Distance(mainCamera.transform.position, targetPosition) > 0.01f)
        {
            mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, targetPosition, Time.deltaTime * moveSpeed);
        }
        mainCamera.transform.position = targetPosition; // Ensure the camera reaches the exact target position
    }
}
