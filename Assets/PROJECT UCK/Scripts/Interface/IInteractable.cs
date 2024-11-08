using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UCK
{
    public interface IInteractable
    {
        public bool IsAutoInteract { get; }
        public string Message { get; }


        public void Interact(CharacterBase playerCharacter);
    }
}
