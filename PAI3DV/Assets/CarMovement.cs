using Unity.VisualScripting;
using UnityEngine;

public class CarMovement : MonoBehaviour
{   
    public WheelCollider[] wheel_col;
    public Transform[] wheels;
    
    [SerializeField] private float torque = 100;
    private float angle = 45;
    void Update()
    {
        for (int i = 0; wheels.Length > i; i++)
        {
            wheel_col[i].motorTorque = Input.GetAxis("Vertical") * torque;
            if (i == 0 || i == 1)
            {
                wheel_col[i].steerAngle = Input.GetAxis("Horizontal") * angle;
            }

            var pos = transform.position;
            var rot = transform.rotation;
            wheel_col[i].GetWorldPose(out pos, out rot);
            wheels[i].position = pos;
            wheels[i].rotation = rot;
        } 
    }
}
