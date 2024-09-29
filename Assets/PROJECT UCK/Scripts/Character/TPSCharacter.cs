using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UCK
{
    public class TPSCharacter : CharacterBase
    {
        public void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(primaryWeapon.fireStartPoint.position, aimingTargetPoint.position);

            Gizmos.color = Color.blue;
            Gizmos.DrawLine(primaryWeapon.fireStartPoint.position, primaryWeapon.fireStartPoint.forward.normalized * 10f);
        }

        public WeaponBase primaryWeapon;
        public WeaponBase secondaryWeapon;

        protected float aiming = 0f;
        private float aimingBlend = 0f;
        private bool isReloading = false;
        private WeaponBase currentWeapon = null;


        public Transform aimingTargetPoint;

        protected override void Start()
        {
            currentWeapon = primaryWeapon;
            currentWeapon.gameObject.SetActive(true);

            GameHUD_UI.Instance.SetWeaponInfo(
                currentWeapon.gameObject.name,
                currentWeapon.CurrentAmmo,
                currentWeapon.MaxAmmo);
        }

        public override void Fire(bool isFire)
        {
            currentWeapon.SetFireState(isFire);
        }

        public override void SetAiming(float aiming)
        {
            this.aiming = aiming;
            characterAnimator.SetFloat("Aiming", this.aiming);
        }

        protected override void Update()
        {
            aimingTargetPoint.position = CameraSystem.Instance.AimingTargetPoint;
            currentWeapon.fireStartPoint.forward = aimingTargetPoint.position - currentWeapon.fireStartPoint.position;

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                currentWeapon.gameObject.SetActive(false);
                currentWeapon = primaryWeapon;
                currentWeapon.gameObject.SetActive(true);

                GameHUD_UI.Instance.SetWeaponInfo(
                    currentWeapon.gameObject.name,
                    currentWeapon.CurrentAmmo,
                    currentWeapon.MaxAmmo);
            }

            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                currentWeapon.gameObject.SetActive(false);
                currentWeapon = secondaryWeapon;
                currentWeapon.gameObject.SetActive(true);

                GameHUD_UI.Instance.SetWeaponInfo(
                    currentWeapon.gameObject.name,
                    currentWeapon.CurrentAmmo,
                    currentWeapon.MaxAmmo);
            }

            GroundCheck();
            FreeFall();

            aimingBlend = Mathf.Lerp(aimingBlend, (IsAiming ? 1f : 0f), Time.deltaTime * 10f);
            SetAiming(aimingBlend);

            characterAnimator.SetFloat("Strafe", isStrafe ? 1f : 0f);

            speed = isWalk ? 1f : 3f;
            targetSpeedBlend = Mathf.Lerp(targetSpeedBlend, targetSpeed, Time.deltaTime * 10f);

            characterAnimator.SetFloat("Speed", targetSpeedBlend);

            if (currentWeapon.CurrentAmmo <= 0)
            {
                // To do : 재장전
                // 재장전 로직 수행하기
                // 재장전 애니메이션 호출하기

                if (!isReloading)
                {
                    isReloading = true;
                    characterAnimator.SetTrigger("ReloadTrigger");
                }
            }

            GameHUD_UI.Instance.SetWeaponAmmo(currentWeapon.CurrentAmmo, currentWeapon.maxAmmo);
        }

        public void ReloadComplete()
        {
            isReloading = false;
            currentWeapon.Reload();

            GameHUD_UI.Instance.SetWeaponAmmo(currentWeapon.CurrentAmmo, currentWeapon.maxAmmo);
        }
    }
}
