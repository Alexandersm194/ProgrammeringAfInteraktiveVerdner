using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIScript : MonoBehaviour
{
    [SerializeField] private CarMovement carMovement;
    [SerializeField] private PlayerAttributes playerAttributes;
    
    [SerializeField] private GameObject DrivingUI;
    [SerializeField] private GameObject GunUI;

    [SerializeField] private TextMeshProUGUI speedText;
    [SerializeField] private TextMeshProUGUI healthText;
    
    
    [Header("DeathUI Elements")]
    [SerializeField] private GameObject DeathUI;
    [SerializeField] private Animator DeathUIAnim;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button exitButton;

    void Start()
    {
        restartButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        });
        
        
        exitButton.onClick.AddListener(() =>
        {
            Application.Quit();
        });

    }
    void Update()
    {
        UpdateUIText();
    }

    public void UpdateHealth()
    {
        healthText.text = playerAttributes.getPlayerHealth() + " HP";
    }

    public void ActivateDrivingUI()
    {
        GunUI.SetActive(false);
    }

    public void ActivateGunUI()
    {
        GunUI.SetActive(true);
    }

    private void UpdateUIText()
    {
        speedText.text = carMovement.GetKPH().ToString();
    }

    public void DeathScreen()
    {
        
        Cursor.lockState = CursorLockMode.Confined;
        DeathUI.gameObject.SetActive(true);
        DeathUIAnim.SetBool("Dead", true);
    }
    
    
    
    
    
    
}
