using System;
using UnityEngine;

public class GateOpeningScript : MonoBehaviour
{
    [Header("Gate Objects")]
    [SerializeField] private  GameObject GateLeft;
    [SerializeField] private  GameObject GateRight;

    [Header("Rotation adjustments")]
    [SerializeField] private float openRange = 90f;  
    [SerializeField] private float openSpeed = 1f;    
    

    private float leftT = 0f;
    private float rightT = 0f;

    
    // Gate states
    private enum GateState { Open, Closed, Opening, Closing }
    private GateState state = GateState.Closed;
    
    // Desired Gate Rotations:
    private Quaternion leftClosedRot;
    private Quaternion rightClosedRot;
    private Quaternion leftOpenRot;
    private Quaternion rightOpenRot;
    private void Start()
    {
        leftClosedRot = GateLeft.transform.localRotation;
        rightClosedRot = GateRight.transform.localRotation;
        
        leftOpenRot = leftClosedRot * Quaternion.Euler(0f, openRange, 0f);
        rightOpenRot = rightClosedRot * Quaternion.Euler(0f, -openRange, 0f);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            if (state == GateState.Closed) state = GateState.Opening;
            else if (state == GateState.Open) state = GateState.Closing;
        }

        switch (state)
        {
            case GateState.Opening:
                leftT += openSpeed * Time.deltaTime;
                rightT += openSpeed * Time.deltaTime;

                GateLeft.transform.localRotation = Quaternion.Lerp(leftClosedRot, leftOpenRot, leftT);
                GateRight.transform.localRotation = Quaternion.Lerp(rightClosedRot, rightOpenRot, rightT);

                if (Quaternion.Angle(GateLeft.transform.localRotation, leftOpenRot) < 0.5f &&
                    Quaternion.Angle(GateRight.transform.localRotation, rightOpenRot) < 0.5f)
                {
                    state = GateState.Open;
                }
                break;

            case GateState.Closing:
                leftT -= openSpeed * Time.deltaTime;
                rightT -= openSpeed * Time.deltaTime;

                GateLeft.transform.localRotation = Quaternion.Lerp(leftClosedRot, leftOpenRot, leftT);
                GateRight.transform.localRotation = Quaternion.Lerp(rightClosedRot, rightOpenRot, rightT);

                if (Quaternion.Angle(GateLeft.transform.localRotation, leftClosedRot) < 0.5f &&
                    Quaternion.Angle(GateRight.transform.localRotation, rightClosedRot) < 0.5f)
                {
                    state = GateState.Closed;
                }
                break;
        }
        
        leftT = Mathf.Clamp01(leftT);
        rightT = Mathf.Clamp01(rightT);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            state = GateState.Opening;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            state = GateState.Closing;
        }
    }
}