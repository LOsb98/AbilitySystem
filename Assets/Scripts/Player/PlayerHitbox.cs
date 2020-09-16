using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Hitbox/Player Hitbox")]
public class PlayerHitbox : ScriptableObject
{
    public int damage;
    public float size;
    public Vector2 position;
    public float endTime;
}
