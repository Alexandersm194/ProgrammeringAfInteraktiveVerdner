using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentCollision : MonoBehaviour
{
    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Tree"))
        {
            Rigidbody collisionRB = collision.GetComponent<Rigidbody>();
            Vector3 fallDirection = transform.position - collision.gameObject.transform.position ;
            StartCoroutine(Fall(collisionRB, collision.gameObject));
            collision.gameObject.tag = "Untagged";
        }
    }

    IEnumerator Fall(Rigidbody rb, GameObject collision)
    {
        yield return new WaitForSeconds(0.1f);
        rb.freezeRotation = false;
        rb.AddTorque(Vector3.forward * 500f, ForceMode.Force);
        yield return new WaitForSeconds(0.5f);
        Destroy(collision);
    }
}
