using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour {

    public Animator animator;

    public float speed = 2f;
    public float flightSpeed = 2f;
    public float fastFlightSpeed = 2f;
    public float jumpSpeed = 2f;

    public LayerMask groundMask;

    public bool grounded = false;

    public bool canFly = false;

    public bool isFlying = false;
    public bool isFlyingFast = false;

    PlayerInput _input;

    Rigidbody _rigidbody;
    RaycastHit _hitGround;
    ShootArrow _shootArrow;

    void Start ()
    {
        _input = GetComponent<PlayerInput>();
        _rigidbody = GetComponent<Rigidbody>();
        _shootArrow = GetComponent<ShootArrow>();
    }
	
	void Update ()
    {
        grounded = IsGrounded();

        Animate();
    }

    private void LateUpdate()
    {
        if (isFlyingFast)
        {
            if (!_input.run || grounded)
            {
                ExitFastFlightMode();
            }
            else
            {
                transform.LookAt(_shootArrow.aimingPoint);
            }
        }
        else if (isFlying)
        {
            if (grounded)
            {
                ExitFlightMode();
            }
            else if (_input.run)
            {
                EnterFastFlightMode();
            }
        }
        else
        {
            if (grounded && _input.jump_Down)
            {
                Jump();
            }
            else if (!grounded && canFly && _input.jump_Down)
            {
                EnterFlightMode();
            }
        }
    }

    private void FixedUpdate()
    {
        if (isFlyingFast)
        {
            FlightFast();
        }
        else
        {
            Move();

            if (isFlying)
            {
                if (_input.jump)
                {
                    GoUp();
                }
                else if (_input.goDown)
                {
                    GoDown();
                }
            }
        }
        
    }

    private void EnterFlightMode()
    {
        isFlying = true;
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.useGravity = false;
    }

    private void ExitFlightMode()
    {
        isFlying = false;
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.useGravity = true;
    }



    private void EnterFastFlightMode()
    {
        isFlyingFast = true;
        _shootArrow.canShoot = false;
    }

    private void ExitFastFlightMode()
    {
        isFlyingFast = false;
        transform.rotation = Quaternion.Euler(Vector3.zero);
        _shootArrow.canShoot = true;

        if (grounded)
        {
            ExitFlightMode();
        }
    }


    void Move()
    {
        transform.Translate(new Vector3(_input.horizontal, 0, _input.vertical) * speed * Time.fixedDeltaTime);
    }

    void FlightFast()
    {
        transform.Translate(Vector3.forward * fastFlightSpeed * Time.fixedDeltaTime);
    }

    void Jump()
    {
        _rigidbody.AddForce(Vector3.up * jumpSpeed, ForceMode.Impulse);
    }


    void GoUp()
    {
        transform.Translate(Vector3.up * flightSpeed * Time.fixedDeltaTime);
    }

    void GoDown()
    {
        transform.Translate(Vector3.down * flightSpeed * Time.fixedDeltaTime);
    }

    bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, out _hitGround, 0.1f, groundMask);
    }

    void Animate()
    {
        animator.SetBool("Aiming", _shootArrow.isAiming);
        animator.SetBool("Grounded", grounded);
        animator.SetBool("FlyingFast", isFlyingFast);
        animator.SetFloat("Horizontal", _input.horizontal);
        animator.SetFloat("Vertical", _input.vertical);
    }
}
