using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerAbility : MonoBehaviour
{
    protected PlayerController controller;
    public float cooldownTime;
    public float cooldownTimer;
    public bool isActive;
    //Abstract methods for triggering ability
    public abstract void TriggerAbility();
    public abstract void EndAbility();
    //public Sprite icon

    void Awake()
    {
        controller = GetComponent<PlayerController>();
    }
}
