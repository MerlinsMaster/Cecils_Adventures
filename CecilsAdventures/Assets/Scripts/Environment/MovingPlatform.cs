using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public GameObject platform;                                                 // Reference to the platform game object
    public Transform currentWaypoint;                                           // The waypoint that the platform is currently destined for
    public Transform[] waypoints;                                               // An array of all the waypoints for this platform

    public float moveSpeed;                                                     // How fast the platform moves
    public int waypointIndex;                                                   // The number of the cureentWaypoint in the waypoints array

    void Start()
    {
        currentWaypoint = waypoints[waypointIndex];                             // Define the currentWaypoint
    }

    void Update()
    {
        platform.transform.position = Vector3.MoveTowards(platform.transform.position, currentWaypoint.position, Time.deltaTime * moveSpeed);       // Platform moves toward currentWaypoint at moveSpeed

        if (platform.transform.position == currentWaypoint.position)            // If the platform reaches the currentWaypoint location...
        {
            waypointIndex++;                                                    // ...advance the waypointIndex by 1

            if (waypointIndex == waypoints.Length)                              // If that number exceeds the total number in the array...
            {
                waypointIndex = 0;                                              // ...then make the index the first item in the array
            }

            currentWaypoint = waypoints[waypointIndex];                         // Define the currentWaypoint
        }
    }
}
