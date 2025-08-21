using UnityEngine;

public class Rotater : MonoBehaviour
{
    public bool canRotate = true;
    public float rotationSpeed = 50f;
    public RectTransform rectTransform;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (rectTransform != null)
        {
            rectTransform.Rotate(Vector3.forward, rotationSpeed * Time.unscaledDeltaTime);
        }
    }
    
}
