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
            // 프로그래밍에서 덧셈 : +
            // 프로그래밍에서 뺄셈 : -
            // 프로그래밍에서 곱셈 : *
            // 프로그래밍에서 나눗셈 : /

            // 1. 만약에 W 키를 누른상태이면은
            // 2. 앞으로 이동
            // Input.GetKey(KeyCode.W) => W 키를 누르고 있는지 확인 => true or false
            if (Input.GetKey(KeyCode.W))
            {
                // transform 에서 position을 변경
                // transform.forward => 해당 Transform의 Forward(앞 방향)을 나타냄
                transform.Translate(transform.forward * Time.deltaTime * moveSpeed);
            }

            // 1. 만약에 S 키를 누른상태이면은
            // 2. 뒤로 이동
            if (Input.GetKey(KeyCode.S))
            {
                transform.Translate(-transform.forward * Time.deltaTime * moveSpeed);
            }

            // 1. 만약에 A 키를 누른상태이면은
            // 2. 왼쪽으로 이동
            if (Input.GetKey(KeyCode.A))
            {
                transform.Translate(-transform.right * Time.deltaTime * moveSpeed);
            }

            // 1. 만약에 D 키를 누른상태이면은
            // 2. 오른쪽으로 이동
            if (Input.GetKey(KeyCode.D))
            {
                transform.Translate(transform.right * Time.deltaTime * moveSpeed);
            }
        }
    }
}