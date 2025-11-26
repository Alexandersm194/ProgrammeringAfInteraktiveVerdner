using UnityEngine;

public class SpawnLastBikers : MonoBehaviour
{
    [SerializeField] private SpawnBikers script;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            script.Spawn3rdPhase();
            gameObject.SetActive(false);
        }
    }
}
