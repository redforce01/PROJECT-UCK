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

            // ���� 1�� Ű�� ������ �� ���� �ѹ��� ����
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                currentBulletPrefab = bulletResourceData.bulletDatas[0].bulletPrefab;
                selectedBulletIndex = 0;
            }

            // ���� 2�� Ű�� ������ �� ���� �ѹ��� ����
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                currentBulletPrefab = bulletResourceData.bulletDatas[1].bulletPrefab;
                selectedBulletIndex = 1;
            }

            // ���� 3�� Ű�� ������ �� ���� �ѹ��� ����
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                currentBulletPrefab = bulletResourceData.bulletDatas[2].bulletPrefab;
                selectedBulletIndex = 2;
            }

            if (Input.GetMouseButton(0))
            {
                // To do : Bullet Fire
                // 1. bulletPrefab�� �����Ѵ�
                // 2. ������ ������Ʈ�� ��ġ/ȸ�� ���� firepoint�� ��ġ/ȸ�� ������ �����Ѵ�.
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
            // Trigger �ݶ��̴��� ó�� ����� ��, �� ���� ȣ��
            Debug.Log("Trigger Enter : " + other.name);
        }

        private void OnTriggerStay(Collider other)
        {
            // Trigger �ݶ��̴��� ��� �ִ� ����, ��� ȣ��
            Debug.Log("Trigger Stay : " + other.name);
        }

        private void OnTriggerExit(Collider other)
        {
            // Trigger �ݶ��̴����� ������ ��, �� ���� ȣ��
            Debug.Log("Trigger Exit : " + other.name);
        }

        private void OnCollisionEnter(Collision collision)
        {
            // Collision �ݶ��̴��� ó�� ����� ��, �� ���� ȣ��
            Debug.Log("Collision Enter : " + collision.gameObject.name);
        }

        private void OnCollisionStay(Collision collision)
        {
            // Collision �ݶ��̴��� ��� �ִ� ����, ��� ȣ��
            Debug.Log("Collision Stay : " + collision.gameObject.name);
        }

        private void OnCollisionExit(Collision collision)
        {
            // Collision �ݶ��̴����� ������ ��, �� ���� ȣ��
            Debug.Log("Collision Exit : " + collision.gameObject.name);
        }
    }
}

