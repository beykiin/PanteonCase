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
        if (collision.gameObject.CompareTag("Wall"))
        {
            Knockback(collision.contacts[0].normal);


            WallBounce wallBounce = collision.gameObject.GetComponent<WallBounce>();
            if (wallBounce != null)
            {
                wallBounce.BounceEffect();
            }
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
