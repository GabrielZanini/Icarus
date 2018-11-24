using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCharacter : MonoBehaviour
{
    public int maxHeath = 5;
    public int heath = 2;

    public float speed = 2f;
    public float rotationSpeed = 2f;
    
    public virtual void TakeDamage(int damage)
    {
        heath -= damage;
    }
}
