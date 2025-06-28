using System.Collections;
using UnityEngine;


public class locationAnnouncer : MonoBehaviour
{
    public waypointScript[] waypoints;
    public bool canMove;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        waypoints = GetComponentsInChildren<waypointScript>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public IEnumerator WaypointMover(int value)
    {
        if (!canMove)
        {
            yield break;
        }
        
        yield return null;
    }
    public void SetCanMove(bool value)
    {
        canMove = value;
    }
    public void SetRealWaypoints(int value)
    {
        value = 0;
        waypoints = GetComponentsInChildren<waypointScript>();
        transform.position = Vector2.MoveTowards(transform.position, waypoints[value].transform.position, 10);
        if (Vector2.Distance(transform.position, waypoints[value].transform.position) < 0.1f)
        {
            
        }
        else
        {
            canMove = true;
        }
    } 
}

