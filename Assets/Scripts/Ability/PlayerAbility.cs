using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerAbility : MonoBehaviour
{
    public PlayerController controller;
    //Abstract methods for triggering ability
    //Child classes will use these for their own unique behaviours
    public abstract void TriggerAbility();
    public abstract void EndAbility();

    public float cooldownTime;
    //public Sprite icon

    void Awake()
    {
        controller = GetComponent<PlayerController>();
    }
}
