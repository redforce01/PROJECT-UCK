using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UCK
{
    public class PlayerCharacter : CharacterBase
    {
        public static PlayerCharacter Instance { get; private set; }


        private InteractionSensor interactionSensor;

        private List<IInteractable> currentInteractionItems = new List<IInteractable>();

        protected override void Awake()
        {
            base.Awake();

            Instance = this;

            interactionSensor = GetComponentInChildren<InteractionSensor>();
            interactionSensor.OnDetected += OnDetectedInteraction;
            interactionSensor.OnLostSignal += OnLostInteraction;
        }

        public void Interact()
        {
            if (currentInteractionItems.Count <= 0)
            {
                return;
            }

            Interaction_UI.Instance.ExecuteInteractionData();
        }

        private void OnDetectedInteraction(IInteractable interactable)
        {
            if (interactable.IsAutoInteract == true)
            {
                interactable.Interact(this);
            }
            else
            {
                Interaction_UI.Instance.AddInteractionData(interactable);
                currentInteractionItems.Add(interactable);
            }
        }

        private void OnLostInteraction(IInteractable interactable)
        {
            Interaction_UI.Instance.RemoveInteractionData(interactable);

            currentInteractionItems.Remove(interactable);
        }

        public override void Attack()
        {
            if (IsPossibleAttack == false || IsGrounded == false)
                return;

            if (CurrentSP < 10f)
                return;

            if (isAttacking)
                return;

            isAttacking = true;
            characterAnimator.SetTrigger("AttackTrigger");

            DecreaseStamina(10f);
            staminaDeltaTime = Time.time;

            Collider[] overlapped = Physics.OverlapSphere(transform.position, 1f, LayerMask.GetMask("Character"));
            for (int i = 0; i < overlapped.Length; i++)
            {
                Vector3 forward = transform.forward;
                Vector3 direction = (overlapped[i].transform.position - transform.position).normalized;
                float dotProduct = Vector3.Dot(forward, direction);
                float cosAngleThreshold = Mathf.Cos(30f * Mathf.Deg2Rad);
                if (dotProduct >= cosAngleThreshold)
                {
                    CharacterBase character = overlapped[i].GetComponent<CharacterBase>();
                    character.TakeDamage(10f);
                }
            }
        }
    }
}
