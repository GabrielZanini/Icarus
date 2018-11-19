using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootArrow : MonoBehaviour {

    public GameObject arrowPrefab;
    public LayerMask layerMask;

    public Vector3 aimingPoint;

    public float minArrowSpeed = 1f;
    public float maxArrowSpeed = 50f;
    public float arrowSpeed = 0f;



    public float drawTime = 2f;
    public float drawCounter = 0f;


    public Transform archer;
    public Transform arrowSpawnner;
    public Transform bow;



    public bool canShoot = true;
    public bool isAiming = false;
    public bool changeRotation = false;


    PlayerInput _input;
    Vector3 _start;
    Vector3 _direction;

    RaycastHit hit;

    private void Start()
    {
        _input = GetComponent<PlayerInput>();
    }

    void Update ()
    {
        _start = Camera.main.transform.position;
        _direction = Camera.main.transform.forward;

        CastRay();


        if (!isAiming)
        {
            if (_input.fire.Down && canShoot)
            {
                isAiming = true;
                archer.localRotation = Quaternion.Euler(0f, 90f, 0f);
                drawCounter = 0;
            }
        }
        else
        {


            if (_input.fire.Up)
            {
                isAiming = false;
                archer.localRotation = Quaternion.Euler(0f, 0f, 0f);
                drawCounter = 0;

                Shoot();
            }
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
        }
        else
        {
            Debug.DrawRay(_start, _direction * 1000, Color.red);
            aimingPoint = _start + (_direction * 1000);
        }
    }

    void Shoot()
    {
        var arrow = (GameObject)Instantiate(arrowPrefab, arrowSpawnner.position, arrowSpawnner.rotation);
        var projectile = arrow.GetComponent<Projectile>();

        projectile.speed = arrowSpeed;
        projectile.speed = arrowSpeed;
    }

    void LookAtPoint()
    {
        if (changeRotation)
        {
            bow.LookAt(aimingPoint);
        }
    }
}
