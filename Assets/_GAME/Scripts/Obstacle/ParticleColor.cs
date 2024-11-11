using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleColor : MonoBehaviour
{
    [SerializeField] private ParticleSystem particleSystem;
    private ParticleSystem.MainModule mainModule;

    private void Awake()
    {
        
        mainModule = particleSystem.main;
    }

    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.collider.CompareTag("Player") || collision.collider.CompareTag("AI"))
        {
            
            ChangeParticleColor(Random.ColorHSV());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Player") || other.CompareTag("AI"))
        {
            
            ChangeParticleColor(Random.ColorHSV());
        }
    }

    private void ChangeParticleColor(Color color)
    {
        mainModule.startColor = color; 
    }
}
