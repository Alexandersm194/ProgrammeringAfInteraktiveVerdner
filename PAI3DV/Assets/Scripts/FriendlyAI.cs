using System;
using UnityEngine;
using UnityEngine.AI;

public class FriendlyAI : MonoBehaviour
{
    private NavMeshAgent agent;
    [SerializeField] private Transform[] targetList;
    [SerializeField] private int currentTarget = -1;

    [SerializeField] private GameObject explosionEffect;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    void Update()
    {
        if(agent == null) return;
        if (currentTarget < targetList.Length && currentTarget >= 0)
        {
            agent.SetDestination(targetList[currentTarget].position);
        }
        else
        {
            agent.SetDestination(transform.position);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Explode();
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Waypoint"))
        {
            currentTarget++;
        }
        if (other.gameObject.CompareTag("AnimGrenade"))
        {
            Explode();
            Debug.Log("Explode");
        }
    }

    private void Explode()
    {
        agent = null;
        GameObject explosionVFX = Instantiate(explosionEffect, transform.position, Quaternion.identity);
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.AddExplosionForce(1000, transform.position - new Vector3(0f, -5f, 0f), 500, 1000000f, ForceMode.Impulse);
        Destroy(explosionVFX, 2f);
        Destroy(gameObject, 2f);
    }
    public void SetCurrentTarget()
    {
        currentTarget++;
    }
}
