using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UCK
{
    public class Indicator_UI : UIBase
    {
        public static Indicator_UI Instance { get; private set; }


        public List<GameObject> indicatorTestTargets = new List<GameObject>();

        public IndicatorItem indicatorPrefab;

        private Dictionary<Transform, IndicatorItem> createdIndicators 
            = new Dictionary<Transform, IndicatorItem>();

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            for (int i = 0; i < indicatorTestTargets.Count; i++)
            {
                CreateIndicator(indicatorTestTargets[i].transform);
            }
        }

        public void CreateIndicator(Transform target)
        {
            IndicatorItem newIndicatorItem = Instantiate(indicatorPrefab, transform);
            newIndicatorItem.SetTarget(target);
            newIndicatorItem.gameObject.SetActive(true);
            createdIndicators.Add(target, newIndicatorItem);
        }

        public void RemoveIndicator(Transform target)
        {
            if (createdIndicators.ContainsKey(target))
            {
                Destroy(createdIndicators[target].gameObject);
                createdIndicators.Remove(target);
            }
        }
    }
}
