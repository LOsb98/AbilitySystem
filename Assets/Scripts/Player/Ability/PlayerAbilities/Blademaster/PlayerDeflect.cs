using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeflect : PlayerAbility
{
    public bool debugView;

    public float deflectTime;
    [SerializeField]
    private float deflectTimer;
    public float deflectSize;

    public List<GameObject> projDeflected;
    public LayerMask projectileLayer;

    public override void TriggerAbility()
    {
        print("Start deflect");
        deflectTimer = deflectTime;
        //controller.canMove = false;
        controller.canAttack = false;
        isActive = true;
    }

    void Update()
    {
        if (deflectTimer > 0)
        {
            Collider2D[] deflectArea = Physics2D.OverlapCircleAll(transform.position, deflectSize, projectileLayer);
            for (int i = 0; i < deflectArea.Length; i++)
            {
                //Check if a projectile has been deflected already, otherwise it gets stuck inside the deflect box
                if (!projDeflected.Contains(deflectArea[i].gameObject))
                {
                    deflectArea[i].GetComponent<Projectile>().Deflect(10);
                    projDeflected.Add(deflectArea[i].gameObject);
                }
            }

            deflectTimer -= Time.deltaTime;
        }

        else if (deflectTimer <= 0 && isActive == true)
        {
            EndAbility();
        }
    }

    public override void EndAbility()
    {
        projDeflected.Clear();
        print("End deflect");
        deflectTimer = 0;
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
            Gizmos.DrawWireSphere(transform.position, deflectSize);
        }
    }
}
