using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UCK
{
    public class SimpleStudy : MonoBehaviour
    {
        private void Start()
        {
            // 변수 이름 :
            // 1. 문자로 시작
            // 2. 특수문자 사용 안됨
            // 3. 공백 사용 안됨
            // 4. 예약어 사용 안됨
            // 5. 대소문자 구분
            // 6. 변수 이름은 의미를 부여하여 작성
            // 7. 변수 이름은 명사로 작성

            int value_int = 12; // int : 정수형 변수
            float value_float = 10.81f; // float : 실수형 변수
            string value_string = "ABCDE"; // string : 문자(열) 변수
            char value_char = 'Y'; // char : 문자 변수
            bool value_bool = true; // bool : 논리형 변수 (true or false)

            Debug.Log("Value : " + value_int);
            Debug.Log("Value : " + value_float);
            Debug.Log("Value : " + value_string);
            Debug.Log("Value : " + value_char);
            Debug.Log("Value : " + value_bool);


            int characterHP = 25;
            // 조건문 : if, else if, else
            if (characterHP > 50)
            {
                Debug.Log("HP is over Half");
            }
            else if (characterHP > 20)
            {
                Debug.Log("HP is low");
            }
            else
            {
                Debug.Log("HP is Under Half");
            }

            // 반복문 : for
            // for (초기화; 반복 조건; 증감)
            for (int i = 0; i < 10; i++)
            {
                Debug.Log("Index : " + i);
            }

            // 구구단 출력
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    Debug.Log(i + "x" + j + " : " + i * j);
                }
            }
        }
    }
}

