using System.Collections;
using UnityEngine;

public class Mover : MonoBehaviour
{
    public static Mover instance;
    public RectTransform mtransform;
    public Vector2[] waypoints;
    public int waypointIndex = 0;
    public float speed = 5f;
    public bool loop = false;
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
        if (waypoints.Length == 0 || waypointIndex >= waypoints.Length) return;
        mtransform.anchoredPosition = Vector3.MoveTowards(mtransform.anchoredPosition, waypoints[waypointIndex], Time.deltaTime * speed);
        if (Vector2.Distance(mtransform.anchoredPosition, waypoints[waypointIndex]) < 0.01f)
        {
            waypointIndex++;
            if (waypointIndex >= waypoints.Length && loop)
            {
                waypointIndex = 0; // Loop back to the first waypoint
            }
        }
    }
    public void AssignNewWaypointsAndMoveObject(RectTransform newTransform, Vector2[] waypointsNew, float speed, bool floop = false)
    {
        if (newTransform == null)
        {
            newTransform = GetComponent<RectTransform>();
        }
        for (int i = 0; i < waypointsNew.Length; i++)
        {
            waypointsNew[i] += newTransform.anchoredPosition;
        }
        mtransform = newTransform;
        waypoints = waypointsNew;
        loop = floop;
        this.speed = speed;
        waypointIndex = 0;
    }
    
}
