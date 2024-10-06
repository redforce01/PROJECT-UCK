using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UCK
{
    public class CharacterFootIK : MonoBehaviour
    {
        public Animator characterAnimator;
        public Transform leftFoot;  // 왼발의 뼈대 Transform
        public Transform rightFoot; // 오른발의 뼈대 Transform
        public float checkDistance = 1f;      // 레이캐스팅 할 거리
        public LayerMask groundLayer;         // 레이캐스팅 할 Ground Layer 값
        public float skinDistance = 0.25f;    // 레이가 닿은 위치에 발이 그대로 적용되면 스킨이 뚫고 보이기 때문에
                                              // 방지하기 위한 offset 값

        private void Awake()
        {
            characterAnimator = GetComponent<Animator>();
            leftFoot = characterAnimator.GetBoneTransform(HumanBodyBones.LeftFoot);
            rightFoot = characterAnimator.GetBoneTransform(HumanBodyBones.RightFoot);
        }

        private void OnAnimatorIK(int layerIndex)
        {
            if (characterAnimator == null)
                return;

            // 왼쪽 발 IK를 적용할 가중치를 설정
            characterAnimator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, 1f);
            characterAnimator.SetIKRotationWeight(AvatarIKGoal.LeftFoot, 1f);

            // 왼발에서 쏘는 레이
            Ray leftRay = new Ray(
                characterAnimator.GetIKPosition(AvatarIKGoal.LeftFoot) + Vector3.up,
                Vector3.down);

            // 왼발에서 쏜 레이가 닿았는지 체크.
            if (Physics.Raycast(leftRay, out RaycastHit leftHit, checkDistance, groundLayer))
            {
                Vector3 hitPosition = leftHit.point;
                hitPosition.y += skinDistance;

                characterAnimator.SetIKPosition(AvatarIKGoal.LeftFoot, hitPosition);
                characterAnimator.SetIKRotation(
                    AvatarIKGoal.LeftFoot, 
                    Quaternion.LookRotation(transform.forward, leftHit.normal));
            }


            // 오른쪽 발 IK를 적용할 가중치를 설정
            characterAnimator.SetIKPositionWeight(AvatarIKGoal.RightFoot, 1f);
            characterAnimator.SetIKRotationWeight(AvatarIKGoal.RightFoot, 1f);

            // 오른발에서 쏘는 레이
            Ray rightRay = new Ray(
                characterAnimator.GetIKPosition(AvatarIKGoal.RightFoot) + Vector3.up,
                Vector3.down);

            // 오른발에서 쏜 레이가 닿았는지 체크.
            if (Physics.Raycast(rightRay, out RaycastHit rightHit, checkDistance, groundLayer))
            {
                Vector3 hitPosition = rightHit.point;
                hitPosition.y += skinDistance;

                characterAnimator.SetIKPosition(AvatarIKGoal.RightFoot, hitPosition);
                characterAnimator.SetIKRotation(
                    AvatarIKGoal.RightFoot,
                    Quaternion.LookRotation(transform.forward, rightHit.normal));
            }
        }
    }
}
