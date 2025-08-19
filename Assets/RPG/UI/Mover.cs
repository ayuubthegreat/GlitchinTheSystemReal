using System.Collections;
using UnityEngine;

public class Mover : MonoBehaviour
{
    public static Mover instance;
    public RectTransform mtransform;
    public Vector2[] waypoints;
    public int waypointIndex = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        mtransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void AssignNewWaypointsAndMoveObject(RectTransform newTransform, Vector2[] waypointsNew, float duration, bool loop)
    {
        if (newTransform == null)
        {
            newTransform = GetComponent<RectTransform>();
        }
        mtransform = newTransform;
        waypoints = waypointsNew;
        StartCoroutine(MoveObject(duration, loop));

    }
    public IEnumerator MoveObject(float duration, bool loop)
    {
        while (waypointIndex < waypoints.Length)
        {
            waypointIndex++;
            if (loop && waypointIndex >= waypoints.Length)
            {
                waypointIndex = 0; // Reset to the first waypoint if looping
            }

            if (waypoints.Length < 2)
            {
                Debug.LogError("Not enough waypoints to move the object.");
                yield break;
            }

            Vector2 startPosition = mtransform.anchoredPosition;
            Vector2 endPosition = startPosition + waypoints[waypointIndex];
            float elapsedTime = 0f;

            while (elapsedTime < duration)
            {
                mtransform.anchoredPosition = Vector2.Lerp(startPosition, endPosition, elapsedTime / duration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            mtransform.anchoredPosition = endPosition; // Ensure it ends exactly at the target position
        }
        
    }
}
