using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    
    public float mouseSensitivity = 100f;
    float xRotation = 0f;
    float yRotation = 0f;

    public Vector3 offset;
    
    public GameObject camera;
    private Camera mainCam;
    [SerializeField] private Camera gunCam;
    [SerializeField] private GunScript gunScript;
    [SerializeField] private UIScript uiScript;

    private enum CameraState
    {
        main, gun
    }
    
    private CameraState state = CameraState.main;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        camera.transform.position = offset + gameObject.transform.position;
        camera.transform.LookAt(gameObject.transform.position);
        mainCam = camera.GetComponent<Camera>();
    }

    // Update is called once per frame
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
            camera.transform.localPosition = camera.transform.localPosition + new Vector3(0f, 0f, 2f);
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            camera.transform.localPosition = camera.transform.localPosition - new Vector3(0f, 0f, 2f);
        }
    }

    private void MoveCamera()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxisRaw("Mouse Y") * mouseSensitivity * Time.deltaTime;
        
        xRotation -= mouseY;
        yRotation += mouseX;
        
        xRotation = Mathf.Clamp(xRotation, -10f, 45f);
        transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);
    }
}
