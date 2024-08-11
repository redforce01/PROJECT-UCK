using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UCK
{    
    public class SamplePropertyData
    {
        public int sampleValue;
        // private : �ܺο��� ������ �Ұ����ϴ�. => ������ �Ұ����ϴ�.
        // public : �ܺο��� ������ �����ϴ�.
        // protected : �ڽ�Ŭ���������� ������ �����ϴ�(�ܺδ� �Ұ���)

        public int SamplePropertyValue 
        {
            //get => sampleValue;
            //set => sampleValue = value;

            get
            {
                return sampleValue;
            }
            set
            {
                sampleValue = value;
                Debug.Log(sampleValue);
            }
        }
    }

    public class SampleProperty : MonoBehaviour
    {
        private SamplePropertyData myPropertyData; // Null �� ������

        private void Start()
        {
            myPropertyData = new SamplePropertyData();
            myPropertyData.sampleValue = 10;
            myPropertyData.SamplePropertyValue = 20;
            //Debug.Log("Sample Data.Value : " + myPropertyData.sampleValue);
            Debug.Log("Sample Data.Value : " + myPropertyData.SamplePropertyValue);
        }
    }
}
