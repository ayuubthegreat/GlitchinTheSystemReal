using System.Collections;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class Mover : MonoBehaviour
{
    public RectTransform mtransform;
    public Vector2[] waypoints;
    public Vector2[] scaleWaypoints;
    public int waypointIndex = 0;
    public int scaleWaypointIndex = 0;
    public float speed = 5f;
    public float scaleSpeed = 5f;
    public bool loop = false;
    public Vector2 originalPosition;
    public Animator anim;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {

    }
    void Start()
    {
        mtransform = GetComponent<RectTransform>();
        anim = GetComponent<Animator>();
        originalPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        MoveObject();
        ScaleObject();
    }
    public void AssignNewWaypointsAndMoveObject(Vector2[] waypointsNew, float speed, bool floop = false)
    {
        if (mtransform == null)
        {
            mtransform = GetComponent<RectTransform>();
        }
        for (int i = 0; i < waypointsNew.Length; i++)
        {
            waypointsNew[i] += mtransform.anchoredPosition;
        }
        waypoints = waypointsNew;
        loop = floop;
        this.speed = speed;
        waypointIndex = 0;
    }
    public void Scaler(Vector2[] scaleWaypointsNew, float speed, bool floop = false)
    {
        if (mtransform == null)
        {
            mtransform = GetComponent<RectTransform>();
        }
        scaleWaypoints = scaleWaypointsNew;
        loop = floop;
        scaleSpeed = speed;
        scaleWaypointIndex = 0;
    }
    public void MoveObject()
    {
        if (waypoints.Length == 0 || waypointIndex >= waypoints.Length) return;
        mtransform.anchoredPosition = Vector3.MoveTowards(mtransform.anchoredPosition, waypoints[waypointIndex], Time.unscaledDeltaTime * speed);
        if (Vector2.Distance(mtransform.anchoredPosition, waypoints[waypointIndex]) < 0.01f)
        {
            waypointIndex++;
            if (waypointIndex >= waypoints.Length && loop)
            {
                waypointIndex = 0; // Loop back to the first waypoint
            }
        }
    }
    public void ScaleObject()
    {
        if (scaleWaypoints.Length == 0 || scaleWaypointIndex >= scaleWaypoints.Length) return;
        mtransform.localScale = Vector2.MoveTowards(mtransform.localScale, scaleWaypoints[scaleWaypointIndex], Time.unscaledDeltaTime * scaleSpeed);
        if (Mathf.Abs(mtransform.localScale.x - scaleWaypoints[scaleWaypointIndex].x) < 0.01f && Mathf.Abs(mtransform.localScale.y - scaleWaypoints[scaleWaypointIndex].y) < 0.01f)
        {
            scaleWaypointIndex++;
            if (scaleWaypointIndex >= scaleWaypoints.Length && loop)
            {
                scaleWaypointIndex = 0; // Loop back to the first waypoint
            }
        }
    }

}
