using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UCK
{
    public class Container : MonoBehaviour
    {
        public int insideBulletCount = 0;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Bullet"))
            {
                insideBulletCount++;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Bullet"))
            {
                insideBulletCount--;
            }
        }
    }
}

