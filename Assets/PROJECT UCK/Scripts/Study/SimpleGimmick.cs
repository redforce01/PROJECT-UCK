using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UCK
{
    public class SimpleGimmick : MonoBehaviour
    {
        public Transform spawnPoint;
        public GameObject spawnObject;

        public float spawnDelay = 0.3f;
        public float spawnTime = 0.0f;

        public bool isActiveGimmick = false;

        private void Update()
        {
            if (isActiveGimmick == true)
            {
                if (Time.time > spawnTime)
                {
                    GameObject newObject = Instantiate(spawnObject, spawnPoint.position, Quaternion.identity);
                    newObject.SetActive(true);

                    spawnTime = Time.time + spawnDelay;
                }
            }           
        }
    }
}

