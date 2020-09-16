using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMelee : MonoBehaviour
{
    public PlayerHitbox[] hitboxArray; //Array of attack hitbox data
    [SerializeField]
    private int currentAttack = 0;

    private List<GameObject> enemiesHit; //List of enemies hit by a hitbox, may not be needed if hitboxes only linger for a frame

    public float delayTimerStart; //Time the player has to continue the autocombo
    [SerializeField]
    private float delayTimer;
    [SerializeField]
    private float endTimer; //Delay between attacks in the autocombo

    void Update()
    {
        if (delayTimer > 0)
        {
            delayTimer -= Time.deltaTime;
        }

        if (endTimer > 0)
        {
            endTimer -= Time.deltaTime;
        }
    }

    public void Attack()
    {
        if (endTimer > 0)
        {
            print("Cannot attack yet");
            return;
        }

        if (delayTimer <= 0)
        {
            print("Delayed too long, autocombo reset");
            currentAttack = 0;
        }

        //Create hitbox with hitboxArray[currentAttack]
        print("Created hitbox: " + (currentAttack + 1));

        endTimer = hitboxArray[currentAttack].endTime;
        currentAttack++;
        if (currentAttack > (hitboxArray.Length - 1)) currentAttack = 0;
        delayTimer = delayTimerStart;
    }
}