using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDash : PlayerAbility
{
    public bool debugView;

    public float dashTime;
    public float dashSpeed;
    private float dashTimer;
    private RigidbodyConstraints2D originalConstraints;
    private Vector2 dashDirection;
    private bool groundedStart;

    public List<GameObject> enemiesHit;
    public LayerMask enemyLayer;
    public int damage;
    public Vector2 knockbackAngle;
    public float hitboxSize;

    public override void TriggerAbility()
    {
        //Only activate dash if player is holding a direction
        if (controller.moveDirection == Vector2.zero) return;

        if (controller.grounded == true) groundedStart = true;
        else groundedStart = false;
        print("Start dash ability");

        //Constraints used for old horizontal-only dash
        //originalConstraints = controller.rb.constraints;
        //controller.rb.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;

        dashTimer = dashTime;
        dashDirection = controller.moveDirection.normalized * dashSpeed;
        controller.canMove = false;
        controller.canAttack = false;
        isActive = true;
    }

    void Update()
    {
        //If the player starts a dash on the ground it will continue along the ground/into the air
        //If they start a dash from the air into the ground it will cancel the dash on landing
        if (controller.grounded == true && isActive == true && groundedStart == false)
        {
            EndAbility();
            return;
        }

        if (dashTimer > 0 && isActive == true)
        {
            Collider2D[] attackArea = Physics2D.OverlapCircleAll(transform.position, hitboxSize, enemyLayer);
            for (int i = 0; i < attackArea.Length; i++)
            {
                //Checking whether an enemy has already been hit by the attack to prevent unwanted rehits
                if (!enemiesHit.Contains(attackArea[i].gameObject))
                {
                    attackArea[i].GetComponent<Entity>().TakeDamage(damage);
                    enemiesHit.Add(attackArea[i].gameObject);
                }
                attackArea[i].GetComponent<Entity>().Knockback(knockbackAngle.x * transform.localScale.x, knockbackAngle.y);
            }

            controller.rb.velocity = dashDirection;
            dashTimer -= Time.deltaTime;
        }
        else if (dashTimer <= 0 && isActive == true)
        {
            EndAbility();
        }
    }

    public override void EndAbility()
    {
        enemiesHit.Clear();
        print("Cancel/finished dash ability");
        //controller.rb.constraints = originalConstraints;
        dashTimer = 0;
        cooldownTimer = cooldownTime;
        controller.canMove = true;
        controller.canAttack = true;
        isActive = false;
    }

    void OnDrawGizmos()
    {
        if (debugView)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, hitboxSize);
        }
    }
}
