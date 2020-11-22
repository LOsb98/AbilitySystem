using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public bool debugView;

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
    public float staggerTime;
    private float staggerTimer;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        player = GameObject.Find("Player");
        if (staggerTimer > 0)
        {
            staggerTimer -= Time.deltaTime;
            animator.SetFloat("StaggerTimer", staggerTimer);
        }
        animator.SetFloat("PlayerDistance", Vector2.Distance(transform.position, player.transform.position));
        GroundCheck();
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        print("Enemy took " + damage + " damage");
        if (health <= 0) Destroy(gameObject, 0f);
        staggerTimer = staggerTime;
        animator.SetFloat("StaggerTimer", staggerTime);
    }

    public void Knockback(float knockbackX, float knockbackY)
    {
        rb.velocity = new Vector2(knockbackX, knockbackY);
    }

    private void GroundCheck()
    {
        Collider2D groundCheck = Physics2D.OverlapBox(groundCheckPos.position, groundCheckSize, 0.0f, groundLayer);
        if (!groundCheck)
        {
            grounded = false;
            return;
        }
        grounded = true;
    }

    void OnDrawGizmosSelected()
    {
        if (debugView)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(groundCheckPos.position, groundCheckSize);
        }
    }
}
