using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UCK
{
    public class InteractionSensor : MonoBehaviour
    {


        public System.Action<IInteractable> OnDetected;
        public System.Action<IInteractable> OnLostSignal;

        private void OnTriggerEnter(Collider other)
        {
            if (other.transform.root.TryGetComponent(out IInteractable interactable))
            {
                OnDetected?.Invoke(interactable);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.transform.root.TryGetComponent(out IInteractable interactable))
            {
                OnLostSignal?.Invoke(interactable);
            }
        }
    }
}
