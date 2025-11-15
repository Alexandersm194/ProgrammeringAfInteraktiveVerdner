using System;
using UnityEngine;
using UnityEngine.AI;

public class EnemyScript : MonoBehaviour
{
    [Header("Enemy Stats")]
    [SerializeField] private float health = 100f;
    
    [Header("Animations")]
    [SerializeField] private Animator anim;


    [SerializeField] private bool isMoving;
    [SerializeField] private GameObject spine;


    [SerializeField] private NavMeshAgent agent;
    
    private GameObject player;
    
    private Transform target;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    public void TakeDamage(float damage)
    {
        health -= damage;
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }

    void Update()
    {
        agent.SetDestination(target.position);
        spine.transform.LookAt(player.transform.position);
        isMoving = agent.remainingDistance > agent.stoppingDistance;
        anim.SetBool("IsMoving", isMoving);
    }
}
