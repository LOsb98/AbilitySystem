using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [SerializeField]
    private int health;
    private Rigidbody2D rb;
    public int speed;
    private Animator animator;
    private GameObject player;
    public bool grounded;
    public Transform groundCheckPos;
    public Vector2 groundCheckSize;
    public LayerMask groundLayer;
    public LayerMask playerLayer;
    [SerializeField]
    private float staggerTimer;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (staggerTimer > 0) staggerTimer -= Time.deltaTime;
        animator.SetFloat("StaggerTimer", staggerTimer);
        player = GameObject.Find("Player");
        animator.SetFloat("PlayerDistance", Vector2.Distance(transform.position, player.transform.position));
        Collider2D groundCheck = Physics2D.OverlapBox(groundCheckPos.position, groundCheckSize, 0.0f, groundLayer);
        if (!groundCheck)
        {
            grounded = false;
            return;
        }
        grounded = true;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        staggerTimer = 0.5f;
    }

    public void Knockback(Vector2 knockback, float direction)
    {
        rb.velocity = new Vector2(knockback.x * direction, knockback.y);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(groundCheckPos.position, groundCheckSize);
    }
}
