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

            // <= �ּ� (:�ڵ�� �ν��� ���� �ʴ� �ڸ�Ʈ/����)
            SimpleFunction(); // <= �Լ� ȣ��
            SumFunction(7, 7); // <= �Ű������� �����Ͽ� �Լ��� ȣ��

            // ��� ������
            int result1 = 10 + 5; // ���� => 15
            int result2 = 10 - 5; // ���� => 5
            int result3 = 10 * 5; // ���� => 50
            int result4 = 10 / 5; // ������ => 2
            int result5 = 10 % 5; // ������ => 0

            Debug.Log("Result1 : " + result1);
            Debug.Log("Result2 : " + result2);
            Debug.Log("Result3 : " + result3);
            Debug.Log("Result4 : " + result4);
            Debug.Log("Result5 : " + result5);

            int somethingValue = Random.Range(0, 2); // <= 0 ~ 100 ������ ������ ���� ��ȯ
            Debug.Log("Random Value : " + somethingValue);
            if (somethingValue % 2 == 0 && somethingValue > 0)
            {
                Debug.Log("Random Value : ¦��");
            }
            else if (somethingValue == 0)
            {
                Debug.Log("Random Value : " + somethingValue);
            }
            else
            {
                Debug.Log("Random Value : Ȧ��");
            }

            // �� ������ => &&(AND), ||(OR), !(NOT)
            int characterHP = 40;
            int characterMP = 30;
            bool characterIsDead = false;
            if (characterHP >= 50 || characterMP > 20 && characterIsDead == false)
            {
                Debug.Log("���� �츸��!");
            }
            else
            {
                Debug.Log("�����~!");
            }

            // �Ҵ� ������ => ( =, +=, -=, *=, /= )
            int someValueA = 10;
            int someValueB = 5;
            int resultSome = someValueA + someValueB; // 15

            someValueA += 5; // someValueA = someValueA + 5;
            someValueB -= 5; // someValueB = someValueB - 5;
            someValueA *= 5; // someValueA = someValueA * 5;
            someValueB /= 5; // someValueB = someValueB / 5;

            // ���� ������ => (++, --)
            int randomValue1 = Random.Range(0, 10);
            Debug.Log("My Random Value Before ++ : " + randomValue1);
            randomValue1++; // randomValue1 = randomValue1 + 1;
            Debug.Log("My Random Value After ++ : " + randomValue1);

            int randomValue2 = Random.Range(0, 10);
            Debug.Log("My Random Value Before -- : " + randomValue2);
            randomValue2--; // randomValue2 = randomValue2 - 1;
            Debug.Log("My Random Value After -- : " + randomValue2);
        }

        void SimpleFunction() // <= �Լ� or �޼ҵ�
        {
            Debug.Log("Called Simple Function!");
            Debug.Log("Called Simple Function!!");
            Debug.Log("Called Simple Function!!!");
        }

        void SumFunction(int a, int b) // <= �Լ�(�Ű�����:Parameter)
        {
            int sum = a + b;
            Debug.Log("A + B : " + sum);
        }
    }
}
