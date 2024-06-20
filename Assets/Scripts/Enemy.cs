using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public Transform player;
    public float speed = 2.0f;
    private NavMeshAgent navMeshAgent;
    public float deathTime = 0.5f;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();

        // Set the speed of the enemy to follow the player slowly
        navMeshAgent.speed = speed; // Adjust this value as needed
    }

    void Update()
    {
        if (player != null)
        {
            navMeshAgent.SetDestination(player.position);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Enemy entered generic other collision!");

        
        if (other.CompareTag("KillBox"))
        {
            Debug.Log("Enemy entered kill box!");
            Destroy(gameObject, deathTime);
        }
    }
}
