using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalkState : StateMachineBehaviour
{
    private MoveController moveController;
    private PlayerController playerController;
    private GameObject player;

    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = animator.gameObject;
        moveController = player.GetComponent<MoveController>();
        playerController = player.GetComponent <PlayerController>();
        Debug.Log("Player Walking");
    }

    //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!playerController.canMove) return;

        if (playerController.moveDirection.x < 0)
        {
            player.transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (playerController.moveDirection.x > 0)
        {
            player.transform.localScale = new Vector3(1, 1, 1);
        }
        moveController.Move(playerController.rb, playerController.moveDirection.x, playerController.moveSpeed);
    }

    //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }
}
