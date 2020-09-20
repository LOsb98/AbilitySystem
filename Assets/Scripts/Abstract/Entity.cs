using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [SerializeField]
    private int health;
    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
    }

    public void Knockback(Vector2 knockback, float direction)
    {
        rb.velocity = new Vector2(knockback.x * direction, knockback.y);
    }
}
