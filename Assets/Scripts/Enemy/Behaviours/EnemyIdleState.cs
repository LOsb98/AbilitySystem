using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : StateMachineBehaviour
{
    private GameObject player;
    private GameObject enemy;
    private Entity entityData;
    private Rigidbody2D rb;
    private MoveController movementController;

    private float moveDirection;
    private float moveTime;

    void Awake()
    {
        player = GameObject.Find("Player");
    }

    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemy = animator.gameObject;
        rb = enemy.GetComponent<Rigidbody2D>();
        movementController = enemy.GetComponent<MoveController>();
        entityData = enemy.GetComponent<Entity>();
        Debug.Log("Idle state");
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (moveTime <= 0)
        {
            moveTime = Random.Range(0.5f, 1f);
            moveDirection = Random.Range(-0.4f, 0.4f);
        }
        else
        {
            moveTime -= Time.deltaTime;
            if (entityData.grounded == true)
            {
                movementController.Move(rb, moveDirection, entityData.speed);
            }
            else
            {
                movementController.AirMove(rb, moveDirection, entityData.speed);
            }
        }

        if (moveDirection > 0)
        {
            enemy.transform.localScale = new Vector3(1, 1, 1);
        }
        else if (moveDirection < 0)
        {
            enemy.transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("Leaving idle state");
    }
}
