using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : StateMachineBehaviour
{
    private PlayerController playerController;
    private PlayerDash dashController;
    private GameObject player;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = animator.gameObject;
        dashController = player.GetComponent<PlayerDash>();
        playerController = player.GetComponent<PlayerController>();
        Debug.Log("Player Dash");
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        playerController.rb.velocity = new Vector2(player.transform.localScale.x * dashController.dashSpeed, playerController.rb.velocity.y);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
