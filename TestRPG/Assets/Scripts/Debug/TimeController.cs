using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController : MonoBehaviour
{
    [Range(0f,1f)]
    public float slowdown = 0.1f;

    bool _isSlowdown = false;

    void Update ()
    {
        if (Input.GetKey(KeyCode.P))
        {
            Time.timeScale = 0f;
        }

        if (Input.GetKey(KeyCode.O))
        {
            if (_isSlowdown)
            {
                _isSlowdown = false;
                Time.timeScale = 1f;
            }
            else
            {
                _isSlowdown = true;
                Time.timeScale = slowdown;
            }
            
        }
     
    }
}
