using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UCK
{
    public class CharacterFootIK : MonoBehaviour
    {
        public Animator characterAnimator;
        public Transform leftFoot;  // �޹��� ���� Transform
        public Transform rightFoot; // �������� ���� Transform
        public float checkDistance = 1f;      // ����ĳ���� �� �Ÿ�
        public LayerMask groundLayer;         // ����ĳ���� �� Ground Layer ��
        public float skinDistance = 0.25f;    // ���̰� ���� ��ġ�� ���� �״�� ����Ǹ� ��Ų�� �հ� ���̱� ������
                                              // �����ϱ� ���� offset ��

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

            // ���� �� IK�� ������ ����ġ�� ����
            characterAnimator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, 1f);
            characterAnimator.SetIKRotationWeight(AvatarIKGoal.LeftFoot, 1f);

            // �޹߿��� ��� ����
            Ray leftRay = new Ray(
                characterAnimator.GetIKPosition(AvatarIKGoal.LeftFoot) + Vector3.up,
                Vector3.down);

            // �޹߿��� �� ���̰� ��Ҵ��� üũ.
            if (Physics.Raycast(leftRay, out RaycastHit leftHit, checkDistance, groundLayer))
            {
                Vector3 hitPosition = leftHit.point;
                hitPosition.y += skinDistance;

                characterAnimator.SetIKPosition(AvatarIKGoal.LeftFoot, hitPosition);
                characterAnimator.SetIKRotation(
                    AvatarIKGoal.LeftFoot, 
                    Quaternion.LookRotation(transform.forward, leftHit.normal));
            }


            // ������ �� IK�� ������ ����ġ�� ����
            characterAnimator.SetIKPositionWeight(AvatarIKGoal.RightFoot, 1f);
            characterAnimator.SetIKRotationWeight(AvatarIKGoal.RightFoot, 1f);

            // �����߿��� ��� ����
            Ray rightRay = new Ray(
                characterAnimator.GetIKPosition(AvatarIKGoal.RightFoot) + Vector3.up,
                Vector3.down);

            // �����߿��� �� ���̰� ��Ҵ��� üũ.
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
