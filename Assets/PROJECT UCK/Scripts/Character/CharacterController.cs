using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UCK
{
    public class CharacterController : MonoBehaviour
    {
        public Transform cameraPivot;
        public float cameraRotationSpeed = 30f;

        private float yaw;                        // yaw 값은 캐릭터의 y축 회전값을 의미한다.
        private float pitch;                      // pitch 값은 캐릭터의 x축 회전값을 의미한다.

        private CharacterBase characterBase;

        private void Awake()
        {
            characterBase = GetComponent<CharacterBase>();
        }

        private void Start()
        {
            UCKInputSystem.Instance.onJumpCallback += Jump; // Callback 함수에 Chain(체인)을 건다
                                                            // => Callback 구독한다.
            UCKInputSystem.Instance.onAttack += Attack; // InputSystem의 OnAttack 콜백 함수에다가 Chain을 건다.
            UCKInputSystem.Instance.onInteract += Interact;
            UCKInputSystem.Instance.onMouseWheel += OnMouseWheel;

            UCKInputSystem.Instance.onChangeFired += Fire;


            characterBase.OnChangedHP += OnChangedHP;
            characterBase.OnChangedSP += OnChangedSP;

            OnChangedHP(characterBase.CurrentHP, characterBase.MaxHP);
            OnChangedSP(characterBase.CurrentSP, characterBase.MaxSP);
        }

        private void OnMouseWheel(float mouseWheel)
        {
            Interaction_UI.Instance.ChangeSelection(mouseWheel);
        }

        private void Interact()
        {
            var playerCharacter = characterBase as PlayerCharacter;
            if (playerCharacter == null)
                return;

            playerCharacter.Interact();
        }

        private void Update()
        {
            Vector2 input = UCKInputSystem.Instance.moveInput;         // - Input System에서 input 값을 가져온다.
            if (characterBase.IsAlive)
            {
                characterBase.Move(input, Camera.main.transform.rotation.eulerAngles.y); // - Move() 함수로 input 값과, 현재 Main Camera의
                                                                                         //   y 축 값을 전달했음
                characterBase.Rotate(CameraSystem.Instance.AimingTargetPoint);

                characterBase.IsStrafe = UCKInputSystem.Instance.isStrafe;
                characterBase.IsWalk = UCKInputSystem.Instance.isWalk;

                characterBase.IsAiming = UCKInputSystem.Instance.isAim;
            }
            else
            {
                characterBase.Move(Vector2.zero, Camera.main.transform.rotation.eulerAngles.y);
            }

        }

        private void LateUpdate()
        {
            Vector2 look = UCKInputSystem.Instance.look;  // - Input System에서 mouse look 값을 가져온다.
            CameraRotate(look);                           // - CameraRotate() 함수로 mouse look 값을 전달했음
        }


        private void Attack()
        {
            characterBase.Attack();
        }

        private void Fire(bool isFire)
        {
            characterBase.Fire(isFire);
        }

        private void Jump()
        {
            characterBase.Jump();
        }

        private void OnChangedHP(float current, float max)
        {
            GameHUD_UI.Instance.SetHPValue(current, max);
        }

        private void OnChangedSP(float current, float max)
        {
            GameHUD_UI.Instance.SetSPValue(current, max);
        }

        private void CameraRotate(Vector2 look)
        {
            yaw += look.x * cameraRotationSpeed * Time.deltaTime;      // yaw 값에 look.x * cameraRotationSpeed * Time.deltaTime 값을 더한다.
            pitch += look.y * cameraRotationSpeed * Time.deltaTime;    // pitch 값에 look.y * cameraRotationSpeed * Time.deltaTime 값을 더한다.
            pitch = Mathf.Clamp(pitch, -80, 80); // pitch 값의 범위를 -80 ~ 80 사이로 제한한다.

            cameraPivot.rotation = Quaternion.Euler(pitch, yaw, 0.0f); // CameraPivot 이라는 Transform을 회전시킨다.
        }
    }
}
