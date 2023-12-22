using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{



    [SerializeField] private Transform[] waypoints;
    [SerializeField] private float platformSpeed;
    [SerializeField] private float checkDistance = 0.05f;

    private Transform targetWaypoint;
    private int currentWaypointIndex = 0;


    // Start is called before the first frame update
    void Start()
    {

        targetWaypoint = waypoints[0];


    }

    // Update is called once per frame
    void Update()
    {


        transform.position = Vector2.MoveTowards(transform.position, targetWaypoint.position, platformSpeed * Time.deltaTime);

        if (Vector2.Distance(transform.position, targetWaypoint.position) <= checkDistance) 
        {
            targetWaypoint = GetNextWaypoint();
        
        }

    }


    private Transform GetNextWaypoint()
    {


        currentWaypointIndex++;
        if (currentWaypointIndex >= waypoints.Length)
        {
            currentWaypointIndex = 0;
        }

        return waypoints[currentWaypointIndex];
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        var playermovement = collision.gameObject.GetComponent<PlayerMovement>();
        if (playermovement != null) 
        {
            playermovement.SetParent(transform);
        }
    }

  

    private void OnCollisionExit2D(Collision2D collision)
    {
        var playermovement = collision.gameObject.GetComponent<PlayerMovement>();
        if (playermovement != null)
        {
            playermovement.ResetParent();
        }
    }


    
}
