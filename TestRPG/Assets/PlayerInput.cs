using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour {

    public bool fire = false;
    public bool aiming = false;
    public float vertical = 0f;
    public float horizontal = 0f;
    public bool jump = false;

    void Start () {
		
	}
	
	void Update ()
    {
        fire = Input.GetButtonDown("Fire1");
        aiming = Input.GetButton("Fire2");

        vertical = Input.GetAxis("Vertical");
        horizontal = Input.GetAxis("Horizontal");
        jump = Input.GetButton("Jump");

    }
}
