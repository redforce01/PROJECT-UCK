using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UCK
{
    public sealed class MySealedClass { }
    public partial class MyPartialClass { }


    public class UCKInputSystem : MonoBehaviour
    {
        public static UCKInputSystem Instance { get; private set; } = null;

        #region Awake/Destroy
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void OnDestroy()
        {
            if (Instance == this)
            {
                Instance = null;
            }
        }
        #endregion

        public Vector2 moveInput;

        public float rotation;

        // Delegate => �Լ��� ����ó�� ����� �� �ְ� ���ִ� ���
        public delegate void OnJumpCallback(); // Delegate ���� => �Լ��� ���¸� ����

        public OnJumpCallback onJumpCallback; // Delegate ���� ����


        private void Update()
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            moveInput = new Vector2(horizontal, vertical);

            rotation = 0f;

            // Input.GetKey        // Key�� ������ �ִ��� Ȯ��
            // Input.GetKeyDown    // Key�� ���ȴ���(1����) Ȯ��
            // Input.GetKeyUp      // Key�� �����ٰ� ��������(1����) Ȯ��

            if (Input.GetKey(KeyCode.Q))
            {
                rotation -= 1;
            }
            if (Input.GetKey(KeyCode.E))
            {
                rotation += 1;
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                // To do : SpaceŰ�� �������� => �̺�Ʈ�� => �ܺο� ���� ���ִ� �ڵ�
                // => Callback(�ݹ�) �̶�� ������ �̶� ������

                onJumpCallback();
            }
        }
    }
}