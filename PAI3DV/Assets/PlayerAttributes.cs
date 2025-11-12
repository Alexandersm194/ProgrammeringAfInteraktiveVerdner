using UnityEngine;

public class PlayerAttributes : MonoBehaviour
{
    [SerializeField] private float playerHealth = 100f;
    [SerializeField] private float playerDamage = 10f;
    
    
    
    
    public float getPlayerHealth() { return playerHealth; }
    public float getPlayerDamage() { return playerDamage; }
    public void setPlayerHealth(float value) { playerHealth = value; }
    public void setPlayerDamage(float value) { playerDamage = value; }
}
