using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootArrow : MonoBehaviour {

    public GameObject arrow;

    bool ready = false;

    RaycastHit hit;
	
	void Update ()
    {
		if (!ready && Input.GetButtonDown("Fire1"))
        {
            ready = true;
        }
        else if (ready && Input.GetButtonUp("Fire1"))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        Vector3 start = Camera.main.transform.position;
        Vector3 dir = Camera.main.transform.forward;
        
    }
}
