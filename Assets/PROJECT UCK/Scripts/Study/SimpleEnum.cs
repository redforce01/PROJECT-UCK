using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UCK
{
    public enum SimpleEnumType
    {
        Unknown = 0,
        Player,
        Monster,
        NPC,
        Animal,
    }

    public class SimpleEnum : MonoBehaviour
    {
        public SimpleEnumType myType = SimpleEnumType.Player;

        public int myValue;             // �⺻�� : 0 
        public float myFloatValue;      // �⺻�� : 0
        public bool myBoolValue;        // �⺻�� : false
        public double myDoubleValue;    // �⺻�� : 0
        public string myStringValue;    // �⺻�� : ����ִ� null
        public SimpleEnumType myEnumType; // Enum ���� �⺻ ���� 0 �̴�.

        void Start()
        {
            Debug.Log("Who Am I ?");
        }

        void Update()
        {
            if (myType == SimpleEnumType.Player)
            {
                Debug.Log("I am Player");
            }
            else if (myType == SimpleEnumType.NPC)
            {
                Debug.Log("I am NPC");
            }
            else if (myType == SimpleEnumType.Monster)
            {
                Debug.Log("I am Monster");
            }
            else if (myType == SimpleEnumType.Animal)
            {
                Debug.Log("I am Animal");
            }

            Debug.Log("Enum Value : " + (int)myType);
            // Debug.Log("I am :" + myType);
        }
    }
}

