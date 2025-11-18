using UnityEngine;

public class GrenadeFlash : MonoBehaviour
{
    [SerializeField] private Material grenadeMaterial;
    
    [SerializeField] private float minIntensity;
    [SerializeField] private float maxIntensity;
    
    [SerializeField] private float intensityChangeAmount;
    
    [SerializeField] private bool intensityChange;

    private float rate;
    
    void Start()
    {
        grenadeMaterial = GetComponent<MeshRenderer>().material;
        rate = intensityChangeAmount;
    }
    
    void Update()
    {
        
        if (!intensityChange)
        { 
            Color baseColor = Color.red;
            float intensity = maxIntensity;      
            grenadeMaterial.SetColor("_EmissionColor", baseColor * intensity);
            grenadeMaterial.EnableKeyword("_EMISSION");
        }
        else if (intensityChange)
        {
            Color baseColor = Color.black; 
            float intensity = minIntensity;             
            grenadeMaterial.SetColor("_EmissionColor", baseColor * intensity);
            grenadeMaterial.EnableKeyword("_EMISSION");
        }
        
        rate -= Time.deltaTime;

        if (rate < 0)
        {
            intensityChange = !intensityChange;
            rate = intensityChangeAmount;
        }
        
        
        
    }
    
}
