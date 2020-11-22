using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    public int damage;
    protected Vector2 aimDirection;
    [SerializeField]
    protected int speed;
    public abstract void InitializeProjectile(Vector2 aim, float spread, int attackLayer);
    //Different projectile types use slightly different layer masks
    public abstract void Deflect(int deflectLayer);
    public float hitboxSize;
    public LayerMask layerToHit;

}
