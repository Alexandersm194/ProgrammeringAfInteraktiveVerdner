using UnityEngine;

public class GunScript : MonoBehaviour
{
    
    public float mouseSensitivity = 100f;
    float xRotation = 0f;
    float yRotation = 0f;
    public bool isActive = false;

    [SerializeField] private Camera cam;
    [SerializeField] private ParticleSystem muzzleFlash;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isActive) return;
        MoveGun();
        
        if(Input.GetMouseButtonDown(0))
        {
            ShootGun();
        }
    }
    
    private void MoveGun()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxisRaw("Mouse Y") * mouseSensitivity * Time.deltaTime;
        
        xRotation -= mouseY;
        yRotation += mouseX;
        
        xRotation = Mathf.Clamp(xRotation, -45f, 10f);
        transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);
    }

    private void ShootGun()
    {
        muzzleFlash.Play();
        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit))
        {
            Debug.Log(hit.transform.name);
        }
    }
}
