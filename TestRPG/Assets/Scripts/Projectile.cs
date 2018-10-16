using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Vector3 targetPoint;
    public float speed = 1f;
    public bool falling = false;

    Rigidbody _rigidbody;
    bool _stop = false;

    void Start()
    {
        if (falling)
        {
            _rigidbody = GetComponent<Rigidbody>();
            _rigidbody.useGravity = true;
            _rigidbody.velocity = transform.forward * speed;
        }

        transform.LookAt(targetPoint);
    }

    void Update()
    {
        if (falling)
        {
            rotateOnFall();
        }
        else
        {
            MoveToTarget();
        }
    }

    void rotateOnFall()
    {
        transform.rotation = Quaternion.LookRotation(_rigidbody.velocity); 
    }

    void MoveToTarget()
    {
        if ((transform.position - targetPoint).sqrMagnitude > 0.3f && !_stop)
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (falling)
        {
            _rigidbody.useGravity = false;
            _rigidbody.velocity = Vector3.zero;
            falling = false;
        }

        _stop = true;

        if (other.tag == "Enemy")
        {
            Monoeye mono = other.gameObject.GetComponent<Monoeye>();

            if (mono != null)
            {
                mono.hp--;
            }

            Destroy(gameObject);
        }
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
    
}
