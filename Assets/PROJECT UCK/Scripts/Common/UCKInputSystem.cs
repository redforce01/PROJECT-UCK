using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UCK
{
    public sealed class MySealedClass { }
    public partial class MyPartialClass { }


    public class UCKInputSystem : SingletonBase<UCKInputSystem>
    {
        public Vector2 moveInput;
        public Vector2 look;
        public bool isStrafe;
        public bool isWalk;
        public bool isAim;
        public bool isFire;

        // Delegate => �Լ��� ����ó�� ����� �� �ְ� ���ִ� ���
        public delegate void OnJumpCallback(); // Delegate ���� => �Լ��� ���¸� ����
        public OnJumpCallback onJumpCallback; // Delegate ���� ����

        public System.Action<bool> onChangeFired;
        public System.Action onAttack;
        public System.Action onInteract;
        public System.Action<float> onMouseWheel;
        public System.Action onTab;

        private Vector2 lastMousePosition;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                onTab?.Invoke();
            }

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
            isAim = Input.GetMouseButton(1); // ���콺 �� Ŭ���� ������ �ִٸ� isAim�� true �ƴϸ� false

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
                onChangeFired?.Invoke(true);
            }

            if (Input.GetMouseButtonUp(0))
            {
                onChangeFired?.Invoke(false);
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