using System;
using Unity.Mathematics;
using UnityEngine;

public class Speedometer : MonoBehaviour
{
    [SerializeField] private CarMovement carMovement;
    [SerializeField] private Transform speedNeedlePivot;

    [SerializeField] private Vector3 minNeedleRot = new Vector3(0f, 60f, 0f);
    [SerializeField] private Vector3 maxNeedleRot = new Vector3(0f, -180f, 0f);
    

    [SerializeField] private float maxSpeed = 100;
    private float playerSpeed = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        playerSpeed = carMovement.GetKPH();
        UpdateNeedle();
    }

    void UpdateNeedle()
    {
        float speedRatio = Mathf.Clamp01(playerSpeed / maxSpeed);
        float val = Mathf.Lerp(60f, -180f, speedRatio);
        speedNeedlePivot.eulerAngles = new Vector3(0f, 0f, val);
        //math.lerp(minNeedleRot, maxNeedleRot, playerSpeed * Time.deltaTime);
    }
}
