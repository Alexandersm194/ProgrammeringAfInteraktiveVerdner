using UnityEngine;

public class BikeAttackScript : MonoBehaviour
{
    [SerializeField] GameManager gameManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameManager.BikerAttack();
            gameObject.SetActive(false);
        }
    }
}
