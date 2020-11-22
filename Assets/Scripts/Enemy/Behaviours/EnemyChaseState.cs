using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChaseState : StateMachineBehaviour
{
    private GameObject player;
    private GameObject enemy;
    private int speed;
    private Rigidbody2D rb;
    private MoveController moveController;

    void Awake()
    {
        player = GameObject.Find("Player");
    }

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemy = animator.gameObject;
        moveController = enemy.GetComponent<MoveController>();
        rb = enemy.GetComponent<Rigidbody2D>();
        speed = enemy.GetComponent<Entity>().speed;
        Debug.Log("Chasing");
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (player.transform.position.x < enemy.transform.position.x) enemy.transform.localScale = new Vector3(-1, 1, 1); 
        else enemy.transform.localScale = new Vector3(1, 1, 1);

        moveController.Move(rb, enemy.transform.localScale.x, speed);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
