using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UCK
{
    public class PlayerCharacter : CharacterBase
    {
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
