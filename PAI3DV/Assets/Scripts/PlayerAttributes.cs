using UnityEngine;

public class PlayerAttributes : MonoBehaviour
{
    [SerializeField] private float playerHealth = 100f;
    [SerializeField] private float playerDamage = 10f;
    [SerializeField] private UIScript uiScript;
    [SerializeField] private PlayerBehavior player;

    [SerializeField] private GameObject crystalBox;
    private CrystalScript[] crystalScript;
    
    public float getPlayerHealth() { return playerHealth; }
    public float getPlayerDamage() { return playerDamage; }

    void Start()
    {
        crystalScript = crystalBox.GetComponentsInChildren<CrystalScript>();
    }
    public void TakeDamage(float damage)
    {
        playerHealth -= damage;
        uiScript.UpdateHealth();

        foreach (CrystalScript s in crystalScript)
        {
            s.TakeDamage();
        }

        if (playerHealth <= 0)
        {
            uiScript.DeathScreen();
            player.OnFire();
        }
    }
    public void setPlayerDamage(float value) { playerDamage = value; }
}
