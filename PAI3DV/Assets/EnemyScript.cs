using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    [Header("Enemy Stats")]
    [SerializeField] private float health = 100f;
    
    [Header("Animations")]
    [SerializeField] private Animator anim;
    [SerializeField] private AnimationClip clip;

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            anim.SetTrigger("Die");
            Destroy(gameObject, clip.length * 2);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            anim.SetTrigger("Jump");
        }
    }
}
