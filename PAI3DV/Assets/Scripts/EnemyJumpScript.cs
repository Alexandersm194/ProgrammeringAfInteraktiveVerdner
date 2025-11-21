using UnityEngine;

public class EnemyJumpScript : MonoBehaviour
{
    [SerializeField] private GameObject bombPrefab;

    public void DropBomb()
    {
        GameObject bomb = Instantiate(bombPrefab, transform.position, Quaternion.identity);
        bomb.GetComponent<Rigidbody>().AddForce(-transform.up * 10f, ForceMode.Force);
    }
}
