using UnityEngine;

public class UIScript : MonoBehaviour
{
    [SerializeField] private GameObject DrivingUI;
    [SerializeField] private GameObject GunUI;

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
}
