using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UCK
{
    public class WeaponBase : MonoBehaviour
    {
        public int CurrentAmmo => curAmmo;
        public int MaxAmmo => maxAmmo;


        public Projectile projectilePrefab;
        public Transform fireStartPoint;

        public int maxAmmo;
        public int curAmmo;

        public float reloadTime;
        public float fireRate;
        public float damage;
        public float bulletSpeed = 10f;

        private bool isFiring = false;
        private float lastFiredTime = 0f;

        protected virtual void Update()
        {
            if (isFiring)
            {
                if (Time.time > lastFiredTime + fireRate)
                {
                    if (curAmmo > 0)
                    {
                        lastFiredTime = Time.time;
                        curAmmo--;
                        Shoot();
                    }
                }
            }
        }

        public void Shoot()
        {
            Projectile newBullet = Instantiate(projectilePrefab, fireStartPoint.position, fireStartPoint.rotation);
            newBullet.gameObject.SetActive(true);
            newBullet.SetForce(bulletSpeed);

            int randomSound = UnityEngine.Random.Range(0, 2);
            SoundType fireSoundType = randomSound == 0 ? SoundType.Fire_01 : SoundType.Fire_02;
            SoundSystem.Singleton.PlaySFX(fireSoundType, fireStartPoint.position);
        }

        public void SetFireState(bool isFire)
        {
            isFiring = isFire;
        }

        public void Reload()
        {
            curAmmo = maxAmmo;
        }
    }
}
