using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {


    public int heath = 2;
    public float speed = 2f;
    public float rotationSpeed = 2f;

    public virtual void TakeDamage(int damage)
    {
        heath -= damage;
    }

    public virtual void Respawn(Vector3 respawnPoint)
    {
        heath = 2;

        Vector3 newPosition;
        newPosition = transform.position + new Vector3(Random.Range(-20, 20), Random.Range(-1, 10), Random.Range(-20, 20));
        newPosition += (newPosition - respawnPoint).normalized * 10;

        transform.position = newPosition;
    }

}
