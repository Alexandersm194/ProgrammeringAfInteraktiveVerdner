using Unity.VisualScripting;
using UnityEngine;

public class FactoryEvent : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameManager.FactoryEvent();
        }
    }
    
}
