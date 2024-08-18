using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UCK
{
    public class ChestBox : MonoBehaviour, IInteractable
    {
        public bool IsAutoInteract => false;
        public string Message => "Pickup - Chest Box";

        public void Interact(CharacterBase playerCharacter)
        {
            Debug.Log("ChestBox Interact");

            Interaction_UI.Instance.RemoveInteractionData(this);

            Destroy(gameObject);
        }
    }
}
