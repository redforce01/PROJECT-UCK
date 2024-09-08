using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UCK
{
    public class SampleGenericMethod : MonoBehaviour
    {
        private void Start()
        {
            ShowLog(1, 2.3f);
            ShowLog(19.0f, "Hello");
            ShowLog("Hello", 234234);
        }

        // Generic �� �̿��ؼ� �ϳ��� �Լ��� ���� Ÿ���� ���� ���� �� �ְ� ����
        public void ShowLog<T, K>(T parameterA, K parameterB)
        {
            Debug.Log(parameterA);
            Debug.Log(parameterB);
        }

        #region �������� �Լ��� Overloading ���� ������ ���
        //public void ShowLog(int value)
        //{
        //    Debug.Log(value);
        //}

        //public void ShowLog(float value)
        //{
        //    Debug.Log(value);
        //}

        //public void ShowLog(string value)
        //{
        //    Debug.Log(value);
        //}
        #endregion

    }
}
