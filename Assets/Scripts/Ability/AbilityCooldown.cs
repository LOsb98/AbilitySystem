using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityCooldown : MonoBehaviour
{
    public PlayerAbility ability1;
    public PlayerAbility ability2;

    public void StartAbility1()
    {
        if (ability1.isActive == false)
        {
            if (ability1.cooldownTimer <= 0)
            {
                ability1.TriggerAbility();
            }
        }
    }

    public void StartAbility2()
    {
        if (ability2.isActive == false)
        {
            if (ability2.cooldownTimer <= 0)
            {
                ability2.TriggerAbility();
            }
        }
    }

    void Update()
    {
        if (ability1.cooldownTimer > 0 && ability1.isActive == false)
        {
            ability1.cooldownTimer -= Time.deltaTime;
        }

        //if (ability2.cooldownTimer > 0)
        //{
        //    ability2.cooldownTimer -= Time.deltaTime;
        //}
    }
}
