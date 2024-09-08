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
