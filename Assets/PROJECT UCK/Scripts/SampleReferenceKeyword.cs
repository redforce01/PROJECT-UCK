using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UCK
{
    public class SampleReferenceKeyword : MonoBehaviour
    {
        private void Start()
        {
            int a = 10;
            int b = 12;
            int result = -1;
            bool flag = MyLogic(a, b, out result);

            Debug.Log(result); // 22
        }

        private bool MyLogic(int a, int b, out int result)
        {
            result = a + b;
            if (a > b)
                return true;
            else
                return false;
        }
    }
}
