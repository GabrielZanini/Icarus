using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 1f;
    public float speedMultiplier = 1f;
    public float noGravityTime = 0.5f;

    [Header("Impact")]
    public Vector3 targetPoint;
    public int damage = 5;    
    public bool hitted = false;
    
    Rigidbody _rigidbody;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
                
        StartCoroutine(AddPhisics());
    }

    private void FixedUpdate()
    {
        transform.rotation = Quaternion.LookRotation(_rigidbody.velocity);
    }

    private void OnTriggerEnter(Collider other)
    {
        _rigidbody.useGravity = false;
        _rigidbody.velocity = Vector3.zero;

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
        _rigidbody.velocity = transform.forward * speed * speedMultiplier;

        yield return new WaitForSeconds(noGravityTime * speedMultiplier);

        if (!hitted)
        {

        }
        _rigidbody.useGravity = true;
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
