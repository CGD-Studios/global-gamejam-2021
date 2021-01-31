using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent), typeof(SphereCollider))]
public class EnemyController : MonoBehaviour
{
    private static readonly string playerTag = "Player";

    [SerializeField]
    private Transform goal;

    private NavMeshAgent navMeshAgent;
    private bool playerInArea;

    // Start is called before the first frame update
    void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        // Other setup if necessary
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(playerInArea)
        {
            // Raycast to see if can see player
            RaycastHit hit;
            Vector3 rayDirection = goal.position - transform.position;
            Debug.DrawLine(transform.position, goal.position);
            if (Physics.Raycast(transform.position, rayDirection.normalized, out hit, 100))
            {
                Debug.Log("Hit something");
                if(hit.transform.gameObject.tag == playerTag)
                {
                    navMeshAgent.destination = goal.position;
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == playerTag)
        {
            playerInArea = true;
            navMeshAgent.isStopped = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == playerTag)
        {
            playerInArea = false;
            navMeshAgent.isStopped = true;
        }
    }
}
