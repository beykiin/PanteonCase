using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectCoinAI : MonoBehaviour
{
    public Waypoint linkedWaypoint;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("AI"))
        { 
            //Destroy(linkedWaypoint.gameObject);
            //linkedWaypoint = null;


            other.GetComponent<AICharaters>().ChooseNextWaypoint();

        }
    }
}
