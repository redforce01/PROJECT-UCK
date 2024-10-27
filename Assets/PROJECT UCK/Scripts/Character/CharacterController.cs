using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UCK
{
    public class CharacterController : MonoBehaviour
    {
        public static CharacterController Instance;

        public Transform cameraPivot;
        public float cameraRotationSpeed = 30f;

        private float yaw;                        // yaw ���� ĳ������ y�� ȸ������ �ǹ��Ѵ�.
        private float pitch;                      // pitch ���� ĳ������ x�� ȸ������ �ǹ��Ѵ�.

        private CharacterBase characterBase;

        private void Awake()
        {
            Instance = this;
            characterBase = GetComponent<CharacterBase>();
        }

        private void Start()
        {
            UIManager.Show<GameHUD_UI>(UIList.GameHUD_UI);
            UIManager.Show<Indicator_UI>(UIList.Indicator_UI);
            UIManager.Show<Interaction_UI>(UIList.Interaction_UI);


            UCKInputSystem.Singleton.onJumpCallback += Jump; // Callback �Լ��� Chain(ü��)�� �Ǵ�
                                                            // => Callback �����Ѵ�.
            UCKInputSystem.Singleton.onAttack += Attack; // InputSystem�� OnAttack �ݹ� �Լ����ٰ� Chain�� �Ǵ�.
            UCKInputSystem.Singleton.onInteract += Interact;
            UCKInputSystem.Singleton.onMouseWheel += OnMouseWheel;
            UCKInputSystem.Singleton.onTab += OnCameraSideChange;

            UCKInputSystem.Singleton.onChangeFired += Fire;


            characterBase.OnChangedHP += OnChangedHP;
            characterBase.OnChangedSP += OnChangedSP;

            OnChangedHP(characterBase.CurrentHP, characterBase.MaxHP);
            OnChangedSP(characterBase.CurrentSP, characterBase.MaxSP);            
        }

        private void OnCameraSideChange()
        {
            if (characterBase.IsAlive)
            {
                CameraSystem.Instance.SetChangeCameraSide();
            }
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
            Vector2 input = UCKInputSystem.Singleton.moveInput;         // - Input System���� input ���� �����´�.
            if (characterBase.IsAlive)
            {
                characterBase.Move(input, Camera.main.transform.rotation.eulerAngles.y); // - Move() �Լ��� input ����, ���� Main Camera��
                                                                                         //   y �� ���� ��������
                characterBase.Rotate(CameraSystem.Instance.AimingTargetPoint);

                characterBase.IsStrafe = UCKInputSystem.Singleton.isStrafe;
                characterBase.IsWalk = UCKInputSystem.Singleton.isWalk;

                characterBase.IsAiming = UCKInputSystem.Singleton.isAim;
                CameraSystem.Instance.SetActiveAimingCamera(UCKInputSystem.Singleton.isAim);
            }
            else
            {
                characterBase.Move(Vector2.zero, Camera.main.transform.rotation.eulerAngles.y);
            }

        }

        private void LateUpdate()
        {
            Vector2 look = UCKInputSystem.Singleton.look;  // - Input System���� mouse look ���� �����´�.
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
