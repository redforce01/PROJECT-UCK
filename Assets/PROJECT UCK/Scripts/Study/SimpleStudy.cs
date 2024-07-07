using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UCK
{
    public class SimpleStudy : MonoBehaviour
    {
        private void Start()
        {
            // ���� �̸� :
            // 1. ���ڷ� ����
            // 2. Ư������ ��� �ȵ�
            // 3. ���� ��� �ȵ�
            // 4. ����� ��� �ȵ�
            // 5. ��ҹ��� ����
            // 6. ���� �̸��� �ǹ̸� �ο��Ͽ� �ۼ�
            // 7. ���� �̸��� ���� �ۼ�

            int value_int = 12; // int : ������ ����
            float value_float = 10.81f; // float : �Ǽ��� ����
            string value_string = "ABCDE"; // string : ����(��) ����
            char value_char = 'Y'; // char : ���� ����
            bool value_bool = true; // bool : ���� ���� (true or false)

            Debug.Log("Value : " + value_int);
            Debug.Log("Value : " + value_float);
            Debug.Log("Value : " + value_string);
            Debug.Log("Value : " + value_char);
            Debug.Log("Value : " + value_bool);


            int characterHP = 25;
            // ���ǹ� : if, else if, else
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

            // �ݺ��� : for
            // for (�ʱ�ȭ; �ݺ� ����; ����)
            for (int i = 0; i < 10; i++)
            {
                Debug.Log("Index : " + i);
            }

            // ������ ���
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

