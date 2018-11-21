using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour {
    
    public InputButton fire;
    public InputButton aiming;
    public InputButton jump;
    public InputButton run;
    public InputButton goDown;

    public InputAxis vertical;
    public InputAxis horizontal;


    void Awake ()
    {
        fire = new InputButton("Fire1");
        aiming = new InputButton("Fire2");
        jump = new InputButton("Jump");
        goDown = new InputButton("GoDown");
        run = new InputButton("Run");
        
        vertical = new InputAxis("Vertical");
        horizontal = new InputAxis("Horizontal");
    }
}

public class InputButton
{
    private string _name;

    public bool Up {
        get {
            return Input.GetButtonUp(_name);
        }
    }
    public bool Down {
        get {
            return Input.GetButtonDown(_name);
        }
    }
    public bool Hold {
        get {
            return Input.GetButton(_name);
        }
    }


    public InputButton(string name)
    {
        _name = name;
    }

}

public class InputAxis
{
    private string _name;

    public float Smooth {
        get {
            return Input.GetAxis(_name);
        }
    }
    public float Raw {
        get {
            return Input.GetAxisRaw(_name);
        }
    }

    public InputAxis(string name)
    {
        _name = name;
    }
}
