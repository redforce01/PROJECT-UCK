using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UCK
{
    [System.Serializable]
    public class SampleDataObject
    {
        public string name;
        public int age;
        public float height;
    }

    public class SampleCallByReference : MonoBehaviour
    {
        public string orinial;
        public string after;

        public SampleDataObject dataA;

        private void Start()
        {
            orinial = "Hello World";
            after = ChangeText(orinial);

            dataA = new SampleDataObject();
            dataA.name = "Unity";
            dataA.age = 2024;
            dataA.height = 180f;

            ChangeData(dataA);
        }

        private string ChangeText(string before)
        {
            before = "Hello Unity";
            return before;
        }

        private void ChangeData(SampleDataObject data)
        {
            data.name = "Unreal";
            data.age = 123123;
            data.height = 200.09f;
        }
    }
}
