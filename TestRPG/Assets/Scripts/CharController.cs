using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharController : MonoBehaviour
{
    public Animator animator;
    public float directionDampTime;
    public float speed = 6.0f;
    public float h = 0.0f;
    public float v = 0.0f;
    public bool attack1 = false;
    public bool attack2 = false;
    public bool attack3 = false;
    public bool jump = false;
    public bool die = false;

    private Vector3 moveDirection = Vector3.zero;
    private Rigidbody rigidbody;



    void Start()
    {
        this.animator = GetComponentInChildren<Animator>() as Animator;
        this.rigidbody = GetComponent<Rigidbody>();
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            this.attack1 = true;
            //this.GetComponent<IKHandle>().enabled = false;
        }
        else if (Input.GetKeyUp(KeyCode.C))
        {
            this.attack1 = false;
            //this.GetComponent<IKHandle>().enabled = true;
        }
        animator.SetBool("Attack1", attack1);

        if (Input.GetKeyDown(KeyCode.Z))
        {
            this.attack2 = true;
            //this.GetComponent<IKHandle>().enabled = false;
        }
        else if (Input.GetKeyUp(KeyCode.Z))
        {
            this.attack2 = false;
            //this.GetComponent<IKHandle>().enabled = true;
        }
        animator.SetBool("Attack2", attack2);

        if (Input.GetKeyDown(KeyCode.X))
        {
            this.attack3 = true;
            //this.GetComponent<IKHandle>().enabled = false;
        }
        else if (Input.GetKeyUp(KeyCode.X))
        {
            this.attack3 = false;
            //this.GetComponent<IKHandle>().enabled = true;
        }
        animator.SetBool("Attack3", attack3);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            this.jump = true;
            //this.GetComponent<IKHandle>().enabled = false;
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            this.jump = false;
            //this.GetComponent<IKHandle>().enabled = true;
        }

        animator.SetBool("Jump", jump);

        if (Input.GetKeyDown(KeyCode.I))
        {
            this.die = true;
            SendMessage("Died");
        }
        animator.SetBool("Die", die);
    }

    void FixedUpdate()
    {
        // The Inputs are defined in the Input Manager
        
        // get value for horizontal axis
        h = Input.GetAxis("Horizontal");
        
        // get value for vertical axis
        v = Input.GetAxis("Vertical");

        speed = new Vector2(h, v).sqrMagnitude;
        
        // Used to get values on console
        //Debug.Log(string.Format("H:{0} - V:{1} - Speed:{2}", h, v, speed));

        animator.SetFloat("Speed", speed);
        animator.SetFloat("Horizontal", h);
        animator.SetFloat("Vertical", v);

        
    }


}
