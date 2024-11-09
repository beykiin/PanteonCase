using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICharaters : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    public Waypoint targetWaypoint;
    private Waypoint nextTargetWaypoint;

    private void Start()
    {
        nextTargetWaypoint= targetWaypoint;
    }

    private void Update()
    {
        MoveTowardsWaypoint();
    }

   private void MoveTowardsWaypoint()
   {
        if (nextTargetWaypoint == null) return;
        transform.position = Vector3.MoveTowards(transform.position, nextTargetWaypoint.transform.position, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, nextTargetWaypoint.transform.position) < 0.2f)
        { 
            ChooseNextWaypoint();
        }
   }

    public void ChooseNextWaypoint()
    {
        if (nextTargetWaypoint.connectedWaypoints.Count > 0)
        {
            nextTargetWaypoint = nextTargetWaypoint.connectedWaypoints[Random.Range(0,nextTargetWaypoint.connectedWaypoints.Count)];
        }
    }

}
