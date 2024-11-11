using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Rigidbody))]
public class CharacterCollision : MonoBehaviour
{
    [SerializeField] private float _knockbackForce = 10f;
    [SerializeField] private AudioClip knockbackSound;


    private Rigidbody _rb;
    private CharacterMovement _characterMovement;
    private AudioSource _audioSource;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _characterMovement = GetComponent<CharacterMovement>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("AI"))
        {
            Knockback(collision.contacts[0].normal);

        }

        if (collision.gameObject.TryGetComponent(out IInteractable interactable))
        {
            interactable.Interact();
        }

        if (collision.gameObject.CompareTag("RotatingPlatform"))
        {
            _characterMovement.SetOnPlatform(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out IInteractable interactable))
        {
            interactable.Interact();
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
            PlayKnockbackSound();
        }
    }


    private void PlayKnockbackSound()
    {
        if (knockbackSound != null && _audioSource != null)
        {
            _audioSource.PlayOneShot(knockbackSound);
        }
    }



}
