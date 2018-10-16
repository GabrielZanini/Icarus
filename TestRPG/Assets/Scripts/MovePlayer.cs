using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour {

    public Animator animator;

    public float speed = 2f;
    public float flightSpeed = 2f;
    public float jumpSpeed = 2f;

    public LayerMask layerMaskFloor;



    public bool grounded = false;

    public bool canFly = false;

    public bool isFlying = false;

    PlayerInput input;

    Rigidbody rigidbody;
    RaycastHit hitGround;

    void Start ()
    {
        input = GetComponent<PlayerInput>();
        rigidbody = GetComponent<Rigidbody>();
    }
	
	void Update ()
    {
        grounded = IsGrounded();

        Animate();
    }

    private void LateUpdate()
    {
        if (isFlying)
        {
            if (grounded)
            {
                ExitFlightMode();
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

    void Move()
    {
        transform.Translate(new Vector3(input.horizontal, 0, input.vertical) * speed * Time.fixedDeltaTime);
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
        animator.SetBool("Aiming", input.aiming);
        animator.SetFloat("Horizontal", input.horizontal);
        animator.SetFloat("Vertical", input.vertical);
    }
}
