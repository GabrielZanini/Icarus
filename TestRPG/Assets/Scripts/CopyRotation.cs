using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyRotation : MonoBehaviour {

    [SerializeField] Transform target;

    public bool copyX;
    public bool copyY;
    public bool copyZ;

    public UpdateType updateType = UpdateType.FixedUpdate;

    private Vector3 newRotation;

    void Update()
    {
        if (updateType == UpdateType.Update)
        {
            UpdateRotation();
        }
    }

    void FixedUpdate()
    {
        if (updateType == UpdateType.FixedUpdate)
        {
            UpdateRotation();
        }
    }

    void LateUpdate()
    {
        if (updateType == UpdateType.LateUpdate)
        {
            UpdateRotation();
        }
    }

    void UpdateRotation()
    {
        if (target == null)
            return;
                
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

    public void SetTarget(Transform newTransform)
    {
        target = newTransform;
    }
}

public enum UpdateType
{
    Update,
    FixedUpdate,
    LateUpdate
}
