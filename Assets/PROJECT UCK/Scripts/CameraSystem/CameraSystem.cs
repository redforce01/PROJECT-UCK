using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UCK
{
    public class CameraSystem : MonoBehaviour
    {

        public static CameraSystem Instance { get; private set; } = null;

        public Camera mainCamera;
        public CinemachineVirtualCamera defaultCamera;
        public CinemachineVirtualCamera aimingCamera;

        public Vector3 AimingTargetPoint { get; protected set; } = Vector3.zero;
        public LayerMask aimingLayers;

        private bool isRightCameraSide = false;
        private float cameraSideBlend = 0f;

        private void Awake()
        {
            Instance = this;
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

            cameraSideBlend = Mathf.Lerp(cameraSideBlend, isRightCameraSide ? 0f : 1f, Time.deltaTime * 10f);
            var defaultOfThirdPersonFollow = defaultCamera.GetCinemachineComponent<Cinemachine3rdPersonFollow>();
            defaultOfThirdPersonFollow.CameraSide = cameraSideBlend;

            var aimingOfThirdPersonFollow = aimingCamera.GetCinemachineComponent<Cinemachine3rdPersonFollow>();
            aimingOfThirdPersonFollow.CameraSide = cameraSideBlend;
        }

        public void SetActiveAimingCamera(bool isAiming)
        {
            aimingCamera.gameObject.SetActive(isAiming);
        }

        public void SetChangeCameraSide()
        {
            isRightCameraSide = !isRightCameraSide;
        }
    }
}
