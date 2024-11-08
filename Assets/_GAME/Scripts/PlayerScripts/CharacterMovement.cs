using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _rotate = 600f;
    [SerializeField] private float _jumpForce = 5f;

    private Rigidbody _rb;
    private Vector3 _moveDirect;
    private Animator _animator;


    public float sphereRadius = 0.5f;
    public LayerMask groundLayer;
    public bool isGrounded; 
    public Transform groundCheckPosition;


    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {

        if (groundCheckPosition == null)
        {
            Debug.LogError("Null Referance groundCheckPosition", gameObject);
        }

    }

    private void Update()
    {
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
#if UNITY_EDITOR
        DebugDrawSphere();
#endif
    }

    private void FixedUpdate()
    {
        float _horizontal = Input.GetAxisRaw("Horizontal");
        float _vertical = Input.GetAxisRaw("Vertical");


        _moveDirect = new Vector3(_horizontal, 0, _vertical).normalized;


        if (_moveDirect.magnitude > 0)
        {
            Move();
            Rotate();
            _animator.SetFloat("Speed", _moveDirect.magnitude);
            Debug.Log(_rb.velocity.magnitude);
        }
        else
        {
            _animator.SetFloat("Speed", 0);
        }

        CheckGround();
    }


    private void Move()
    {
        if (_moveDirect.magnitude == 0)
        {
            _rb.velocity = Vector3.zero;
        }
        else
        {
            Vector3 movement = _moveDirect * _speed * Time.deltaTime;
            _rb.MovePosition(transform.position + movement);
        }

    }

    private void Rotate()
    {
        if (_moveDirect.magnitude > 0)
        {
            Quaternion rotation = Quaternion.LookRotation(_moveDirect);
            _rb.MoveRotation(Quaternion.RotateTowards(transform.rotation, rotation, _rotate * Time.deltaTime));
        }
    }

    private void Jump()
    {
        _rb.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
        _animator.SetTrigger("Jump");
    }

    private void CheckGround()
    {
        isGrounded = Physics.CheckSphere(groundCheckPosition.position, sphereRadius, groundLayer);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = isGrounded ? Color.green : Color.red;

        Gizmos.DrawWireSphere(groundCheckPosition.position, sphereRadius);
    }

#if UNITY_EDITOR
    private void DebugDrawSphere()
    {
        Debug.DrawLine(groundCheckPosition.position - Vector3.up * sphereRadius, groundCheckPosition.position + Vector3.up * sphereRadius, isGrounded ? Color.green : Color.red);
    }
#endif

}
