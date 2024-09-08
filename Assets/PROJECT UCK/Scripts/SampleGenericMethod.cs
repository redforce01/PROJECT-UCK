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

        // Generic 을 이용해서 하나의 함수로 여러 타입의 값을 받을 수 있게 구현
        public void ShowLog<T, K>(T parameterA, K parameterB)
        {
            Debug.Log(parameterA);
            Debug.Log(parameterB);
        }

        #region 여러개의 함수를 Overloading 으로 구현한 경우
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
