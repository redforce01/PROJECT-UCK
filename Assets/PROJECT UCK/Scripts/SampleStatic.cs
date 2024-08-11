using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UCK
{
    public class SampleUtility
    {
        public static int count;
        public const string NetworkIP = "adsfasdf";

        public void SampleMethod()
        {
            Debug.Log("Count : " + count);
        }

        public static void StaticMethod()
        {
            Debug.Log("Static Method Called");
        }
    }

    public class SampleStatic : MonoBehaviour
    {
        private SampleUtility sampleA;
        private SampleUtility sampleB;

        private void Start()
        {
            sampleA = new SampleUtility();
            sampleB = new SampleUtility();

            SampleUtility.count = 10;            
            sampleA.SampleMethod();
            sampleB.SampleMethod();

            SampleUtility.StaticMethod();
        }
    }
}
