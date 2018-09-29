using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyRotation : MonoBehaviour {

    Transform target;

    public bool x;
    public bool y;
    public bool z;

    private Vector3 newRotation;

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {

        newRotation = transform.rotation;

    }
}
