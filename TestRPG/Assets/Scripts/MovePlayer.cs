using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour {

    public Animator animator;

    public float speed = 2f;
    public float flightSpeed = 2f;
    public float fastFlightSpeed = 2f;
    public float jumpSpeed = 2f;

    public LayerMask layerMaskFloor;

    public bool grounded = false;

    public bool canFly = false;

    public bool isFlying = false;
    public bool isFlyingFast = false;

    PlayerInput input;

    Rigidbody rigidbody;
    RaycastHit hitGround;
    ShootArrow shootArrow;

    void Start ()
    {
        input = GetComponent<PlayerInput>();
        rigidbody = GetComponent<Rigidbody>();
        shootArrow = GetComponent<ShootArrow>();
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
            if (!input.run || grounded)
            {
                ExitFastFlightMode();
            }
            else
            {
                transform.LookAt(shootArrow.aimingPoint);
            }
        }
        else if (isFlying)
        {
            if (grounded)
            {
                ExitFlightMode();
            }
            else if (input.run)
            {
                EnterFastFlightMode();
            }
        }
        else
        {
            if (grounded && input.jump_Down)
            {
                Jump();
            }
            else if (!grounded && canFly && input.jump_Down)
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
                if (input.jump)
                {
                    GoUp();
                }
                else if (input.goDown)
                {
                    GoDown();
                }
            }
        }
        
    }

    private void EnterFlightMode()
    {
        isFlying = true;
        rigidbody.velocity = Vector3.zero;
        rigidbody.useGravity = false;
    }

    private void ExitFlightMode()
    {
        isFlying = false;
        rigidbody.velocity = Vector3.zero;
        rigidbody.useGravity = true;
    }



    private void EnterFastFlightMode()
    {
        isFlyingFast = true;
        shootArrow.canShoot = false;
    }

    private void ExitFastFlightMode()
    {
        isFlyingFast = false;
        transform.rotation = Quaternion.Euler(Vector3.zero);
        shootArrow.canShoot = true;

        if (grounded)
        {
            ExitFlightMode();
        }
    }


    void Move()
    {
        transform.Translate(new Vector3(input.horizontal, 0, input.vertical) * speed * Time.fixedDeltaTime);
    }

    void FlightFast()
    {
        transform.Translate(Vector3.forward * fastFlightSpeed * Time.fixedDeltaTime);
    }

    void Jump()
    {
        rigidbody.AddForce(Vector3.up * jumpSpeed, ForceMode.Impulse);
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
        if (Physics.Raycast(transform.position, Vector3.down, out hitGround, 0.1f, layerMaskFloor))
        {
            return true;
        }
        else
        {
            return false;
        }

    }

    void Animate()
    {
        animator.SetBool("Aiming", shootArrow.isAiming);
        animator.SetBool("Grounded", grounded);
        animator.SetBool("FlyingFast", isFlyingFast);
        animator.SetFloat("Horizontal", input.horizontal);
        animator.SetFloat("Vertical", input.vertical);
    }
}
