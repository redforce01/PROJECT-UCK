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
        private int myCount; // 0 �̶�� ������ �ʱ�ȭ�� �̷������.
        private SampleData mySampleData; // Null �̶�� ������ �ʱ�ȭ�� �̷������.
        private SampleData mySampleData2; // Null �̶�� ������ �ʱ�ȭ�� �̷������.
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

        // public : ����������
        // void : ���� Ÿ��
        // ChangeCount : �Լ��� �̸�
        // () : �Ű����� �ڸ�
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
