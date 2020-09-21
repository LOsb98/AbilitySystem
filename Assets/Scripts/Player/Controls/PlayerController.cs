﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerController : MonoBehaviour
{
    public int health;
    public bool canMove;
    public bool canAttack;
    public bool grounded;
    public float moveSpeed;
    public int jumpForce;
    public Transform groundCheckPos;
    public Vector2 groundCheckSize;
    public LayerMask groundLayer;

    private AbilityCooldown abilityManager;
    private PlayerInput controls;
    private PlayerMelee meleeController;
    private MoveController movementController;
    private Vector2 moveDirection;
    public Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        movementController = GetComponent<MoveController>();
        abilityManager = GetComponent<AbilityCooldown>();
        meleeController = GetComponent<PlayerMelee>();

        #region controls
        controls = new PlayerInput();

        controls.Gameplay.Move.performed += ctx =>
        {
            moveDirection = ctx.ReadValue<Vector2>();
            if (!canMove) return;

            if (moveDirection.x < 0)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else if (moveDirection.x > 0)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }

        };

        controls.Gameplay.Move.canceled += ctx =>
        {
            moveDirection = Vector2.zero;
        };

        controls.Gameplay.Jump.performed += ctx =>
        {
            //Jumping can be done in controls since it's a one-off impulse force
            //If adding double jump, jump can be moved to its own method
            if (grounded)
            {
                movementController.Jump(rb, jumpForce);
            }
        };

        controls.Gameplay.Attack.performed += ctx =>
        {
            if (canAttack)
            {
                //Basic melee input will use a method in the melee controller to check whether an attack can be performed
                meleeController.Attack();
            }
        };

        controls.Gameplay.Ability1.performed += ctx =>
        {
            abilityManager.StartAbility1();
        };

        controls.Gameplay.Ability2.performed += ctx =>
        {
            abilityManager.StartAbility2();
        };

        controls.Gameplay.Parry.performed += ctx =>
        {
            print("Parry");
        };
        #endregion
    }

    void Update()
    {
        //Ground check
        Collider2D groundCheck = Physics2D.OverlapBox(groundCheckPos.position, groundCheckSize, 0.0f, groundLayer);
        if (!groundCheck)
        {
            grounded = false;
            return;
        }
        grounded = true;
    }

    void FixedUpdate()
    {
        //Player movement check
        if (!canMove) return;

        //Grounded movement
        if (grounded == true)
        {
            movementController.Move(rb, moveDirection.x, moveSpeed);
            return;
        }

        //Air movement
        movementController.AirMove(rb, moveDirection.x, moveSpeed);

    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(groundCheckPos.position, groundCheckSize);
    }

    void OnEnable()
    {
        controls.Gameplay.Enable();
    }

    void OnDisable()
    {
        controls.Gameplay.Disable();
    }
}
