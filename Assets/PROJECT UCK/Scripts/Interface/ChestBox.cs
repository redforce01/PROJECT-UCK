using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UCK
{
    public class ChestBox : MonoBehaviour, IInteractable
    {
        public bool IsAutoInteract => false;

        public void Interact(CharacterBase playerCharacter)
        {
            Debug.Log("ChestBox Interact");
            Destroy(gameObject);
        }
    }
}
