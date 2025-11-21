using UnityEngine;

public class TransportScript : MonoBehaviour
{
    [SerializeField] private Animator animator;
    
    [SerializeField] private GameObject crystalBox;
    [SerializeField] private GameObject fuelPod;


    public void CrystalBoxUp()
    {
        animator.SetTrigger("CrystalBox");
    }
    
    public void FuelPodDown()
    {
        fuelPod.gameObject.SetActive(true);
        crystalBox.gameObject.SetActive(false);
        animator.SetTrigger("FuelPod");
    }
}
