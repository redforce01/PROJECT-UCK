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

        // Delegate => 함수를 변수처럼 사용할 수 있게 해주는 기능
        public delegate void OnJumpCallback(); // Delegate 선언 => 함수의 형태를 정의
        public OnJumpCallback onJumpCallback; // Delegate 변수 선언

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
                // To do : Space키가 눌러졌음 => 이벤트를 => 외부에 전달 해주는 코드
                // => Callback(콜백) 이라는 개념이 이때 등장함

                onJumpCallback();
            }

            isStrafe = Input.GetMouseButton(1); // 마우스 오른쪽 버튼이 눌러져 있다면 true, 아니면 false

            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                isWalk = true;
            }

            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                isWalk = false;
            }

            if (Input.GetMouseButtonDown(0)) // Mouse 왼쪽 버튼이 눌러졌다면
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