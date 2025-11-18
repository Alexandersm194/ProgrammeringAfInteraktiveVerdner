using System;
using System.Collections;
using UnityEngine;

public class GrenadeThrow : MonoBehaviour
{
    [SerializeField] private float fireRate = 2f;
    [SerializeField] private float rate = 2f;
    [SerializeField] private float throwForce = 1000f;
    
    [SerializeField] private float upForce = 5f;
    
    [SerializeField] private GameObject grenadePrefab;
    
    [Header("Animations")]
    [SerializeField] private Animator anim;
    [SerializeField] private AnimationClip throwClip;
    
    private GameObject target;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        gameObject.transform.LookAt(target.transform);
        if (rate < 0)
        {
            StartCoroutine(Throw());
            rate = fireRate;
        }

        rate -= Time.deltaTime;
    }

    IEnumerator Throw()
    {
        anim.SetTrigger("Throw");
        yield return new WaitForSeconds(throwClip.length);
        ThrowGrenade();
        StopCoroutine(Throw());
    }
    private void ThrowGrenade()
    {
        GameObject grenade = Instantiate(grenadePrefab, transform.position, transform.rotation);
        Rigidbody rb = grenade.GetComponent<Rigidbody>();
        rb.AddForce((transform.forward * throwForce + new Vector3(0f, upForce, 0f)), ForceMode.Force);
    }
}
