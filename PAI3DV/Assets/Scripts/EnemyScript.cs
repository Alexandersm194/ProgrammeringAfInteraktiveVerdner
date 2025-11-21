using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyScript : MonoBehaviour
{
    [Header("Enemy Stats")]
    [SerializeField] private float health = 100f;
    
    [SerializeField] private GameObject spine;


    [SerializeField] private NavMeshAgent agent;

    [SerializeField] private GameObject explosion;
    
    [SerializeField] private Slider healthSlider;
    
    private GameObject player;
    
    private Transform target;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        SetTarget(player.transform);
        healthSlider.maxValue = health;
    }
    public void TakeDamage(float damage)
    {
        health -= damage;
        healthSlider.value = health;
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }
    

    void Update()
    {
        agent.SetDestination(target.position);
        spine.transform.LookAt(player.transform.position);

        if (health <= 0)
        {
            GameObject exp = Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(exp, 2f);
            Destroy(gameObject);
        }
    }
}
