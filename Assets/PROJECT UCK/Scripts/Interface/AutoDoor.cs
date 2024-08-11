using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UCK
{
    public class AutoDoor : MonoBehaviour, IInteractable
    {
        public bool IsAutoInteract => true;

        public float waitTime = 3f;
        public Transform leftDoor;
        public Transform rightDoor;
        public Vector3 leftDoorOpenPosition;
        public Vector3 leftDoorClosedPosition;
        public Vector3 rightDoorOpenPoisition;
        public Vector3 rightDoorClosedPosition;

        private bool isOpened;
        private float lastOpenedTime;

        public void Interact(CharacterBase playerCharacter)
        {
            isOpened = true;
            lastOpenedTime = Time.time;
        }

        private void Update()
        {
            if (isOpened)
            {
                leftDoor.localPosition = Vector3.Lerp(leftDoor.localPosition, leftDoorOpenPosition, Time.deltaTime);
                rightDoor.localPosition = Vector3.Lerp(rightDoor.localPosition, rightDoorOpenPoisition, Time.deltaTime);

                if (Time.time > lastOpenedTime + waitTime)
                {
                    isOpened = false;
                }
            }
            else
            {
                leftDoor.localPosition = Vector3.Lerp(leftDoor.localPosition, leftDoorClosedPosition, Time.deltaTime);
                rightDoor.localPosition = Vector3.Lerp(rightDoor.localPosition, rightDoorClosedPosition, Time.deltaTime);
            }
        }
    }
}
