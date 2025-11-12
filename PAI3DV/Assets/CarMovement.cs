using System;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class CarMovement : MonoBehaviour
{   
    public WheelCollider[] wheel_col;
    public Transform[] wheels;
    
    [SerializeField] private float torque = 100;
    [SerializeField] private float downforceValue = 100f;
    [SerializeField] private float steerAngle = 45;
    [SerializeField] private float radius = 6f;
    [SerializeField] private float maxSpeed = 100f;
    [SerializeField] private float KPH = 100;
    private float horizontal;
    private float vertical;
    private Rigidbody rb;

    private enum driveState
    {
        rearWheelDrive, allWheelDrive
    }

    [SerializeField] private driveState drive = driveState.allWheelDrive;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    
    void FixedUpdate()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        AddDownforce();
        Steer();
        Drive();
        for (int i = 0; wheels.Length > i; i++)
        {
            var pos = transform.position;
            var rot = transform.rotation;
            wheel_col[i].GetWorldPose(out pos, out rot);
            wheels[i].position = pos;
            wheels[i].rotation = rot;
        }

        if (Input.anyKeyDown)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                Reset();
            }
            
            if (Input.GetKeyDown(KeyCode.Space))
            {
                foreach (var i in wheel_col)
                {
                    i.brakeTorque = 7000;
                }
            }
            else
            {
                foreach (var i in wheel_col)
                {
                    i.brakeTorque = 0;
                }
            }
        }
        
    }

    private void Drive()
    {
        KPH = rb.linearVelocity.magnitude * 3.6f;
        if(KPH < maxSpeed)
        {
            if (drive == driveState.allWheelDrive)
            {
                for (var i = 0; i < wheels.Length; i++)
                {
                    wheel_col[i].motorTorque = vertical * (torque / 4);
                }
            }
            else if (drive == driveState.rearWheelDrive)
            {
                for (var i = 2; i < wheels.Length; i++)
                {
                    wheel_col[i].motorTorque = vertical * (torque / 2);
                }
            }
        }
        else
        {
            for (var i = 0; i < wheels.Length; i++)
            {
                wheel_col[i].motorTorque = 0;
            }
        }
       
    }

    private void Steer()
    {
        if (horizontal > 0)
        {
            wheel_col[0].steerAngle = Mathf.Rad2Deg * Mathf.Atan(2.55f / (radius + (1.5f/2))) * horizontal;
            wheel_col[1].steerAngle = Mathf.Rad2Deg * Mathf.Atan(2.55f / (radius - (1.5f/2))) * horizontal;
        }
        else if (horizontal < 0)
        {
            wheel_col[0].steerAngle = Mathf.Rad2Deg * Mathf.Atan(2.55f / (radius - (1.5f/2))) * horizontal;
            wheel_col[1].steerAngle = Mathf.Rad2Deg * Mathf.Atan(2.55f / (radius + (1.5f/2))) * horizontal;
        }
        else
        {
            wheel_col[0].steerAngle = 0;
            wheel_col[1].steerAngle = 0;
        }
    }

    void AddDownforce()
    {
        rb.AddForce(-transform.up * downforceValue * rb.linearVelocity.magnitude);
    }

    public int GetKPH()
    {
        float KPHFloor = math.floor(KPH);
        return System.Convert.ToInt32(KPHFloor);
    }

    private void Reset()
    {
        transform.position = transform.position + new Vector3(0, 2f, 0);
        transform.localRotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0); 
    }
}
