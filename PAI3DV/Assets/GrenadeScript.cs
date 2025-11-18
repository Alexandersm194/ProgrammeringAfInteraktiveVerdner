using System.Collections;
using UnityEngine;

public class GrenadeScript : MonoBehaviour
{
    [SerializeField] private float grenadeDamage = 5f;
    [SerializeField] private GameObject explosionPrefab;
    [SerializeField] private float timeToExplode;
    void Start()
    {
        StartCoroutine(TimeToDestroy());
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Explode(collision.gameObject);
            PlayerAttributes playerAttributes = collision.gameObject.GetComponent<PlayerAttributes>();
            playerAttributes.TakeDamage(grenadeDamage);
        }
    }
    
    IEnumerator TimeToDestroy()
    {
        yield return new WaitForSeconds(timeToExplode);
        Explode();
    }

    void Explode(GameObject player = null)
    {
        StopCoroutine(TimeToDestroy());
        if (player != null)
        {
            Rigidbody rb = player.GetComponent<Rigidbody>();
            rb.AddExplosionForce(200000f, transform.position, 30, 0.5f);
        }
        GameObject explosion = Instantiate(explosionPrefab, transform.position, transform.rotation);
        Destroy(explosion, 2f);
        Destroy(gameObject);
    }
    
}
