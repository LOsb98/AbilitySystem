using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class EnemyShootState : StateMachineBehaviour
{
    private GameObject player;
    private GameObject enemy;
    private RangedWeapon weapon;

    public int clip;
    public float fireRateTimer;
    public float reloadTimer;

    void Awake()
    {
        player = GameObject.Find("Player");
    }
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemy = animator.gameObject;
        weapon = enemy.GetComponent<WeaponManager>().weapon;
        clip = weapon.clipSize;
        fireRateTimer = weapon.fireRate;
        Debug.Log("Shooting state");
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (player.transform.position.x < enemy.transform.position.x) enemy.transform.localScale = new Vector3(-1, 1, 1);
        else enemy.transform.localScale = new Vector3(1, 1, 1);

        if (reloadTimer > 0) reloadTimer -= Time.deltaTime;
        if (fireRateTimer > 0) fireRateTimer -= Time.deltaTime;
        if (reloadTimer <= 0 && fireRateTimer <= 0 && clip > 0)
        {
            for (int i = 0; i < weapon.projectilesPerShot; i++)
            {
                GameObject newProjectile = Instantiate(weapon.projectile, enemy.transform.position, enemy.transform.rotation);
                Vector2 aimDirection = player.transform.position - enemy.transform.position;
                newProjectile.GetComponent<Projectile>().InitializeProjectile(aimDirection, weapon.spread, 9);
            }
            clip--;
            if (clip > 0)
            {
                fireRateTimer = weapon.fireRate;
            }
            else
            {
                fireRateTimer = weapon.fireRate;
                reloadTimer = weapon.reloadTime;
                clip = weapon.clipSize;
            }
        }

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        fireRateTimer = weapon.fireRate;
    }
}
