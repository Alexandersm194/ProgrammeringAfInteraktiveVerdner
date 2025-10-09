using UnityEngine;

public class GunScript : MonoBehaviour
{
    
    public float mouseSensitivity = 100f;
    float xRotation = 0f;
    float yRotation = 0f;
    public bool isActive = false;

    [SerializeField] private Camera cam;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private ParticleSystem muzzleFlash;
    [SerializeField] private AudioSource gunSound;
    
    [SerializeField] private float fireRate = 15f;
    private float nextFire = 0f;
    
    [SerializeField] private float damage = 10f;
    
    
    
    
    [SerializeField] private GameObject impactEffect;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void SetRotation()
    {
        transform.rotation = Quaternion.Euler(0f, mainCamera.transform.eulerAngles.y, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            if(gunSound.isPlaying) gunSound.Stop();
        }
        if (!isActive) return;
        MoveGun();
        
        
        if(Input.GetMouseButton(0) && Time.time >= nextFire)
        {
            nextFire = Time.time + 1f / fireRate;
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
        if(gunSound.isPlaying == false) gunSound.Play();
        muzzleFlash.Play();
        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit))
        {
            if (hit.transform.CompareTag("Enemy"))
            {
                var enemy = hit.transform.gameObject.GetComponent<EnemyScript>();
                enemy.TakeDamage(damage);
            }
            
            GameObject impactObj = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactObj, 2f);
        }
        
    }
}
