using UnityEngine;

public class RocketScript : MonoBehaviour
{
    [SerializeField] private GameObject ExlposionEffect;
    [SerializeField] private float damage;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GiveDamage(other.gameObject);
            Explode(other.gameObject);
        }
        Destroy(gameObject);
        
    }


    private void GiveDamage(GameObject player)
    {
        PlayerAttributes playerAttributes = player.GetComponent<PlayerAttributes>();
        playerAttributes.TakeDamage(damage);
    }

    private void Explode(GameObject player)
    {
        Rigidbody rb = player.GetComponent<Rigidbody>();
        rb.AddExplosionForce(200000f, transform.position, 30, 0.5f);
        GameObject effect = Instantiate(ExlposionEffect, transform.position, Quaternion.identity);
        Destroy(effect, 2f);
    }
}
