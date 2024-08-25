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

        protected float aiming = 0f;
        private float aimingBlend = 0f;

        public override void Attack()
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
        }
    }
}
