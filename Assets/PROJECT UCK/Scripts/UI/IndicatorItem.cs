using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace UCK
{
    public class IndicatorItem : MonoBehaviour
    {
        public TMPro.TextMeshProUGUI text;

        [Range(0f, 1f)] public float borderOffsetX;
        [Range(0f, 1f)] public float borderOffsetY;

        private Transform target; // 따라갈려고하는 진짜 오브젝트의 Transform

        public void SetTarget(Transform target)
        {
            this.target = target;
        }

        private void Update()
        {
            UpdatePosition();
        }

        public void UpdatePosition()
        {
            Vector3 targetViewportPos = Camera.main.WorldToViewportPoint(target.position);
            float distance = Vector3.Distance(Camera.main.transform.position, target.position);

            bool isInsideOnScreen = true;
            bool isBehind = targetViewportPos.z < 0;
            if (isBehind || 
                targetViewportPos.x < 0 || targetViewportPos.x > Screen.width ||
                targetViewportPos.y < 0 || targetViewportPos.y > Screen.height) // 화면 바깥으로 나가있는 상태
            {
                isInsideOnScreen = false;
            }

            if (distance > 10)
            {
                text.text = $"{distance:0.0}m";
            }
            else
            {
                if (false == isInsideOnScreen)
                {
                    text.text = $"{distance:0.0}m";
                }
            }

            if (false == isInsideOnScreen)
            {
                if (targetViewportPos.x < borderOffsetX)
                {
                    targetViewportPos.x = borderOffsetX;
                }

                if (targetViewportPos.x > 1 - borderOffsetX)
                {
                    targetViewportPos.x = 1 - borderOffsetX;
                }

                if (isBehind || targetViewportPos.y < borderOffsetY)
                {
                    targetViewportPos.y = borderOffsetY;
                }

                if (targetViewportPos.y > 1 - borderOffsetY)
                {
                    targetViewportPos.y = 1 - borderOffsetY;
                }
            }

            transform.position = Camera.main.ViewportToScreenPoint(targetViewportPos);
        }
    }
}
