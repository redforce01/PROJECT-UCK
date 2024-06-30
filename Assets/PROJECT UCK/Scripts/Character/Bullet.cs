using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UCK
{
    public class Bullet : MonoBehaviour
    {
        public float speed = 10f;

        private Rigidbody bulletRigidbody;

        private void Start()
        {
            bulletRigidbody = GetComponent<Rigidbody>();
            bulletRigidbody.AddForce(transform.forward * speed, ForceMode.Impulse);
        }

        //private void Update()
        //{
        //    transform.Translate(transform.forward * speed * Time.deltaTime);
        //}
    }
}

