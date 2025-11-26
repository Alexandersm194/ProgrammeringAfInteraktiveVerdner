using UnityEngine;

public class EnemyHealthBar : MonoBehaviour
{
    private GameObject playerObject;

    void Start()
    {
        playerObject = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        transform.LookAt(playerObject.transform.position);
    }
}
