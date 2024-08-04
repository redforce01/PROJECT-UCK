using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UCK
{
    public class CharacterBase : MonoBehaviour
    {
        public float CurrentSP => currentSP;
        public float CurrentHP => currentHP;
        public float MaxSP => maxSP;
        public float MaxHP => maxHP;
        public bool IsGrounded => isGrounded;
        public bool IsAlive => currentHP > 0f;

        public bool IsPossibleMovement
        {
            get => isPossibleMovement;
            set => isPossibleMovement = value;
        }

        public bool IsPossibleAttack
        {
            get => isPossibleAttack;
            set
            {
                isPossibleAttack = value;
                if (false == isPossibleAttack)
                {
                    isAttacking = false;
                }
            }
        }

        public bool IsStrafe
        {
            get => isStrafe;
            set => isStrafe = value;
        }

        public bool IsWalk
        {
            get => isWalk;
            set => isWalk = value;
        }

        [SerializeField] private float currentHP;
        [SerializeField] private float maxHP;
        [SerializeField] private float currentSP;
        [SerializeField] private float maxSP;

        public System.Action<float, float> OnChangedHP;
        public System.Action<float, float> OnChangedSP;


        public float moveSpeed;
        public float rotationSpeed;
        public float jumpForce;

        public float staminaRecoveryAmount = 5f;
        public float staminaRecoveryTime = 3f;
        public float staminaDeltaTime;

        public float groundRadius = 0.1f;
        public float groundOffset = 0.1f;
        public LayerMask groundLayer;

        private float verticalVelocity; // ���� �ӵ�
        private bool isGrounded; // ���� �پ��ִ��� ����
        private float jumpTimeout = 0.1f;
        private float jumpTimeoutDelta = 0f;

        private bool isWalk = false;
        private bool isStrafe = false;
        private float speed = 0f;
        private float targetSpeed = 0f;
        private float targetSpeedBlend = 0f;

        private bool isPossibleMovement = false;
        private bool isPossibleAttack = false;
        protected bool isAttacking = false;

        private float targetRotation;             // targetRotation�� �ǹ̴� ĳ���Ͱ� �ٶ󺸾ƾ� �ϴ� ������ �ǹ��Ѵ�.
        private float rotationVelocity;           // rotationVelocity ���� ĳ������ ȸ�� �ӵ��� �ǹ��Ѵ�.
        private float RotationSmoothTime = 0.12f; // RotationSmoothTime ���� ĳ������ ȸ���� �ε巴�� ó���ϱ� ���� ���̴�.

        private UnityEngine.CharacterController unityCharacterController;
        protected Animator characterAnimator;

        private void Awake()
        {
            currentHP = maxHP;
            currentSP = maxSP;

            characterAnimator = GetComponent<Animator>();
            unityCharacterController = GetComponent<UnityEngine.CharacterController>();
        }

        private void Update()
        {
            if (!isAttacking) // ���� �������� �ƴ϶��? 
            {
                // ĳ������ ���׹̳��� �ִ�ġ�� �ƴ϶��? 
                if (CurrentSP < MaxSP)
                {
                    // ���� �ð��� ���׹̳� ȸ�� �ð��� �Ѿ��ٸ�?
                    if (Time.time >= staminaDeltaTime + staminaRecoveryTime)
                    {
                        // StaminaDeltaTime ���� ���� �ð������� �����ϰ�, ���׹̳��� ȸ����Ų��.
                        staminaDeltaTime = Time.time;
                        IncreaseStamina(staminaRecoveryAmount);
                    }
                }
            }

            GroundCheck();
            FreeFall();

            characterAnimator.SetFloat("Strafe", isStrafe ? 1f : 0f);

            speed = isWalk ? 1f : 3f;            
            targetSpeedBlend = Mathf.Lerp(targetSpeedBlend, targetSpeed, Time.deltaTime * 10f);

            characterAnimator.SetFloat("Speed", targetSpeedBlend);
        }

        public virtual void Attack()
        {
           
        }

        public void Jump()
        {
            if (isGrounded == false || jumpTimeoutDelta > 0f)
                return;

            verticalVelocity = jumpForce;
            jumpTimeoutDelta = jumpTimeout;

            characterAnimator.SetTrigger("JumpTrigger");
        }

        public void IncreaseStamina(float value)
        {
            currentSP += value;
            currentSP = Mathf.Clamp(currentSP, 0, maxSP);

            OnChangedSP?.Invoke(currentSP, maxSP);
        }

        public void DecreaseStamina(float value)
        {
            currentSP -= value;
            currentSP = Mathf.Clamp(currentSP, 0, maxSP);

            OnChangedSP?.Invoke(currentSP, maxSP);
        }

        public void TakeDamage(float damage)
        {
            currentHP -= damage;

            OnChangedHP?.Invoke(currentHP, maxHP);

            if (currentHP <= 0)
            {
                // �״°Ϳ� ���� ó��
                // ��) �״� ����� ����Ѵ�.
                characterAnimator.SetTrigger("DeadTrigger");
                characterAnimator.SetBool("IsDead", true);
            }
        }


        public void Move(Vector2 input, float yAxisAngle)
        {
            targetSpeed = input.magnitude > 0f ? targetSpeed = speed : 0f;

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

            // input ���� Animator �� "Horizontal" �Ķ���ͷ� ����
            characterAnimator.SetFloat("Horizontal", false == IsPossibleMovement ? 0 : input.x);

            // input ���� Animator �� "Vertical" �Ķ���ͷ� ����
            characterAnimator.SetFloat("Vertical", false == IsPossibleMovement ? 0 : input.y);
        }

        public void Rotate(Vector3 targetPoint)
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
