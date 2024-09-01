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

    /// 모든 파티클 이펙트들을 Prefab으로 들고 있고, 관리하는 클래스
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

                // duration 만큼 일정 시간 뒤에, 생성 된 이펙트를 삭제 하도록 예약한 것
                Destroy(newEffect, targetData.duration);
            }
        }
    }
}
