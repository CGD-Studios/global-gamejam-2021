using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent), typeof(SphereCollider))]
public class EnemyController : MonoBehaviour
{
    private static readonly string playerTag = "Player";

    [SerializeField]
    private int baseHealth = 100;
    [SerializeField]
    private int baseDamage = 5;
    [SerializeField]
    private float attackDistance = 5.0f;
    [SerializeField]
    private float attackCooldown = 1.0f;
    [SerializeField]
    private Transform goal;

    private NavMeshAgent navMeshAgent;
    private bool playerInArea;
    private int health;

    // Start is called before the first frame update
    void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        // Other setup if necessary
        health = baseHealth;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(playerInArea)
        {
            // Raycast to see if can see player
            RaycastHit hit;
            Vector3 rayDirection = goal.position - transform.position;
            if(Physics.Raycast(transform.position, rayDirection.normalized, out hit, rayDirection.magnitude))
            {
                if(hit.transform.gameObject.tag == playerTag)
                {
                    navMeshAgent.destination = goal.position;
                }
            }

            // Check if player is close enough to be attacked
            if(rayDirection.magnitude <= attackDistance)
            {
                // attack player
                // TODO: implement proper attack logic
                Debug.Log("Attack Player");
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

    public void ApplyDamage(int damage)
    {
        health -= damage;
        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void GiveHealth(int value)
    {
        health += value;
    }
}
