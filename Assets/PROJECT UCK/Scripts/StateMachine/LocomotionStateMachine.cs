using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UCK
{
    public class LocomotionStateMachine : StateMachineBehaviour
    {
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            UCK.CharacterController uckCharacterController = animator.GetComponent<UCK.CharacterController>();
            uckCharacterController.IsPossibleMovement = true;
            uckCharacterController.IsPossibleAttack = true;
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            UCK.CharacterController uckCharacterController = animator.GetComponent<UCK.CharacterController>();
            uckCharacterController.IsPossibleMovement = false;
            uckCharacterController.IsPossibleAttack = false;
        }
    }
}
