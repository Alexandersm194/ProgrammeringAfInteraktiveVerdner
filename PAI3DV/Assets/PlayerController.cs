using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;
    
    
    [Header("Physics")]
    [SerializeField] private float speed = 5f;
    [SerializeField] private float gravity = -9.81f * 2;
    [SerializeField] private float jumpHeight = 3f;
    
    [Header("Ground Checking")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundDistance = 0.4f;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private bool isActive = true;
    
    Vector3 velocity;
    
    bool isGrounded;
    bool isMoving;

    [Header("Effects")]
    [SerializeField] private ParticleSystem[] dustParticles;
    [SerializeField] private TrailRenderer[] wheelTrails;
    
    private Vector3 lastPosition = new Vector3(0f, 0f, 0f);
    
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        
        //controller = GetComponent<CharacterController>();
       
    }

    // Update is called once per frame
    void Update()
    {
        DrivingEffects();
        GroundedCheck();
        //MovingCheck();
        
    }

    private void MovingCheck()
    {
        if (lastPosition != gameObject.transform.position && isGrounded)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }

        lastPosition = gameObject.transform.position;
    }
    

    private void GroundedCheck()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
    }
    
    private void Jump()
    {
        velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
    }


    private void Move()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 direction = transform.forward;

        float turnSpeed = Mathf.Lerp(1f, 0.5f, Time.deltaTime * velocity.magnitude * 10f);
        
        if (x != 0)
        {
            if (z < 0)
            {
                direction = Vector3.Lerp(direction, transform.right * -x, Time.deltaTime * turnSpeed);
            }
            else
            {
                direction = Vector3.Lerp(direction, transform.right * x, Time.deltaTime * turnSpeed);
            }
            
        }
        
        Vector3 move = direction * z;

        float upSpeed = speed * Time.deltaTime;

        controller.Move(move * upSpeed);
        if (z < 0)
        {
            transform.localRotation = Quaternion.LookRotation(-move);
        }
        else if(z > 0)
        {
            transform.localRotation = Quaternion.LookRotation(move);
        }
        
        
    }

    private void DrivingEffects()
    {
        
        foreach (var dustParticle in dustParticles)
        {
            if(!dustParticle.isPlaying && isGrounded) dustParticle.Play();
            else if (dustParticle.isPlaying && !isGrounded) dustParticle.Stop();
        }

        foreach (var wheelTrail in wheelTrails)
        {
            if(!wheelTrail.emitting && isGrounded) wheelTrail.emitting = true;
            else if (wheelTrail.emitting && !isGrounded) wheelTrail.emitting = false;
        }
        
    }
}
