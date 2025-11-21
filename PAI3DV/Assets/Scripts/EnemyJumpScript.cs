using System.Collections;
using UnityEngine;

public class EnemyJumpScript : MonoBehaviour
{
    [SerializeField] private GameObject bombPrefab;

    public void DropBomb()
    {
        GameObject bomb = Instantiate(bombPrefab, transform.position, Quaternion.identity);
        bomb.GetComponent<Rigidbody>().AddForce(-transform.up * 50f, ForceMode.Force);
    }
    
    private IEnumerator SlowDownTime()
    {
        Time.timeScale = 0.1f;
        yield return new WaitForSeconds(0.1f);
        Time.timeScale = 1;
    }
}
