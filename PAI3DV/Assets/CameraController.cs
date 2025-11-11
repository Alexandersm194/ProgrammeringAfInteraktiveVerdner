using System;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Input")]
    public float mouseSensitivity = 100f;
    float xRotation = 0f;
    float yRotation = 0f;
    [SerializeField] private Vector3 offset;
    [SerializeField] private GameObject target;
    
    
    [Header("Cameras")]
    [SerializeField] private GameObject camera;
    [SerializeField] private Camera gunCam;
    private Camera mainCam;
    
    [Header("Used Scripts")]
    [SerializeField] private GunScript gunScript;
    [SerializeField] private UIScript uiScript;
    
    [SerializeField] private float YMin = -45.0f;
    [SerializeField] private float YMax = 10.0f;

    public Transform lookAt;

    public float distance = 10.0f;
    private float currentX = 0.0f;
    private float currentY = 0.0f;

    private enum CameraState
    {
        main, gun
    }
    
    private CameraState state = CameraState.main;
    
    void Start()
    {
        camera.transform.position = offset + gameObject.transform.position;
        camera.transform.LookAt(gameObject.transform.position);
        mainCam = camera.GetComponent<Camera>();
    }
    
    void Update()
    {
        UpdateCam();
        
        if(Input.GetAxis("Mouse ScrollWheel") != 0) ScrollCamera();
        
        if (Input.GetMouseButton(1))
        {
            state = CameraState.gun;
        }
        else
        {
            state = CameraState.main;
        }

        if (Input.GetMouseButtonDown(1))
        {
            gunScript.SetRotation();
        }
        
        
        
    }
    private void UpdateCam()
    {
        switch (state)
        {
            case CameraState.main:
                uiScript.ActivateDrivingUI();
                gunCam.gameObject.SetActive(false);
                mainCam.gameObject.SetActive(true);
                gunScript.isActive = false;
                MoveCamera();
                break;
            case CameraState.gun:
                uiScript.ActivateGunUI();
                gunCam.gameObject.SetActive(true);
                mainCam.gameObject.SetActive(false);
                gunScript.isActive = true;
                break;
        }
    }
    private void ScrollCamera()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            distance += 2f;
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            distance -= 2f;
        }
    }

    private void MoveCamera()
    {
        currentX += Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        currentY += Input.GetAxis("Mouse Y") * -mouseSensitivity * Time.deltaTime;

        currentY = Mathf.Clamp(currentY, YMin, YMax);

        Vector3 Direction = new Vector3(0, 0, -distance);
        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
        camera.transform.position = lookAt.position + rotation * Direction;

        camera.transform.LookAt(lookAt.position);
        
        
    }
}
