using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UCK
{
    public class Projectile : MonoBehaviour
    {
        public LayerMask hitLayer;

        private Rigidbody rigid;

        private void Awake()
        {
            rigid = GetComponent<Rigidbody>();
        }

        public void SetForce(float force)
        {
            rigid.AddForce(transform.forward * force, ForceMode.Impulse);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (hitLayer == (hitLayer | (1 << other.gameObject.layer)))
            {
                Destroy(gameObject);
            }
        }
    }
}
