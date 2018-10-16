using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour {

    public Animator animator;

    public float speed = 2f;
    public float jumpSpeed = 2f;

    public LayerMask layerMaskFloor;



    public bool grounded = false;

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

    private void FixedUpdate()
    {
        Move();

        if (grounded && input.jump)
        {
            Jump();
        }
    }

    void Move()
    {
        transform.Translate(new Vector3(input.horizontal, 0, input.vertical) * speed * Time.fixedDeltaTime);
    }

    void Jump()
    {
        rigidbody.AddForce(Vector3.up * jumpSpeed, ForceMode.Impulse);
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
