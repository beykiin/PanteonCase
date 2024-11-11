using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICharaters : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    public Waypoint targetWaypoint;
    private Waypoint nextTargetWaypoint;
    



    private Vector3 startPosition;
    private Animator animator;
    private Rigidbody _rb;
    private bool isStopped = false;


    private void Awake()
    {
        animator = GetComponent<Animator>();
        _rb= GetComponent<Rigidbody>();
    }


    private void Start()
    {
        startPosition = transform.position;
        nextTargetWaypoint= targetWaypoint;
        _rb.freezeRotation= true;
    }

    public void ResetAI()
    {
        transform.position = startPosition;
        nextTargetWaypoint = targetWaypoint;
        animator.SetTrigger("StartWalking");
       
    }

    private void FixedUpdate()
    {
        if (!isStopped)
        {
            MoveTowardsWaypoint();
        }
    }

   private void MoveTowardsWaypoint()
   {
        if (nextTargetWaypoint == null) return;
        Vector3 direction = (nextTargetWaypoint.transform.position - transform.position).normalized;
        _rb.MovePosition(_rb.position + direction * speed * Time.fixedDeltaTime);
        //transform.position = Vector3.MoveTowards(transform.position, nextTargetWaypoint.transform.position, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, nextTargetWaypoint.transform.position) < 0.2f)
        {
            animator.SetTrigger("StopWalking");
            ChooseNextWaypoint();
        }
        else
        {
            animator.SetTrigger("StartWalking");
        }
   }

    public void ChooseNextWaypoint()
    {
        if (nextTargetWaypoint.connectedWaypoints.Count > 0)
        {
            nextTargetWaypoint = nextTargetWaypoint.connectedWaypoints[Random.Range(0,nextTargetWaypoint.connectedWaypoints.Count)];
        }
    }
    public void StopAI(bool stop)
    {
        isStopped = stop;
        if (stop)
        {
            animator.SetFloat("Speed", 0);
        }
    }




}
