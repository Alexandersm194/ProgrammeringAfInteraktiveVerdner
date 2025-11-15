using System.Collections;
using UnityEngine;

public class RocketLauncher : MonoBehaviour
{
    [SerializeField] private float fireRate = 2f;
    private float rate = 2f;
    [SerializeField] private float rocketLaunchForce = 1000f;
    [SerializeField] private GameObject rocketPrefab;

    private GameObject target;


    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
    }
    void Update()
    {
        transform.LookAt(target.transform);
        if (rate < 0)
        {
            FireRocket();
            rate = fireRate;
        }

        rate -= Time.deltaTime;
    }


    private void FireRocket()
    {
        GameObject rocket = Instantiate(rocketPrefab, transform.position, transform.rotation);
        Rigidbody rb = rocket.GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * rocketLaunchForce, ForceMode.Force);
        StartCoroutine(DestroyAfterSeconds(10f, rocket));
    }

    private IEnumerator DestroyAfterSeconds(float seconds, GameObject rocket)
    {
        yield return new WaitForSeconds(seconds);
        Destroy(rocket);
    }
}
