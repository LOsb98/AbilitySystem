using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StraightLineProjectile : Projectile
{
    public override void InitializeProjectile(Vector2 aim, float spread)
    {
        aimDirection = aim;
        aimDirection.x += Random.Range(-spread, spread);
        aimDirection.y += Random.Range(-spread, spread);
        aimDirection.Normalize();
        aimDirection *= speed;
        print("Initialized: " + aimDirection);
    }
}
