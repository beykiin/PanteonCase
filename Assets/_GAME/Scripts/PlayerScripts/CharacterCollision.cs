using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CharacterCollision : MonoBehaviour
{
    [SerializeField] private float _knockbackForce = 10f;
    

    private Rigidbody _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("AI"))
        {
            Knockback(collision.contacts[0].normal);

        }

        if (collision.gameObject.TryGetComponent(out IInteractable interactable))
        {
            interactable.Interact();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out IInteractable interactable))
        {
            interactable.Interact();
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
