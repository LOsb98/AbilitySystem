using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StraightLineProjectile : Projectile
{
    //Make lifeTimer protected once everything works
    public float lifeTimer;
    public float lifeTime;

    public override void InitializeProjectile(Vector2 aim, float spread, int attackLayer)
    {
        aimDirection = aim;
        aimDirection.x += Random.Range(-spread, spread);
        aimDirection.y += Random.Range(-spread, spread);
        aimDirection.Normalize();
        aimDirection *= speed;
        layerToHit = (1 << attackLayer | 1 << 8);
    }

    public override void Deflect(int deflectLayer)
    {
        aimDirection *= -1;
        layerToHit = (1 << deflectLayer);
        lifeTimer = lifeTime;
    }
}
