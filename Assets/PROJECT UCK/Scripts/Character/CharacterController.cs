using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UCK
{
    public class CharacterController : MonoBehaviour
    {
        public float moveSpeed;
        public float rotationSpeed;
        public float jumpForce;

        public float groundRadius = 0.1f;
        public float groundOffset = 0.1f;
        public LayerMask groundLayer;

        private UnityEngine.CharacterController unityCharacterController;

        private float verticalVelocity; // ���� �ӵ�
        private bool isGrounded; // ���� �پ��ִ��� ����
        private float jumpTimeout = 0.1f;
        private float jumpTimeoutDelta = 0f;

        private void Awake()
        {
            unityCharacterController = GetComponent<UnityEngine.CharacterController>();
        }

        private void Start()
        {
            UCKInputSystem.Instance.onJumpCallback += Jump; // Callback �Լ��� Chain(ü��)�� �Ǵ�
                                                            // => Callback �����Ѵ�.
        }

        private void Jump()
        {
            if (isGrounded == false)
                return;

            verticalVelocity = jumpForce;
            jumpTimeoutDelta = jumpTimeout;
        }

        private void Update()
        {
            GroundCheck();
            FreeFall();

            Vector2 input = UCKInputSystem.Instance.moveInput;

            float inputRotation = UCKInputSystem.Instance.rotation;
            float currentRot = transform.rotation.eulerAngles.y;
            currentRot += (inputRotation * Time.deltaTime * rotationSpeed);
            transform.rotation = Quaternion.Euler(0, currentRot, 0);

            // transform.right : X�� ���� ����, input.x : X�� ���� �Է°�,
            // transform.forward : Z�� ���� ����, input.y : Z�� ���� �Է°�
            Vector3 finalDirection = (transform.right * input.x) + (transform.forward * input.y);
            Vector3 movement =
                (finalDirection * Time.deltaTime * moveSpeed) +     // finalDirection �������� X/Z �� �̵��ϴ� ����
                (Vector3.up * verticalVelocity * Time.deltaTime);   // �������� Y �� �������� verticalVelocity �̵��ϴ� ����

            unityCharacterController.Move(movement);
        }

        public void GroundCheck()
        {
            // ĳ������ �߹ٴ� ��ġ + Offset �� ��ŭ �Ʒ��� ��ġ�� ���غ���.
            Vector3 spherePosition = transform.position + (Vector3.down * groundOffset);

            // ������ ���� ��ġ�� Sphere�� �ϳ� �����غ���, Sphere�� groundLayer �� �浹�ϴ��� �˻��Ѵ�.
            isGrounded = Physics.CheckSphere(spherePosition, groundRadius, groundLayer, QueryTriggerInteraction.Ignore);
        }

        public void FreeFall()
        {
            if (isGrounded == false) // ĳ���Ͱ� ���� ���� �ʴٸ� => ���� �ӵ��� �߷� ���� ���߽�Ų��.
            {
                verticalVelocity += Physics.gravity.y * Time.deltaTime;
            }
            else // ���� ��������ϱ� ���� �ӵ��� 0���� �����.
            {
                // jumpTimeoutDelta ���� 0 ���� ũ�ٸ� verticalVelocity �� 0���� ������ �ʴ´�.
                // Jump �� ���� �ð��� ������ ���� �ӵ��� 0���� �����.
                // jumpTimeoutDelta ���� 0 ���� ũ�ٸ� => jumpTimeoutDelta ���� ���ҽ�Ų��.
                if (jumpTimeoutDelta > 0)
                {
                    jumpTimeoutDelta -= Time.deltaTime;
                }
                else
                {
                    verticalVelocity = 0f;
                }
            }
        }
    }
}
