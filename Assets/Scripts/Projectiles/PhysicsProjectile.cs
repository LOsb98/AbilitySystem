using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PhysicsProjectile : Projectile
{
    public Rigidbody2D rb;

    //InitializeProjectile will be used to aim and fire the projectile
    //Using an abstract method that will apply to all projectiles
    public override void InitializeProjectile(Vector2 aim, float spread, int attackLayer)
    {
        //Getting rb component without using Awake() so it can be used in child classes
        rb = GetComponent<Rigidbody2D>();
        aimDirection = aim;
        aimDirection.x += Random.Range(-spread, spread);
        aimDirection.y += Random.Range(-spread, spread);
        aimDirection.Normalize();
        rb.velocity = aimDirection * speed;
        layerToHit = (1 << attackLayer);
    }

    public override void Deflect(int deflectLayer)
    {
        aimDirection *= -1;
        layerToHit = (1 << deflectLayer);
        rb.velocity = aimDirection * speed;
    }
}
