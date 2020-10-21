using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : StraightLineProjectile
{
    public float lifeTimer;

    void Update()
    {
        lifeTimer -= Time.deltaTime;
        if (lifeTimer <= 0) Destroy(gameObject, 0.0f);

        transform.Translate(aimDirection * Time.deltaTime);
        Collider2D[] collisionbox = Physics2D.OverlapCircleAll(transform.position, hitboxSize, layertoHit);
        foreach (var entity in collisionbox)
        {
            //Check which layer the hit object falls under, need to find different component script for player/enemy
            if (entity.gameObject.layer == LayerMask.NameToLayer("Player")) entity.GetComponent<PlayerController>().TakeDamage(damage);
            else if (entity.gameObject.layer == LayerMask.NameToLayer("Enemy")) entity.GetComponent<Entity>().TakeDamage(damage);
            Destroy(gameObject, 0.0f);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, hitboxSize);
    }
}
