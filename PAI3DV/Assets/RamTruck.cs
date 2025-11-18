using System.Collections;
using NUnit.Framework.Constraints;
using UnityEngine;
using UnityEngine.AI;

public class RamTruck : MonoBehaviour
{
    [Header("Ram Truck Stats")]
    [SerializeField] private float ExplosionDamage = 100f;
    
    [Header("AI")]
    [SerializeField] private GameObject playerTarget;
    private NavMeshAgent agent;
    private Rigidbody enemyRB;
    [SerializeField] private GameObject explosionEffect;
    [SerializeField] private Transform explosionEffectTransform;
    
    
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


    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerAttributes = collision.gameObject.GetComponent<PlayerAttributes>();
            playerAttributes.TakeDamage(ExplosionDamage);
            var playerVFX = collision.gameObject.GetComponent<PlayerBehavior>();
            playerVFX.OnFire();
            Explode();
        }
    }

    public void Explode()
    {
        enemyRB.AddExplosionForce(1000000f, transform.position, 500f, 3f);
        agent.isStopped = true;
        GameObject explosion = Instantiate(explosionEffect, explosionEffectTransform.position, Quaternion.identity);
        Destroy(explosion.gameObject, 2f);
        Destroy(gameObject);
       // StartCoroutine(WaitAndDestroy());
    }

    IEnumerator WaitAndDestroy()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
        StopCoroutine(WaitAndDestroy());
    }
    
    
    
    
}
