using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UCK
{
    public class SimpleGimmick2 : MonoBehaviour
    {
        public GameObject spawnObject;
        public Transform spawnPoint;

        public int spawnCount = 10;

        void Start()
        {
            // UseForCaseFunction();

            // �ݺ��� ���� : while(���ǽ�)
            int index = 0;
            while (index < spawnCount)
            {
                float randomX = Random.Range(-50, 50);
                float randomZ = Random.Range(-50, 50);

                Vector3 randomPosition = new Vector3(randomX, 5, randomZ);
                GameObject newSpawnObject = Instantiate(spawnObject, randomPosition, Quaternion.identity);
                newSpawnObject.SetActive(true);

                index++;
            }
        }

        private void UseForCaseFunction()
        {
            // �ݺ��� ���� : for(�ʱⰪ; ���ǽ�; ������)
            // �ʱⰪ : int i = 0
            // ���ǽ� : i < spawnCount
            // ������ : i++
            for (int i = 0; i < spawnCount; i++)
            {
                float randomX = Random.Range(-50, 50);
                float randomZ = Random.Range(-50, 50);

                Vector3 randomPosition = new Vector3(randomX, 5, randomZ);
                GameObject newSpawnObject = Instantiate(spawnObject, randomPosition, Quaternion.identity);
                newSpawnObject.SetActive(true);
            }
        }
    }
}

