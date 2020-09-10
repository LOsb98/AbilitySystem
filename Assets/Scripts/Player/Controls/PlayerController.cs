using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerController : MonoBehaviour
{
    public AbilityCooldown abilityManager;
    private PlayerInput controls;
    private Vector2 moveDirection;
    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        #region controls
        controls = new PlayerInput();

        controls.Gameplay.Move.performed += ctx =>
        {
            moveDirection = ctx.ReadValue<Vector2>();
            rb.velocity = (moveDirection * 5);
            print("Moving");
        };

        controls.Gameplay.Move.canceled += ctx =>
        {
            moveDirection = Vector2.zero;
        };

        controls.Gameplay.Jump.performed += ctx =>
        {
            //Jump code
            print("Jump");
        };

        controls.Gameplay.Attack.performed += ctx =>
        {
            //Attack code
            print("Attack");
        };

        controls.Gameplay.Ability1.performed += ctx =>
        {
            print("Ability1");
        };

        controls.Gameplay.Ability2.performed += ctx =>
        {
            print("Ability2");
        };

        controls.Gameplay.Parry.performed += ctx =>
        {
            print("Parry");
        };
        #endregion
    }

    void OnEnable()
    {
        controls.Gameplay.Enable();
    }

    void OnDisable()
    {
        controls.Gameplay.Disable();
    }

    void Update()
    {

    }
}
