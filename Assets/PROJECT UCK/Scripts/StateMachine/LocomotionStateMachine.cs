using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UCK
{
    public class LocomotionStateMachine : StateMachineBehaviour
    {
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            UCK.CharacterBase uckCharacterController = animator.GetComponent<UCK.CharacterBase>();
            uckCharacterController.IsPossibleMovement = true;
            uckCharacterController.IsPossibleAttack = true;
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            UCK.CharacterBase uckCharacterController = animator.GetComponent<UCK.CharacterBase>();
            uckCharacterController.IsPossibleMovement = false;
            uckCharacterController.IsPossibleAttack = false;
        }
    }
}
