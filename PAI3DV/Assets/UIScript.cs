using TMPro;
using Unity.Mathematics;
using UnityEngine;

public class UIScript : MonoBehaviour
{
    [SerializeField] private CarMovement carMovement;
    
    [SerializeField] private GameObject DrivingUI;
    [SerializeField] private GameObject GunUI;

    [SerializeField] private TextMeshProUGUI speedText;
    
    void Update()
    {
        UpdateUIText();
    }

    public void ActivateDrivingUI()
    {
        DrivingUI.SetActive(true);
        GunUI.SetActive(false);
    }

    public void ActivateGunUI()
    {
        DrivingUI.SetActive(false);
        GunUI.SetActive(true);
    }

    private void UpdateUIText()
    {
        speedText.text = carMovement.GetKPH().ToString();
    }
    
    
}
