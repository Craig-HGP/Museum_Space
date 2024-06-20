using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public Transform player;
    public float speed = 2.0f;
    private NavMeshAgent navMeshAgent;
    public float deathTime = 0.5f;

    public int life = 1;

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

        if (life <= 0)
        {
            Destroy(gameObject, deathTime);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collision detected with: " + other.name);
        
        if (other.CompareTag("KillBox1"))
        {
            Debug.Log("Collision with KillBox detected!");
            life -= 1;
            Debug.Log("Life remaining: " + life);
        }
    }
}
