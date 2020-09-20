using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMelee : MonoBehaviour
{
    public PlayerHitbox[] hitboxArray; //Array of attack hitbox data
    [SerializeField]
    private int currentAttack = 0;

    public float delayTimerStart; //Time the player has to continue the autocombo
    [SerializeField]
    private float delayTimer;
    [SerializeField]
    private float endTimer; //Delay between attacks in the autocombo

    private Vector2 attackPos;
    public LayerMask enemyLayer;

    private Vector2 debugPos;
    private float debugSize;

    void Update()
    {
        if (delayTimer > 0) delayTimer -= Time.deltaTime;

        if (endTimer > 0) endTimer -= Time.deltaTime;

        //Create the attack position in Update() so transform.localScale can be used for hitbox
        attackPos = new Vector2(hitboxArray[currentAttack].position.x * transform.localScale.x, hitboxArray[currentAttack].position.y);
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

        Collider2D[] attackArea = Physics2D.OverlapCircleAll(AsVector2(transform.position) + attackPos, hitboxArray[currentAttack].size, enemyLayer);
        for (int i = 0; i < attackArea.Length; i++)
        {
            print("Hit enemy");
        }
        Debug();

        endTimer = hitboxArray[currentAttack].endTime;
        currentAttack++;
        if (currentAttack > (hitboxArray.Length - 1)) currentAttack = 0;
        delayTimer = delayTimerStart;
    }

    private static Vector2 AsVector2(Vector3 v3)
    {
        //Used for turning transform.position into a usable Vector2, as Vector2 and Vector3 cannot be added together
        return new Vector2(v3.x, v3.y);
    }

    private void Debug()
    {
        print("Created hitbox: " + (currentAttack + 1));
        print(hitboxArray[currentAttack].position);
        print(hitboxArray[currentAttack].size);
        //Hitbox data is taken before incrementing currentAttack so gizmo shows the last hitbox created, instead of the next one to be created
        debugPos = AsVector2(transform.position) + attackPos;
        debugSize = hitboxArray[currentAttack].size;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(debugPos, debugSize);
    }
}