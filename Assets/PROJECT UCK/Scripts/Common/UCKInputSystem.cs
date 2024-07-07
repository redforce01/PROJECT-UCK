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

        // Delegate => 함수를 변수처럼 사용할 수 있게 해주는 기능
        public delegate void OnJumpCallback(); // Delegate 선언 => 함수의 형태를 정의

        public OnJumpCallback onJumpCallback; // Delegate 변수 선언


        private void Update()
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            moveInput = new Vector2(horizontal, vertical);

            rotation = 0f;

            // Input.GetKey        // Key가 눌러져 있는지 확인
            // Input.GetKeyDown    // Key가 눌렸는지(1번만) 확인
            // Input.GetKeyUp      // Key가 눌렀다가 때졌는지(1번만) 확인

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
                // To do : Space키가 눌러졌음 => 이벤트를 => 외부에 전달 해주는 코드
                // => Callback(콜백) 이라는 개념이 이때 등장함

                onJumpCallback();
            }
        }
    }
}