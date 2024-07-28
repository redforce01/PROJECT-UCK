using Cinemachine.Utility;
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
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, checkRadius);

            Gizmos.color = Color.red;
            for (int i = 0; i < overlappedObjects.Count; i++)
            {
                Gizmos.DrawLine(transform.position, overlappedObjects[i].position);
            }
        }

        [Header("Overlapped Objects")]
        public float checkRadius = 5f;
        public float angleThreshhold = 90f;
        public List<Transform> overlappedObjects = new List<Transform>();
        public LayerMask overlapCheckLayers;

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

        private float targetRotation;             // targetRotation의 의미는 캐릭터가 바라보아야 하는 방향을 의미한다.
        private float yaw;                        // yaw 값은 캐릭터의 y축 회전값을 의미한다.
        private float pitch;                      // pitch 값은 캐릭터의 x축 회전값을 의미한다.
        private float rotationVelocity;           // rotationVelocity 값은 캐릭터의 회전 속도를 의미한다.
        private float RotationSmoothTime = 0.12f; // RotationSmoothTime 값은 캐릭터의 회전을 부드럽게 처리하기 위한 값이다.

        private UnityEngine.CharacterController unityCharacterController;
        private CharacterBase characterBase;
        private Animator characterAnimator;

        private float verticalVelocity; // 수직 속도
        private bool isGrounded; // 땅에 붙어있는지 여부
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
            characterBase = GetComponent<CharacterBase>();
        }

        private void Start()
        {
            UCKInputSystem.Instance.onJumpCallback += Jump; // Callback 함수에 Chain(체인)을 건다
                                                            // => Callback 구독한다.

            UCKInputSystem.Instance.onAttack += Attack; // InputSystem의 OnAttack 콜백 함수에다가 Chain을 건다.

            characterBase.OnChangedHP += OnChangedHP;
        }

        private void OnChangedHP(float current, float max)
        {
            GameHUD_UI.Instance.SetHPValue(current / max);
        }

        private void Attack()
        {
            if (IsPossibleAttack == false || isGrounded == false)
                return;

            characterAnimator.SetTrigger("AttackTrigger");

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

            #region 수업 때 배운 OverlapSphere 코드
            //// Update에서 매 Frame마다 overlappedObjects 리스트를 초기화한다.
            //overlappedObjects.Clear();

            //// OverlapSphere 함수를 사용하여 Character의 Transform 위치를 기준으로, CheckRadius 만큼의 반경에 있는 Collider를 검출한다.
            //Collider[] overlapColliders = Physics.OverlapSphere(transform.position, checkRadius, overlapCheckLayers);

            //// 검출된 Collider들을 overlappedObjects 리스트에 추가한다.
            //for (int i = 0; i < overlapColliders.Length; i++)
            //{
            //    // 캐릭터의 앞쪽 방향 벡터
            //    Vector3 forwardA = transform.forward;

            //    // 캐릭터에서 검사하려는 객체로 향하는 방향 벡터
            //    Vector3 directionB = (overlapColliders[i].transform.position - transform.position).normalized;

            //    // 두 벡터간의 내적 계산
            //    float dotProduct = Vector3.Dot(forwardA, directionB);

            //    // 각도 임계값을 코사인 값으로 변환
            //    float cosAngleThreshold = Mathf.Cos(angleThreshhold * Mathf.Deg2Rad);

            //    // 내적과 코사인 임계값을 비교
            //    if (dotProduct >= cosAngleThreshold)
            //    {
            //        // 여기에 들어왔다는 말은 검사한 오브젝트의 위치가 일정 각도내에 있다는 뜻
            //        if (overlapColliders[i].gameObject.CompareTag("Enemy"))
            //        {
            //            overlappedObjects.Add(overlapColliders[i].transform);
            //        }
            //    }
            //}
            #endregion


            GroundCheck();
            FreeFall();

            Vector2 input = UCKInputSystem.Instance.moveInput;         // - Input System에서 input 값을 가져온다.
            Move(input, Camera.main.transform.rotation.eulerAngles.y); // - Move() 함수로 input 값과, 현재 Main Camera의
                                                                       //   y 축 값을 전달했음

            Rotate(CameraSystem.Instance.AimingTargetPoint);

            isStrafe = UCKInputSystem.Instance.isStrafe;
            characterAnimator.SetFloat("Strafe", isStrafe ? 1f : 0f);

            isWalk = UCKInputSystem.Instance.isWalk;
            speed = isWalk ? 1f : 3f;
            targetSpeed = input.magnitude > 0f ? targetSpeed = speed : 0f;

            characterAnimator.SetFloat("Speed", targetSpeed);

            // input 값을 Animator 에 "Horizontal" 파라미터로 전달
            characterAnimator.SetFloat("Horizontal", false == IsPossibleMovement ? 0 : input.x);

            // input 값을 Animator 에 "Vertical" 파라미터로 전달
            characterAnimator.SetFloat("Vertical", false == IsPossibleMovement ? 0 : input.y);
        }

        private void LateUpdate()
        {
            Vector2 look = UCKInputSystem.Instance.look;  // - Input System에서 mouse look 값을 가져온다.
            CameraRotate(look);                           // - CameraRotate() 함수로 mouse look 값을 전달했음
        }

        private void CameraRotate(Vector2 look)
        {
            yaw += look.x * cameraRotationSpeed * Time.deltaTime;      // yaw 값에 look.x * cameraRotationSpeed * Time.deltaTime 값을 더한다.
            pitch += look.y * cameraRotationSpeed * Time.deltaTime;    // pitch 값에 look.y * cameraRotationSpeed * Time.deltaTime 값을 더한다.
            pitch = Mathf.Clamp(pitch, -80, 80); // pitch 값의 범위를 -80 ~ 80 사이로 제한한다.

            cameraPivot.rotation = Quaternion.Euler(pitch, yaw, 0.0f); // CameraPivot 이라는 Transform을 회전시킨다.
        }        

        private void Move(Vector2 input, float yAxisAngle)
        {
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
