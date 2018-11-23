using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("Movement")]
    public float minSpeed = 1f;
    public float maxSpeed = 50f;

    [Header("Gravity")]
    public float minNoGravityTime = 0.1f;
    public float maxNoGravityTime = 0.5f;

    [Header("Impact")]
    public Vector3 targetPoint;
    public int damage = 5;    
    public bool hitted = false;

    [Header("Shooter")]
    public GameObject Shooter;
    public float multiplier = 1f;

    Rigidbody _rigidbody;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
                
        StartCoroutine(AddPhisics());
    }

    private void FixedUpdate()
    {
        if (hitted)
            return;

        transform.rotation = Quaternion.LookRotation(_rigidbody.velocity);
    }

    private void OnTriggerEnter(Collider other)
    {
        _rigidbody.useGravity = false;
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.isKinematic = true;

        hitted = true;

        if (other.tag == "Enemy")
        {
            Enemy enemy = other.gameObject.GetComponent<Enemy>();

            if (enemy != null)
            {
                enemy.TakeDamage(damage);
                gameObject.transform.parent = enemy.transform;
            }

            //Destroy(gameObject);
        }
    }

    IEnumerator AddPhisics()
    {
        float speed = minSpeed + ((maxSpeed - minSpeed) * multiplier);
        float noGravityTime = minNoGravityTime + ((maxNoGravityTime - minNoGravityTime) * multiplier);

        _rigidbody.velocity = transform.forward * speed;

        yield return new WaitForSeconds(noGravityTime);

        if (!hitted)
        {
            _rigidbody.useGravity = true;
        }
        
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
