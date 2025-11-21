using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    
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
    [SerializeField] private ParticleSystem fire;
    [SerializeField] private ParticleSystem smoke;
    
    

    // Update is called once per frame
    void Update()
    {
        DrivingEffects();
        GroundedCheck();
        
    }
    
    

    private void GroundedCheck()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
    }

    public void OnFire()
    {
        fire.Play();
        smoke.Play();
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
