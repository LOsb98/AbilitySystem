using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "Weapons/Ranged Weapon")]
public class RangedWeapon : ScriptableObject
{
    public GameObject projectile;
    public int clipSize;
    public float fireRate;
    public float reloadTime;
    public float spread;
    public int projectilesPerShot;
}
