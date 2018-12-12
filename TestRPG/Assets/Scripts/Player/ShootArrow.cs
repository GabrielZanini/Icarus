using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ShootArrow : NetworkBehaviour {

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

    public PlayerCharacter character;
    PlayerInput _input;
    Vector3 _start;
    Vector3 _direction;
    Transform _cameraTranform;

    RaycastHit hit;

    private void Awake()
    {
        character = GetComponent<PlayerCharacter>();
    }

    private void Start()
    {
        if (!localPlayerAuthority)
            return;

        _input = GetComponent<PlayerInput>();
        _cameraTranform = Camera.main.transform;
    }

    void Update ()
    {
        if (!localPlayerAuthority)
            return;

        // Local Player

        _start = _cameraTranform.position;
        _direction = _cameraTranform.forward;

        CastRay();


        if (!isAiming)
        {
            if (_input.fire.Down && canShoot)
            {
                StartDrawing();
            }
        }
        else
        {
            if (_input.fire.Up)
            {
                //Shoot();

                CmdUpdateBow(bow.rotation);
                CmdShoot(draw, arrowSpawnner.position, arrowSpawnner.rotation);

                StopDrawing();
            }
            else if (_input.run.Down)
            {
                StopDrawing();
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

    void FixedUpdate()
    {
        if (hasAuthority)
        {
            CmdUpdateBow(bow.rotation);
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
    
    void StartDrawing()
    {
        isAiming = true;
    }

    void StopDrawing()
    {
        isAiming = false;
        drawCounter = 0;
        draw = 0;
    }

    void Shoot()
    {
        var arrow = (GameObject)Instantiate(arrowPrefab, arrowSpawnner.position, arrowSpawnner.rotation);
        var projectile = arrow.GetComponent<Projectile>();

        projectile.multiplier = draw;
    }

    [Command]
    void CmdShoot(float draw, Vector3 arrowPosition, Quaternion arrowRotation)
    {
        GameObject arrow = Instantiate(arrowPrefab, arrowPosition, arrowRotation);
        var projectile = arrow.GetComponent<Projectile>();
        projectile.multiplier = draw;
        projectile.shooter = character;

        NetworkServer.Spawn(arrow);        
    }

    void LookAtPoint()
    {
        if (changeRotation)
        {
            bow.LookAt(aimingPoint);
        }
    }

    [Command]
    void CmdUpdateBow(Quaternion newRotation)
    {
        bow.rotation = newRotation;
    }
}
