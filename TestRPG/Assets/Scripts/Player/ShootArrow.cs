using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootArrow : MonoBehaviour {

    public GameObject arrowPrefab;
    public LayerMask layerMask;

    public Vector3 aimingPoint;

    [Header("Transforms")]

    public float arrowSpeed = 0f;


    [Header("Draw")]
    public float drawTime = 2f;
    public float drawCounter = 0f;
    public float draw = 0f;


    [Header("Transforms")]
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
            }
        }
        else
        {
            if (_input.fire.Up)
            {
                Shoot();

                isAiming = false;
                archer.localRotation = Quaternion.Euler(0f, 0f, 0f);
                drawCounter = 0;
                draw = 0;
            }
            else
            {
                if (drawCounter < drawTime)
                {
                    drawCounter += Time.deltaTime;
                }

                draw = drawCounter / drawTime;
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

        projectile.multiplier = draw;
    }

    void LookAtPoint()
    {
        if (changeRotation)
        {
            bow.LookAt(aimingPoint);
        }
    }
}
