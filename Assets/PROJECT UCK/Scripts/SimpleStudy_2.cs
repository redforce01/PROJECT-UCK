using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UCK
{
    public class SimpleStudy_2 : MonoBehaviour
    {
        void Start()
        {
            int a = 10;
            if (a > 5)
            {
                Debug.Log("A is greater than 5");
            }
            else
            {
                Debug.Log("A is less than 5");
            }

            // <= 주석 (:코드로 인식이 되지 않는 코멘트/설명)
            SimpleFunction(); // <= 함수 호출
            SumFunction(7, 7); // <= 매개변수를 전달하여 함수를 호출

            // 산술 연산자
            int result1 = 10 + 5; // 덧셈 => 15
            int result2 = 10 - 5; // 뺄셈 => 5
            int result3 = 10 * 5; // 곱셈 => 50
            int result4 = 10 / 5; // 나눗셈 => 2
            int result5 = 10 % 5; // 나머지 => 0

            Debug.Log("Result1 : " + result1);
            Debug.Log("Result2 : " + result2);
            Debug.Log("Result3 : " + result3);
            Debug.Log("Result4 : " + result4);
            Debug.Log("Result5 : " + result5);

            int somethingValue = Random.Range(0, 2); // <= 0 ~ 100 사이의 랜덤한 값을 반환
            Debug.Log("Random Value : " + somethingValue);
            if (somethingValue % 2 == 0 && somethingValue > 0)
            {
                Debug.Log("Random Value : 짝수");
            }
            else if (somethingValue == 0)
            {
                Debug.Log("Random Value : " + somethingValue);
            }
            else
            {
                Debug.Log("Random Value : 홀수");
            }

            // 논리 연산자 => &&(AND), ||(OR), !(NOT)
            int characterHP = 40;
            int characterMP = 30;
            bool characterIsDead = false;
            if (characterHP >= 50 || characterMP > 20 && characterIsDead == false)
            {
                Debug.Log("아직 살만해!");
            }
            else
            {
                Debug.Log("힘들다~!");
            }

            // 할당 연산자 => ( =, +=, -=, *=, /= )
            int someValueA = 10;
            int someValueB = 5;
            int resultSome = someValueA + someValueB; // 15

            someValueA += 5; // someValueA = someValueA + 5;
            someValueB -= 5; // someValueB = someValueB - 5;
            someValueA *= 5; // someValueA = someValueA * 5;
            someValueB /= 5; // someValueB = someValueB / 5;

            // 증감 연산자 => (++, --)
            int randomValue1 = Random.Range(0, 10);
            Debug.Log("My Random Value Before ++ : " + randomValue1);
            randomValue1++; // randomValue1 = randomValue1 + 1;
            Debug.Log("My Random Value After ++ : " + randomValue1);

            int randomValue2 = Random.Range(0, 10);
            Debug.Log("My Random Value Before -- : " + randomValue2);
            randomValue2--; // randomValue2 = randomValue2 - 1;
            Debug.Log("My Random Value After -- : " + randomValue2);
        }

        void SimpleFunction() // <= 함수 or 메소드
        {
            Debug.Log("Called Simple Function!");
            Debug.Log("Called Simple Function!!");
            Debug.Log("Called Simple Function!!!");
        }

        void SumFunction(int a, int b) // <= 함수(매개변수:Parameter)
        {
            int sum = a + b;
            Debug.Log("A + B : " + sum);
        }
    }
}
