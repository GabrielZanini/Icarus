using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour {

    public bool fire = false;
    public bool aiming = false;
    public float vertical = 0f;
    public float horizontal = 0f;
    public bool jump = false;
    public bool jump_Down = false;
    public bool run = false;
    public bool goDown = false;

    void Start ()
    {
        Cursor.lockState = CursorLockMode.Locked;
	}
	
	void Update ()
    {
        fire = Input.GetButtonDown("Fire1");
        aiming = Input.GetButton("Fire2");

        vertical = Input.GetAxis("Vertical");
        horizontal = Input.GetAxis("Horizontal");
        jump = Input.GetButton("Jump");
        jump_Down = Input.GetButtonDown("Jump");
        goDown = Input.GetButton("GoDown");

    }
}
