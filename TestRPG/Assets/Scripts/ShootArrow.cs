using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootArrow : MonoBehaviour {

    public GameObject arrow;
    public LayerMask layerMask;

    public Vector3 aimingPoint;

    bool ready = false;
    Vector3 start;
    Vector3 direction;

    RaycastHit hit;
	
	void Update ()
    {
        start = Camera.main.transform.position;
        direction = Camera.main.transform.forward;

        if (Input.GetButton("Fire1"))
        {
            Aim();
            
        }
        else if (ready && Input.GetButtonUp("Fire1"))
        {
            Shoot();
        }
    }

    void Aim()
    {
        if (Physics.Raycast(start, direction, out hit, Mathf.Infinity, layerMask))
        {
            Debug.DrawRay(start, direction * hit.distance, Color.green);
            aimingPoint = hit.point;
            ready = true;
        }
        else
        {
            Debug.DrawRay(start, direction * 1000, Color.red);

            ready = false;
        }

        ready = true;
    }

    void Shoot()
    {      

        if (Physics.Raycast(start, direction, out hit, Mathf.Infinity, layerMask))
        {
            Debug.DrawRay(start, direction * hit.distance, Color.yellow);
            aimingPoint = hit.point;
        }
        else
        {

        }

    }
}
