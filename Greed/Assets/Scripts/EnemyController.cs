using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    private Transform goal;

    private NavMeshAgent navMeshAgent;

    // Start is called before the first frame update
    void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        navMeshAgent.destination = goal.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
