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

        public float fireRate = 0.1f; // ���� �ӵ� ��(�� ����)
        public int maxAmmo = 30; // �ִ� źȯ ��(: 1źâ�� �Ѿ� ��)

        protected float aiming = 0f;
        private float aimingBlend = 0f;

        private bool isReloading = false;
        private bool isFiring = false;
        private float lastFiredTime = 0f; // ������ źȯ �߻� �ð� ��
        private int currentAmmo = 0; // ���� źȯ ��

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
                        // To do : ������
                        // ������ ���� �����ϱ�
                        // ������ �ִϸ��̼� ȣ���ϱ�

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
