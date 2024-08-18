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
        public Vector2 look;
        public bool isStrafe;
        public bool isWalk;

        // Delegate => �Լ��� ����ó�� ����� �� �ְ� ���ִ� ���
        public delegate void OnJumpCallback(); // Delegate ���� => �Լ��� ���¸� ����
        public OnJumpCallback onJumpCallback; // Delegate ���� ����

        public System.Action onAttack;
        public System.Action onInteract;
        public System.Action<float> onMouseWheel;

        private Vector2 lastMousePosition;

        private void Update()
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            moveInput = new Vector2(horizontal, vertical);

            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");
            look = new Vector2(mouseX, mouseY);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                // To do : SpaceŰ�� �������� => �̺�Ʈ�� => �ܺο� ���� ���ִ� �ڵ�
                // => Callback(�ݹ�) �̶�� ������ �̶� ������

                onJumpCallback();
            }

            isStrafe = Input.GetMouseButton(1); // ���콺 ������ ��ư�� ������ �ִٸ� true, �ƴϸ� false

            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                isWalk = true;
            }

            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                isWalk = false;
            }

            if (Input.GetMouseButtonDown(0)) // Mouse ���� ��ư�� �������ٸ�
            {
                onAttack?.Invoke();
            }

            if (Input.GetKeyDown(KeyCode.F))
            {
                onInteract?.Invoke();
            }

            float mouseWheel = Input.GetAxis("Mouse ScrollWheel");
            if (mouseWheel > 0f)
            {
                onMouseWheel?.Invoke(mouseWheel);
            }
            else if (mouseWheel < 0f)
            {
                onMouseWheel?.Invoke(mouseWheel);
            }
        }
    }
}