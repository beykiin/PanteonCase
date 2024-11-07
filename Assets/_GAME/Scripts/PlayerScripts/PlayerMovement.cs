using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _rotate = 600f;
    [SerializeField] private float _jumpForce = 5f;
    [SerializeField] private LayerMask _groundLayer;

    private Rigidbody _rb;
    private Vector3 _moveDirect;
    private bool _isGrounded;

    private Animator _animator;


    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _animator= GetComponent<Animator>();
    }

    private void Update()
    {
        float _horizontal = Input.GetAxisRaw("Horizontal");
        float _vertical = Input.GetAxisRaw("Vertical");


        _moveDirect = new Vector3(_horizontal, 0, _vertical).normalized;


        if(_moveDirect.magnitude > 0)
        {
            Move();
            Rotate();
            _animator.SetFloat("Speed",_moveDirect.magnitude);
            Debug.Log(_rb.velocity.magnitude);
        }
        else
        {
            _animator.SetFloat("Speed", 0);
        }
        

        if (_isGrounded && Input.GetKeyDown(KeyCode.Space)) {
            Jump();
            
        }
        if (!_isGrounded)
        {
            _animator.SetBool("Jumping", true);
        }
        else
        {
            _animator.SetBool("Jumping", false);
        }
        


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
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            _isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            _isGrounded = false;
        }
    }

}
