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

        private float yaw;                        // yaw ���� ĳ������ y�� ȸ������ �ǹ��Ѵ�.
        private float pitch;                      // pitch ���� ĳ������ x�� ȸ������ �ǹ��Ѵ�.

        private CharacterBase characterBase;

        private void Awake()
        {
            characterBase = GetComponent<CharacterBase>();
        }

        private void Start()
        {
            UCKInputSystem.Instance.onJumpCallback += Jump; // Callback �Լ��� Chain(ü��)�� �Ǵ�
                                                            // => Callback �����Ѵ�.
            UCKInputSystem.Instance.onAttack += Attack; // InputSystem�� OnAttack �ݹ� �Լ����ٰ� Chain�� �Ǵ�.
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
            Vector2 input = UCKInputSystem.Instance.moveInput;         // - Input System���� input ���� �����´�.
            if (characterBase.IsAlive)
            {
                characterBase.Move(input, Camera.main.transform.rotation.eulerAngles.y); // - Move() �Լ��� input ����, ���� Main Camera��
                                                                                         //   y �� ���� ��������
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
            Vector2 look = UCKInputSystem.Instance.look;  // - Input System���� mouse look ���� �����´�.
            CameraRotate(look);                           // - CameraRotate() �Լ��� mouse look ���� ��������
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
            yaw += look.x * cameraRotationSpeed * Time.deltaTime;      // yaw ���� look.x * cameraRotationSpeed * Time.deltaTime ���� ���Ѵ�.
            pitch += look.y * cameraRotationSpeed * Time.deltaTime;    // pitch ���� look.y * cameraRotationSpeed * Time.deltaTime ���� ���Ѵ�.
            pitch = Mathf.Clamp(pitch, -80, 80); // pitch ���� ������ -80 ~ 80 ���̷� �����Ѵ�.

            cameraPivot.rotation = Quaternion.Euler(pitch, yaw, 0.0f); // CameraPivot �̶�� Transform�� ȸ����Ų��.
        }
    }
}
