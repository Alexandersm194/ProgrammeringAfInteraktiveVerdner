using UnityEngine;

public class PlayerAttributes : MonoBehaviour
{
    [SerializeField] private float playerHealth = 100f;
    [SerializeField] private float playerDamage = 10f;
    [SerializeField] private UIScript uiScript;
    
    
    
    public float getPlayerHealth() { return playerHealth; }
    public float getPlayerDamage() { return playerDamage; }

    public void TakeDamage(float damage)
    {
        playerHealth -= damage;
        uiScript.UpdateHealth();

        if (playerHealth <= 0)
        {
            uiScript.DeathScreen();
        }
    }
    public void setPlayerDamage(float value) { playerDamage = value; }
}
