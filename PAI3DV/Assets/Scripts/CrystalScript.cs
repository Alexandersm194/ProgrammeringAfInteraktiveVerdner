using System;
using System.Collections;
using UnityEngine;

public class CrystalScript : MonoBehaviour
{
    private Material crystalMat;
    private Color crystalColor;
    private Color newColor;

    private void Start()
    {
        crystalMat = GetComponent<MeshRenderer>().material;
        crystalColor = crystalMat.GetColor("_EmissionColor");
        newColor = crystalColor;
    }

    public void TakeDamage()
    {
        newColor = new Color(newColor.r + 0.4f, newColor.g - 0.4f, newColor.b - 0.4f, 1f);
        crystalMat.SetColor("_EmissionColor", newColor);;
        
    }
    
}
