using NUnit.Framework.Constraints;
using UnityEngine;
using UnityEngine.AI;

public class RamTruck : MonoBehaviour
{
    [Header("AI")]
    [SerializeField] private GameObject playerTarget;
    private NavMeshAgent agent;
    private Rigidbody enemyRB;
    [SerializeField] private ParticleSystem[] explosionParticles;
    
    void Start()
    {
        agent  = GetComponent<NavMeshAgent>();
        enemyRB = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(playerTarget.transform.position);
    }


    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            var playerVFX = collision.gameObject.GetComponent<PlayerBehavior>();
            playerVFX.OnFire();
            Explode();
        }
    }

    public void Explode()
    {
        enemyRB.AddExplosionForce(1000000f, transform.position, 500f, 1f);
        agent.isStopped = true;
        foreach (ParticleSystem par in explosionParticles)
        {
            par.Play();
        }
    }
    
    
    
    
}
