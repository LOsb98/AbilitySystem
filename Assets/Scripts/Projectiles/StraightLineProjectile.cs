using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightLineProjectile : Projectile
{
    public override void InitializeProjectile(Vector2 aim, float spread)
    {
        aimDirection = aim;
    }

    void Update()
    {
        
    }
}
