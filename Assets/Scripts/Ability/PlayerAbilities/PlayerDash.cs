using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDash : PlayerAbility
{
    public float dashTime;
    public float dashSpeed;
    [SerializeField]
    private float dashTimer;

    public override void TriggerAbility()
    {
        print("Start dash ability");
        dashTimer = dashTime;
        controller.canMove = false;
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
        dashTimer = 0;
        cooldownTimer = cooldownTime;
        controller.canMove = true;
        isActive = false;
    }
}
