// TouchManager.cs
// Author: Sura

using UnityEngine;

public class TouchManager : MonoBehaviour
{
    public RectTransform joystickBG;
    public RectTransform joystick;
    public Vector2 inputVector;

    private Vector2 startTouchPosition;
    private float joystickRadius;

    void Start()
    {
        joystickRadius = joystickBG.sizeDelta.x / 2;
        inputVector = Vector2.zero;
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector2 touchPos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                joystickBG, touch.position, null, out touchPos
            );

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    startTouchPosition = touchPos;
                    break;

                case TouchPhase.Moved:
                case TouchPhase.Stationary:
                    Vector2 offset = touchPos - startTouchPosition;
                    inputVector = Vector2.ClampMagnitude(offset / joystickRadius, 1f);
                    joystick.anchoredPosition = inputVector * joystickRadius;
                    break;

                case TouchPhase.Ended:
                case TouchPhase.Canceled:
                    inputVector = Vector2.zero;
                    joystick.anchoredPosition = Vector2.zero;
                    break;
            }
        }
        else
        {
            inputVector = Vector2.zero;
            joystick.anchoredPosition = Vector2.zero;
        }
    }

    public float Horizontal()
    {
        return inputVector.x;
    }

    public float Vertical()
    {
        return inputVector.y;
    }

    public Vector2 Direction()
    {
        return inputVector;
    }
}
