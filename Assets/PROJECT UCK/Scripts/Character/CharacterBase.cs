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

        private float verticalVelocity; // 수직 속도
        private bool isGrounded; // 땅에 붙어있는지 여부
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

        private float targetRotation;             // targetRotation의 의미는 캐릭터가 바라보아야 하는 방향을 의미한다.
        private float rotationVelocity;           // rotationVelocity 값은 캐릭터의 회전 속도를 의미한다.
        private float RotationSmoothTime = 0.12f; // RotationSmoothTime 값은 캐릭터의 회전을 부드럽게 처리하기 위한 값이다.

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
            if (!isAttacking) // 내가 공격중이 아니라면? 
            {
                // 캐릭터의 스테미나가 최대치가 아니라면? 
                if (CurrentSP < MaxSP)
                {
                    // 현재 시간이 스테미나 회복 시간을 넘었다면?
                    if (Time.time >= staminaDeltaTime + staminaRecoveryTime)
                    {
                        // StaminaDeltaTime 값을 현재 시간값으로 대입하고, 스테미나를 회복시킨다.
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
                // 죽는것에 대한 처리
                // 예) 죽는 모션을 재생한다.
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

            // input 의 magnitude 값이 0.1보다 작다면 움직이라고 한 Input 값이 없는것으로 판단한다
            // => Move 함수를 빠져나감
            float magnitude = input.magnitude;
            if (magnitude <= 0.1f && isGrounded)
                return;

            // Vector2로 들어오는 input 값을 Vector3 좌표로 변환한다.
            Vector3 inputDirection = new Vector3(input.x, 0, input.y).normalized;

            // 목표하는 회전 방향을 카메라의 방향을 기준으로 계산한다.
            targetRotation = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg + yAxisAngle;

            // 캐릭터의 방향을 목표하는 회전 방향으로 부드럽게 회전시킬 값을 계산한다.
            float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation,
                ref rotationVelocity, RotationSmoothTime);

            if (!isStrafe)
            {
                transform.rotation = Quaternion.Euler(0, rotation, 0);
            }

            // 최종적인 이동 방향을 계산한다.
            Vector3 targetDirection = Quaternion.Euler(0, targetRotation, 0) * Vector3.forward;

            // 최종적으로 이동 명령을 수행
            unityCharacterController.Move(targetDirection.normalized * moveSpeed * Time.deltaTime
                + new Vector3(0f, verticalVelocity, 0f) * Time.deltaTime);

            // input 값을 Animator 에 "Horizontal" 파라미터로 전달
            characterAnimator.SetFloat("Horizontal", false == IsPossibleMovement ? 0 : input.x);

            // input 값을 Animator 에 "Vertical" 파라미터로 전달
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
            // 캐릭터의 발바닥 위치 + Offset 값 만큼 아래쪽 위치를 구해본다.
            Vector3 spherePosition = transform.position + (Vector3.down * groundOffset);

            // 위에서 구한 위치에 Sphere를 하나 생성해보고, Sphere가 groundLayer 와 충돌하는지 검사한다.
            isGrounded = Physics.CheckSphere(spherePosition, groundRadius, groundLayer, QueryTriggerInteraction.Ignore);

            characterAnimator.SetBool("IsGrounded", isGrounded);
        }

        public void FreeFall()
        {
            if (isGrounded == false) // 캐릭터가 땅에 있지 않다면 => 수직 속도를 중력 값을 가중시킨다.
            {
                verticalVelocity += Physics.gravity.y * Time.deltaTime;
            }
            else // 땅에 닿아있으니까 수직 속도를 0으로 만든다.
            {
                // jumpTimeoutDelta 값이 0 보다 크다면 verticalVelocity 를 0으로 만들지 않는다.
                // Jump 후 일정 시간이 지나면 수직 속도를 0으로 만든다.
                // jumpTimeoutDelta 값이 0 보다 크다면 => jumpTimeoutDelta 값을 감소시킨다.
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
