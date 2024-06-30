using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UCK
{
    public class CharacterController : MonoBehaviour
    {
        public float moveSpeed = 3f;
        public Transform firePoint;
        public BulletResourceData bulletResourceData;


        private int selectedBulletIndex = 0;
        private GameObject currentBulletPrefab;

        private void Update()
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            Vector3 direction = new Vector3(horizontal, 0, vertical);
            transform.Translate(direction * moveSpeed * Time.deltaTime);

            // 숫자 1번 키를 눌렀을 때 순간 한번만 들어옴
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                currentBulletPrefab = bulletResourceData.bulletDatas[0].bulletPrefab;
                selectedBulletIndex = 0;
            }

            // 숫자 2번 키를 눌렀을 때 순간 한번만 들어옴
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                currentBulletPrefab = bulletResourceData.bulletDatas[1].bulletPrefab;
                selectedBulletIndex = 1;
            }

            // 숫자 3번 키를 눌렀을 때 순간 한번만 들어옴
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                currentBulletPrefab = bulletResourceData.bulletDatas[2].bulletPrefab;
                selectedBulletIndex = 2;
            }

            if (Input.GetMouseButton(0))
            {
                // To do : Bullet Fire
                // 1. bulletPrefab을 복제한다
                // 2. 복제한 오브젝트의 위치/회전 값을 firepoint의 위치/회전 값으로 설정한다.
                GameObject newBullet = Instantiate(currentBulletPrefab, firePoint.position, firePoint.rotation);
                Bullet bullet = newBullet.GetComponent<Bullet>();
                bullet.speed = bulletResourceData.bulletDatas[selectedBulletIndex].speed;
                Rigidbody bulletRigidbody = newBullet.GetComponent<Rigidbody>();
                bulletRigidbody.useGravity = bulletResourceData.bulletDatas[selectedBulletIndex].isGravity;
                Destroy(newBullet.gameObject, bulletResourceData.bulletDatas[selectedBulletIndex].lifeTime);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            // Trigger 콜라이더에 처음 닿았을 때, 한 번만 호출
            Debug.Log("Trigger Enter : " + other.name);
        }

        private void OnTriggerStay(Collider other)
        {
            // Trigger 콜라이더에 닿아 있는 동안, 계속 호출
            Debug.Log("Trigger Stay : " + other.name);
        }

        private void OnTriggerExit(Collider other)
        {
            // Trigger 콜라이더에서 떨어질 때, 한 번만 호출
            Debug.Log("Trigger Exit : " + other.name);
        }

        private void OnCollisionEnter(Collision collision)
        {
            // Collision 콜라이더에 처음 닿았을 때, 한 번만 호출
            Debug.Log("Collision Enter : " + collision.gameObject.name);
        }

        private void OnCollisionStay(Collision collision)
        {
            // Collision 콜라이더에 닿아 있는 동안, 계속 호출
            Debug.Log("Collision Stay : " + collision.gameObject.name);
        }

        private void OnCollisionExit(Collision collision)
        {
            // Collision 콜라이더에서 떨어질 때, 한 번만 호출
            Debug.Log("Collision Exit : " + collision.gameObject.name);
        }
    }
}

