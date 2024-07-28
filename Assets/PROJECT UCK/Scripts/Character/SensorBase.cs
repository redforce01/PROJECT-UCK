using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UCK
{
    public class SensorBase : MonoBehaviour
    {
        public LayerMask sensorTargetLayers;
        public List<GameObject> detectedObjects = new List<GameObject>();
        public List<GameObject> ignoreList = new List<GameObject>();

        public System.Action<GameObject> OnDetectedCallback;
        public System.Action<GameObject> OnLostSignalCallback;


        public void Start()
        {
            // 자기 자신은 Sensor로 인지하지 않도록, 무시하기 위해 Root 오브젝트를 Ignore List에 추가 한다.
            ignoreList.Add(transform.root.gameObject);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (ignoreList.Exists(x => x == other.transform.root.gameObject))
            {
                return;
            }

            if (IsLayerMatched(other.gameObject.layer))
            {
                OnDetectedCallback?.Invoke(other.gameObject);
                detectedObjects.Add(other.gameObject);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (ignoreList.Exists(x => x == other.transform.root.gameObject))
            {
                return;
            }

            if (IsLayerMatched(other.gameObject.layer))
            {
                OnLostSignalCallback?.Invoke(other.gameObject);
                detectedObjects.Remove(other.gameObject);
            }
        }

        private bool IsLayerMatched(int layer)
        {
            return (sensorTargetLayers.value & (1 << layer)) != 0;
        }
    }
}
