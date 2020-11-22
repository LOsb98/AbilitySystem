using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : PhysicsProjectile
{
    public float explosionSize;
    public float explosionTime;

    void Awake()
    {
        Invoke("Explode", explosionTime);
    }

    void Update()
    {
        if (explosionTime > 0) explosionTime -= Time.deltaTime;

        Collider2D collisionbox = Physics2D.OverlapCircle(transform.position, hitboxSize, layerToHit);
        if (collisionbox)
        {
            Explode();
        }
    }

    private void Explode()
    {
        print("Exploded");
        Collider2D[] explosion = Physics2D.OverlapCircleAll(transform.position, explosionSize, layerToHit);
        foreach (var entity in explosion)
        {
            //Check which layer the hit object falls under, need to find different component script for player/enemy
            if (entity.gameObject.layer == LayerMask.NameToLayer("Player")) entity.GetComponent<PlayerController>().TakeDamage(damage);
            else if (entity.gameObject.layer == LayerMask.NameToLayer("Enemy")) entity.GetComponent<Entity>().TakeDamage(damage);
        }
        Destroy(gameObject, 0.0f);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, hitboxSize);
        Gizmos.DrawWireSphere(transform.position, explosionSize);
    }
}
