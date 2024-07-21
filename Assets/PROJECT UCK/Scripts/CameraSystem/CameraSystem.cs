using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UCK
{
    public class CameraSystem : MonoBehaviour
    {
        public static CameraSystem Instance { get; private set; } = null;
        public Vector3 AimingTargetPoint { get; protected set; } = Vector3.zero;


        public LayerMask aimingLayers;

        private Camera mainCamera;

        private void Awake()
        {
            Instance = this;
            mainCamera = Camera.main;
        }

        private void OnDestroy()
        {
            Instance = null;
        }

        private void Update()
        {
            Ray ray = mainCamera.ScreenPointToRay(new Vector2(Screen.width * 0.5f, Screen.height * 0.5f));
            if (Physics.Raycast(ray, out RaycastHit hit, 1000f, aimingLayers, QueryTriggerInteraction.Ignore))
            {
                AimingTargetPoint = hit.point;
            }
            else
            {
                AimingTargetPoint = ray.GetPoint(1000f);
            }
        }
    }
}
