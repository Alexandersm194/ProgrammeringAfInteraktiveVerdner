using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterTruck : MonoBehaviour
{
    [Header("Monster Truck Stats")]
    [SerializeField] private float ExplosionDamage = 100f;
    
    [Header("AI")]
    [SerializeField] private GameObject playerTarget;
    private NavMeshAgent agent;
    private Rigidbody enemyRB;
    [SerializeField] private ParticleSystem[] explosionParticles;
    
    
    [SerializeField] private PlayerAttributes playerAttributes;
    void Start()
    {
        playerTarget = GameObject.FindGameObjectWithTag("Player");
        agent  = GetComponent<NavMeshAgent>();
        enemyRB = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(playerTarget.transform.position);
    }
    

    public void Explode()
    {
        enemyRB.AddExplosionForce(1000000f, transform.position, 500f, 3f);
        agent.isStopped = true;
        foreach (ParticleSystem par in explosionParticles)
        {
            par.Play();
        }
        StartCoroutine(WaitAndDestroy());
    }

    IEnumerator WaitAndDestroy()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
        StopCoroutine(WaitAndDestroy());
    }
}
