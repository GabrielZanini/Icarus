using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnparrentAtStart : MonoBehaviour {

	void Start ()
    {	
        if (transform.parent != null)
        {
            transform.parent = null;
        }
	}
}
