using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Hitbox/Player Hitbox")]
public class PlayerHitbox : ScriptableObject
{
    public int damage;
    public Vector2 size;
    public Vector2 position;
    public Vector2 knockback;
    public float endTime;
}
