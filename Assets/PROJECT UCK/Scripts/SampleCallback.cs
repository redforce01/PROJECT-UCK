using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UCK
{
    public class SampleCallback : MonoBehaviour
    {
        public delegate void SampleCallbackDelegate(int valueA, int valueB);
        private SampleCallbackDelegate myCallback; // Null 이 들어가있음

        private System.Action<int, int> myAction; // Null 이 들어가있음

        private void Start()
        {
            //myCallback += CallBackFunctionA;
            //myCallback += CallbackFunctionB;
            //myCallback(20, 30);

            myAction += CallBackFunctionA;
            myAction += CallbackFunctionB;
            myAction(10, 5);

            //if (myAction != null)
            //{
            //    myAction(10, 5);
            //}

            myAction ? .Invoke(10, 5);
        }

        private void CallBackFunctionA(int a, int b)
        {
            Debug.Log("Callback Function A Called");
            Debug.Log("A Result :" + (a + b));
        }

        private void CallbackFunctionB(int a, int b)
        {
            Debug.Log("Callback Function B Called");
            Debug.Log("B Result :" + (a - b));
        }
    }
}
