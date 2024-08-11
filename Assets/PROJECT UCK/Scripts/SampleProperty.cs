using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UCK
{    
    public class SamplePropertyData
    {
        public int sampleValue;
        // private : 외부에서 접근이 불가능하다. => 변경이 불가능하다.
        // public : 외부에서 접근이 가능하다.
        // protected : 자식클래스까지만 접근이 가능하다(외부는 불가능)

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
        private SamplePropertyData myPropertyData; // Null 이 들어가있음

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
