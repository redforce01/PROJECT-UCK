using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UCK
{
    public enum EffectType
    {
        Rock,
        Wood,
    }

    [System.Serializable]
    public class EffectPrefabData
    {
        public EffectType type;
        public GameObject prefab;
        public float duration = 2f;
    }

    /// ��� ��ƼŬ ����Ʈ���� Prefab���� ��� �ְ�, �����ϴ� Ŭ����
    public class UCKEffectSystem : MonoBehaviour
    {
        public static UCKEffectSystem Instance { get; private set; } = null;

        public List<EffectPrefabData> effectPrefabs = new List<EffectPrefabData>();

        private void Awake()
        {
            Instance = this;
        }

        private void OnDestroy()
        {
            Instance = null;
        }

        public void SpawnEffect(EffectType type, Vector3 position, Quaternion rotation)
        {
            EffectPrefabData targetData = effectPrefabs.Find(x => x.type == type);
            if (targetData != null)
            {
                GameObject prefab = targetData.prefab;
                GameObject newEffect = Instantiate(prefab, position, rotation);
                newEffect.gameObject.SetActive(true);

                // duration ��ŭ ���� �ð� �ڿ�, ���� �� ����Ʈ�� ���� �ϵ��� ������ ��
                Destroy(newEffect, targetData.duration);
            }
        }
    }
}
