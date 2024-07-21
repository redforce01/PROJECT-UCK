using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D;
using UnityEngine;
using UnityEngine.UIElements;

namespace UCK
{
    public class CharacterController : MonoBehaviour
    {
        public bool IsPossibleMovement { get; set; } = true;
        public bool IsPossibleAttack { get; set; } = true;


        public float moveSpeed;
        public float rotationSpeed;
        public float jumpForce;

        public float groundRadius = 0.1f;
        public float groundOffset = 0.1f;
        public LayerMask groundLayer;

        public Transform cameraPivot;
        public float cameraRotationSpeed = 30f;

        private float targetRotation;             // targetRotation�� �ǹ̴� ĳ���Ͱ� �ٶ󺸾ƾ� �ϴ� ������ �ǹ��Ѵ�.
        private float yaw;                        // yaw ���� ĳ������ y�� ȸ������ �ǹ��Ѵ�.
        private float pitch;                      // pitch ���� ĳ������ x�� ȸ������ �ǹ��Ѵ�.
        private float rotationVelocity;           // rotationVelocity ���� ĳ������ ȸ�� �ӵ��� �ǹ��Ѵ�.
        private float RotationSmoothTime = 0.12f; // RotationSmoothTime ���� ĳ������ ȸ���� �ε巴�� ó���ϱ� ���� ���̴�.

        private UnityEngine.CharacterController unityCharacterController;
        private Animator characterAnimator;

        private float verticalVelocity; // ���� �ӵ�
        private bool isGrounded; // ���� �پ��ִ��� ����
        private float jumpTimeout = 0.1f;
        private float jumpTimeoutDelta = 0f;

        private bool isWalk = false;
        private bool isStrafe = false;
        private float speed = 0f;
        private float targetSpeed = 0f;


        private void Awake()
        {
            unityCharacterController = GetComponent<UnityEngine.CharacterController>();
            characterAnimator = GetComponent<Animator>();
        }

        private void Start()
        {
            UCKInputSystem.Instance.onJumpCallback += Jump; // Callback �Լ��� Chain(ü��)�� �Ǵ�
                                                            // => Callback �����Ѵ�.

            UCKInputSystem.Instance.onAttack += Attack; // InputSystem�� OnAttack �ݹ� �Լ����ٰ� Chain�� �Ǵ�.
        }

        private void Attack()
        {
            if (IsPossibleAttack == false || isGrounded == false)
                return;

            characterAnimator.SetTrigger("AttackTrigger");
        }


        private void Jump()
        {
            if (isGrounded == false)
                return;

            verticalVelocity = jumpForce;
            jumpTimeoutDelta = jumpTimeout;

            characterAnimator.SetTrigger("JumpTrigger");
        }

        private void Update()
        {
            GroundCheck();
            FreeFall();

            Vector2 input = UCKInputSystem.Instance.moveInput;         // - Input System���� input ���� �����´�.
            Move(input, Camera.main.transform.rotation.eulerAngles.y); // - Move() �Լ��� input ����, ���� Main Camera��
                                                                       //   y �� ���� ��������

            Rotate(CameraSystem.Instance.AimingTargetPoint);

            isStrafe = UCKInputSystem.Instance.isStrafe;
            characterAnimator.SetFloat("Strafe", isStrafe ? 1f : 0f);

            isWalk = UCKInputSystem.Instance.isWalk;
            speed = isWalk ? 1f : 3f;
            targetSpeed = input.magnitude > 0f ? targetSpeed = speed : 0f;

            characterAnimator.SetFloat("Speed", targetSpeed);

            // input ���� Animator �� "Horizontal" �Ķ���ͷ� ����
            characterAnimator.SetFloat("Horizontal", false == IsPossibleMovement ? 0 : input.x);

            // input ���� Animator �� "Vertical" �Ķ���ͷ� ����
            characterAnimator.SetFloat("Vertical", false == IsPossibleMovement ? 0 : input.y);
        }

        private void LateUpdate()
        {
            Vector2 look = UCKInputSystem.Instance.look;  // - Input System���� mouse look ���� �����´�.
            CameraRotate(look);                           // - CameraRotate() �Լ��� mouse look ���� ��������
        }

        private void CameraRotate(Vector2 look)
        {
            yaw += look.x * cameraRotationSpeed * Time.deltaTime;      // yaw ���� look.x * cameraRotationSpeed * Time.deltaTime ���� ���Ѵ�.
            pitch += look.y * cameraRotationSpeed * Time.deltaTime;    // pitch ���� look.y * cameraRotationSpeed * Time.deltaTime ���� ���Ѵ�.
            pitch = Mathf.Clamp(pitch, -80, 80); // pitch ���� ������ -80 ~ 80 ���̷� �����Ѵ�.

            cameraPivot.rotation = Quaternion.Euler(pitch, yaw, 0.0f); // CameraPivot �̶�� Transform�� ȸ����Ų��.
        }        

        private void Move(Vector2 input, float yAxisAngle)
        {
            if (IsPossibleMovement == false)
            {
                input = Vector2.zero;
            }

            // input �� magnitude ���� 0.1���� �۴ٸ� �����̶�� �� Input ���� ���°����� �Ǵ��Ѵ�
            // => Move �Լ��� ��������
            float magnitude = input.magnitude;
            if (magnitude <= 0.1f && isGrounded)
                return;

            // Vector2�� ������ input ���� Vector3 ��ǥ�� ��ȯ�Ѵ�.
            Vector3 inputDirection = new Vector3(input.x, 0, input.y).normalized;

            // ��ǥ�ϴ� ȸ�� ������ ī�޶��� ������ �������� ����Ѵ�.
            targetRotation = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg + yAxisAngle;

            // ĳ������ ������ ��ǥ�ϴ� ȸ�� �������� �ε巴�� ȸ����ų ���� ����Ѵ�.
            float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, 
                ref rotationVelocity, RotationSmoothTime);

            if (!isStrafe)
            {
                transform.rotation = Quaternion.Euler(0, rotation, 0);
            }

            // �������� �̵� ������ ����Ѵ�.
            Vector3 targetDirection = Quaternion.Euler(0, targetRotation, 0) * Vector3.forward;

            // ���������� �̵� ����� ����
            unityCharacterController.Move(targetDirection.normalized * moveSpeed * Time.deltaTime 
                + new Vector3(0f, verticalVelocity, 0f) * Time.deltaTime);
        }

        private void Rotate(Vector3 targetPoint)
        {
            if (!isStrafe)
                return;

            Vector3 position = transform.position;
            Vector3 direction = (targetPoint - position).normalized;
            direction.y = 0f;
            transform.forward = Vector3.Lerp(transform.forward, direction, rotationSpeed * Time.deltaTime);
        }

        public void GroundCheck()
        {
            // ĳ������ �߹ٴ� ��ġ + Offset �� ��ŭ �Ʒ��� ��ġ�� ���غ���.
            Vector3 spherePosition = transform.position + (Vector3.down * groundOffset);

            // ������ ���� ��ġ�� Sphere�� �ϳ� �����غ���, Sphere�� groundLayer �� �浹�ϴ��� �˻��Ѵ�.
            isGrounded = Physics.CheckSphere(spherePosition, groundRadius, groundLayer, QueryTriggerInteraction.Ignore);

            characterAnimator.SetBool("IsGrounded", isGrounded);
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
