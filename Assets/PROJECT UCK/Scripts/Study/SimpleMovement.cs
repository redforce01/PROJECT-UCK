using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UCK
{
    public class SimpleMovement : MonoBehaviour
    {
        public float moveSpeed = 5.0f;

        private void Update()
        {
            // ���α׷��ֿ��� ���� : +
            // ���α׷��ֿ��� ���� : -
            // ���α׷��ֿ��� ���� : *
            // ���α׷��ֿ��� ������ : /

            // 1. ���࿡ W Ű�� ���������̸���
            // 2. ������ �̵�
            // Input.GetKey(KeyCode.W) => W Ű�� ������ �ִ��� Ȯ�� => true or false
            if (Input.GetKey(KeyCode.W))
            {
                // transform ���� position�� ����
                // transform.forward => �ش� Transform�� Forward(�� ����)�� ��Ÿ��
                transform.Translate(transform.forward * Time.deltaTime * moveSpeed);
            }

            // 1. ���࿡ S Ű�� ���������̸���
            // 2. �ڷ� �̵�
            if (Input.GetKey(KeyCode.S))
            {
                transform.Translate(-transform.forward * Time.deltaTime * moveSpeed);
            }

            // 1. ���࿡ A Ű�� ���������̸���
            // 2. �������� �̵�
            if (Input.GetKey(KeyCode.A))
            {
                transform.Translate(-transform.right * Time.deltaTime * moveSpeed);
            }

            // 1. ���࿡ D Ű�� ���������̸���
            // 2. ���������� �̵�
            if (Input.GetKey(KeyCode.D))
            {
                transform.Translate(transform.right * Time.deltaTime * moveSpeed);
            }
        }
    }
}