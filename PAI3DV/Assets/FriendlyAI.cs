using System;
using UnityEngine;
using UnityEngine.AI;

public class FriendlyAI : MonoBehaviour
{
    private NavMeshAgent agent;
    [SerializeField] private Transform[] targetList;
    [SerializeField] private int currentTarget = -1;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    void Update()
    {
        if (currentTarget < targetList.Length && currentTarget >= 0)
        {
            agent.SetDestination(targetList[currentTarget].position);
        }
        else
        {
            agent.SetDestination(transform.position);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Waypoint"))
        {
            currentTarget++;
        }
    }

    public void SetCurrentTarget()
    {
        currentTarget++;
    }
}
