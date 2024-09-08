using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UCK
{
    public class SampleStaticUtility
    {
        public const float PI = 3.14f;

        public static float GetCircleArea(float radius)
        {
            return PI * radius * radius;
        }
    }


    public class SampleStatic_Remember : MonoBehaviour
    {
        private void Start()
        {
            Debug.Log("원주율 : " + SampleStaticUtility.PI);
            Debug.Log("반 지름이 5인 원의 넓이 : " + SampleStaticUtility.GetCircleArea(5));

        }
    }
}
