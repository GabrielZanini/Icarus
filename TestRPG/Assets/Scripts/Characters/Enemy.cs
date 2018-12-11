using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : BaseCharacter
{
    public Transform target;
    public int scorePoints = 10;
    public GameObject deathEffect;

    public virtual void Respawn(Vector3 respawnPoint)
    {
        heath = maxHeath;

        Vector3 newPosition;
        newPosition = transform.position + new Vector3(Random.Range(-20, 20), Random.Range(-1, 10), Random.Range(-20, 20));
        newPosition += (newPosition - respawnPoint).normalized * 10;

        transform.position = newPosition;
    }

    public virtual void TakeDamage(int damage, PlayerCharacter hitter)
    {
        heath -= damage;

        hitter.AddScore(scorePoints);
    }
}
