using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDash : PlayerAbility
{
    public float dashTime;
    public float dashSpeed;
    [SerializeField]
    private float dashTimer;
    private RigidbodyConstraints2D originalConstraints;

    public override void TriggerAbility()
    {
        print("Start dash ability");
        originalConstraints = controller.rb.constraints;
        controller.rb.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
        dashTimer = dashTime;
        controller.canMove = false;
        controller.canAttack = false;
        isActive = true;
    }

    void Update()
    {
        if (dashTimer > 0 && isActive == true)
        {
            controller.rb.velocity = new Vector2(transform.localScale.x * dashSpeed, controller.rb.velocity.y);
            dashTimer -= Time.deltaTime;
        }
        else if (dashTimer <= 0 && isActive == true)
        {
            EndAbility();
        }
    }

    public override void EndAbility()
    {
        controller.rb.constraints = originalConstraints;
        dashTimer = 0;
        cooldownTimer = cooldownTime;
        controller.canMove = true;
        controller.canAttack = true;
        isActive = false;
    }
}
