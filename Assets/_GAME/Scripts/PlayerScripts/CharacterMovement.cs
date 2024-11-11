using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _rotate = 600f;
    [SerializeField] private float _jumpForce = 5f;
    [SerializeField] private VirtualJoystick virtualJoyistick;
    [SerializeField] private AudioClip knockbackSound;

    private Rigidbody _rb;
    private Vector3 _moveDirect;
    private Animator _animator;
    private bool isRaceFinished = false;
    private AudioSource _audioSource;



    public float sphereRadius = 0.5f;
    public LayerMask groundLayer;
    public bool isGrounded; 
    public Transform groundCheckPosition;

    private Vector3 startPosition;

    private RotatingPlatformScript _rotatingPlatformScript;
    private bool _onPlatform = false;





    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        _rotatingPlatformScript = FindObjectOfType<RotatingPlatformScript>();
        _audioSource = GetComponent<AudioSource>();
    }
    public void SetOnPlatform(bool value)
    {
        _onPlatform = value;
    }

    private void Start()
    {
        startPosition = transform.position;
        if (groundCheckPosition == null)
        {
            Debug.LogError("Null Referance groundCheckPosition", gameObject);
        }

    }

    private void Update()
    {
        if (isRaceFinished) return;

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
        if (isRaceFinished) return;
        //float horizontal = Input.GetAxisRaw("Horizontal");
        //float vertical = Input.GetAxisRaw("Vertical");

        float horizontal = virtualJoyistick.GetAxisRaw("Horizontal");
        float vertical = virtualJoyistick.GetAxisRaw("Vertical");


        _moveDirect = new Vector3(horizontal, 0, vertical).normalized;
        


        if (_moveDirect.magnitude > 0)
        {
            Move();
            Rotate();
            _animator.SetFloat("Speed", _moveDirect.magnitude);

        }
        else
        {
            _animator.SetFloat("Speed", 0);
        }

        if (_onPlatform)
        {
            ApplyRotationForce();
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
        PlayJumpSound();
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


    public void ResetPlayer()
    {
        if (!isRaceFinished)
        {
            transform.position = startPosition;
            _rb.velocity = Vector3.zero;
            _animator.SetFloat("Speed", 0);
        }
    }

    private void ApplyRotationForce()
    {
        if (_rotatingPlatformScript != null)
        {

            float rotationSpeed = 3f;

            
            Vector3 forceDirection = transform.right * rotationSpeed;

            
            if (_moveDirect.magnitude > 0)
            {
                _rb.AddForce(-forceDirection * _speed * Time.deltaTime, ForceMode.VelocityChange);  
            }
        }
    }

    public void StopMovement()
    {
        isRaceFinished = true;  
        _animator.SetFloat("Speed", 0);
    }

    private void PlayJumpSound()
    {
        if (knockbackSound != null && _audioSource != null)
        {
            _audioSource.PlayOneShot(knockbackSound);
        }
    }

}
