using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UCK
{
    public class SampleData
    {
        public float sampleFloat;
        public bool sampleFlag;

        public void SampleFunction()
        {
            Debug.Log("Sample Function");
        }
    }

    public class SampleCode : MonoBehaviour
    {
        private int myCount; // 0 이라는 값으로 초기화가 이루어진다.
        private SampleData mySampleData; // Null 이라는 값으로 초기화가 이루어진다.
        private SampleData mySampleData2; // Null 이라는 값으로 초기화가 이루어진다.
        private SampleData mySampleData3;
        private SampleData mySampleData4;

        private void Start()
        {
            Debug.Log("Before : " + mySampleData);
            mySampleData = new SampleData();
            Debug.Log("After : " + mySampleData);

            mySampleData.sampleFloat = 3.1f;
            mySampleData.sampleFlag = true;
            mySampleData.SampleFunction();

            myCount = 99;
            Debug.Log("My Count Value : " + myCount);
            ChangeCount(70);
            int currentValue = GetMyCount();
            Debug.Log("Get My Count : " + currentValue);
        }

        // public : 접근한정자
        // void : 리턴 타입
        // ChangeCount : 함수의 이름
        // () : 매개변수 자리
        public void ChangeCount(int value)
        {
            myCount = value;
            Debug.Log("My Count Value : " + myCount);
        }

        public int GetMyCount()
        {
            return myCount;
        }
    }
}
