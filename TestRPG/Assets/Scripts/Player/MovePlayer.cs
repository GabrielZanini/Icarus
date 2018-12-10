using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour {


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
    SyncAnimation _syncAnimation;
    Rigidbody _rigidbody;
    RaycastHit _hitGround;
    ShootArrow _shootArrow;

    void Start ()
    {
        _input = GetComponent<PlayerInput>();
        _syncAnimation = GetComponent<SyncAnimation>();
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
            if (_input.run.Up || grounded)
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
            else if (_input.run.Down)
            {
                EnterFastFlightMode();
            }
        }
        else
        {
            if (grounded && _input.jump.Down)
            {
                Jump();
            }
            else if (!grounded && canFly && _input.jump.Down)
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
                if (_input.jump.Hold)
                {
                    GoUp();
                }
                else if (_input.goDown.Hold)
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
        transform.Translate(new Vector3(_input.horizontal.Smooth, 0, _input.vertical.Smooth) * speed * Time.fixedDeltaTime);
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
        _syncAnimation.isAiming = _shootArrow.isAiming;
        _syncAnimation.isGrounded = grounded;
        _syncAnimation.isFlyingFast = isFlyingFast;
        _syncAnimation.horizontal = _input.horizontal.Smooth;
        _syncAnimation.vertical = _input.vertical.Smooth;
    }
}
