using System.Runtime.InteropServices;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class AI_Collision : MonoBehaviour
{
    [SerializeField] private float _knockbackForce = 10f;
    

    private Rigidbody _rb;
    private CharacterMovement _characterMovement;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _characterMovement = GetComponent<CharacterMovement>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("AI"))
        {
            Knockback(collision.contacts[0].normal);

        }

        if (collision.gameObject.CompareTag("RotatingPlatform"))
        {
            _characterMovement.SetOnPlatform(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("coin"))
        {
            Destroy(other.gameObject);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("RotatingPlatform"))
        {
            _characterMovement.SetOnPlatform(false);
        }
    }

    private void Knockback(Vector3 _knockbackDirect)
    {
        if(_rb != null)
        {
            _knockbackDirect.y = 0;
            _rb.AddForce(_knockbackDirect * _knockbackForce, ForceMode.Impulse);
        }
    }
}
