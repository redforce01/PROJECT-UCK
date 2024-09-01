using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

namespace UCK
{
    public class TPSCharacter : CharacterBase
    {
        public Projectile projectilePrefab;
        public Transform fireStartPoint;
        public float projectileSpeed = 10f;

        public float fireRate = 0.1f; // 연사 속도 값(초 단위)
        public int maxAmmo = 30; // 최대 탄환 수(: 1탄창의 총알 수)

        protected float aiming = 0f;
        private float aimingBlend = 0f;

        private bool isReloading = false;
        private bool isFiring = false;
        private float lastFiredTime = 0f; // 마지막 탄환 발사 시각 값
        private int currentAmmo = 0; // 현재 탄환 수

        protected override void Start()
        {
            currentAmmo = maxAmmo;
        }

        public override void Fire(bool isFire)
        {
            isFiring = isFire;
        }

        public void Shoot()
        {
            Projectile newBullet = Instantiate(projectilePrefab, fireStartPoint.position, fireStartPoint.rotation);
            newBullet.gameObject.SetActive(true);
            newBullet.SetForce(projectileSpeed);
        }

        public override void SetAiming(float aiming)
        {
            this.aiming = aiming;
            characterAnimator.SetFloat("Aiming", this.aiming);
        }

        protected override void Update()
        {
            GroundCheck();
            FreeFall();

            aimingBlend = Mathf.Lerp(aimingBlend, (IsAiming ? 1f : 0f), Time.deltaTime * 10f);
            SetAiming(aimingBlend);

            characterAnimator.SetFloat("Strafe", isStrafe ? 1f : 0f);

            speed = isWalk ? 1f : 3f;
            targetSpeedBlend = Mathf.Lerp(targetSpeedBlend, targetSpeed, Time.deltaTime * 10f);

            characterAnimator.SetFloat("Speed", targetSpeedBlend);

            if (isFiring)
            {
                if (Time.time > lastFiredTime + fireRate)
                {
                    if (currentAmmo > 0)
                    {
                        lastFiredTime = Time.time;
                        currentAmmo--;
                        Shoot();
                    }
                    else
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
                }
            }
        }

        public void ReloadComplete()
        {
            isReloading = false;
            currentAmmo = maxAmmo;
        }
    }
}
