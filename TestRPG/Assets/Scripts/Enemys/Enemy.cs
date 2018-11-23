using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    public int maxHeath = 5;
    public int heath = 2;

    public float speed = 2f;
    public float rotationSpeed = 2f;



    public virtual void TakeDamage(int damage)
    {
        heath -= damage;
    }

    public virtual void Respawn(Vector3 respawnPoint)
    {
        heath = maxHeath;

        Vector3 newPosition;
        newPosition = transform.position + new Vector3(Random.Range(-20, 20), Random.Range(-1, 10), Random.Range(-20, 20));
        newPosition += (newPosition - respawnPoint).normalized * 10;

        transform.position = newPosition;
    }

}
