using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UCK
{
    public class SimpleEventCycle : MonoBehaviour
    {
        private void Awake()
        {
            Debug.Log("Awake");
        }

        private void Start()
        {
            Debug.Log("Start");
        }

        private void OnEnable()
        {
            Debug.Log("OnEnable");
        }

        private void OnDisable()
        {
            Debug.Log("OnDisable");
        }

        private void Update()
        {
            Debug.Log("Update");
        }

        private void FixedUpdate()
        {
            Debug.Log("FixedUpdate");
        }

        private void LateUpdate()
        {
            Debug.Log("LateUpdate");
        }
    }
}