using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UCK
{
    public class SampleCallByValue : MonoBehaviour
    {
        private void Start()
        {
            int x = 15;
            int y = -5;

            int result = UtilitySum(x, y);
            Debug.Log("Result: " + result);
        }

        public int UtilitySum(int a, int b)
        {
            return a + b;
        }

        // Call by Value

        // Call by Reference
    }
}
