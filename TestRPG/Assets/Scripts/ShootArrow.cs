using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootArrow : MonoBehaviour {

    public GameObject arrowPrefab;
    public LayerMask layerMask;

    public Vector3 aimingPoint;

    public float arrowSpeed = 1f;

    public Transform archer;
    public Transform arrowSpawnner;
    public Transform bow;



    public bool canShoot = true;
    public bool isAiming = false;
    public bool changeRotation = false;


    PlayerInput input;
    bool _ready = false;
    Vector3 _start;
    Vector3 _direction;

    RaycastHit hit;

    private void Start()
    {
        input = GetComponent<PlayerInput>();
    }

    void Update ()
    {
        _start = Camera.main.transform.position;
        _direction = Camera.main.transform.forward;

        CastRay();

        if (input.aiming && canShoot)
        {
            isAiming = true;
            archer.localRotation = Quaternion.Euler(0f, 90f, 0f);

            if (input.fire)
            {
                Shoot();
            }
        }
        else
        {
            isAiming = false;
            archer.localRotation = Quaternion.Euler(0f, 0f, 0f);
        }        
    }

    void LateUpdate()
    {
        LookAtPoint();
    }
    

    void CastRay()
    {
        if (Physics.Raycast(_start, _direction, out hit, Mathf.Infinity, layerMask))
        {
            Debug.DrawRay(_start, _direction * hit.distance, Color.green);
            aimingPoint = hit.point;
            _ready = true;
        }
        else
        {
            Debug.DrawRay(_start, _direction * 1000, Color.red);
            aimingPoint = _start + (_direction * 1000);
            _ready = false;
        }
    }

    void Aim()
    {
        
    }

    void Shoot()
    {
        var arrow = (GameObject)Instantiate(arrowPrefab, arrowSpawnner.position, arrowSpawnner.rotation);
        var projectile = arrow.GetComponent<Projectile>();

        projectile.speed = arrowSpeed;
        projectile.targetPoint = aimingPoint;

        if (!_ready)
        {         
            projectile.falling = true;
        }

    }

    void LookAtPoint()
    {
        if (changeRotation)
        {
            bow.LookAt(aimingPoint);
        }
    }
}
