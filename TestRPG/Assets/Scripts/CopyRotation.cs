using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyRotation : MonoBehaviour {

    [SerializeField] Transform target;

    public bool copyX;
    public bool copyY;
    public bool copyZ;

    private Vector3 newRotation;


	void Update ()
    {

        newRotation = transform.rotation.eulerAngles;

        if (copyX)
        {
            newRotation.x = target.transform.rotation.eulerAngles.x;
        }

        if (copyY)
        {
            newRotation.y = target.transform.rotation.eulerAngles.y;
        }
        
        if (copyZ)
        {
            newRotation.z = target.transform.rotation.eulerAngles.z;
        }

        transform.rotation = Quaternion.Euler(newRotation);

    }
}
