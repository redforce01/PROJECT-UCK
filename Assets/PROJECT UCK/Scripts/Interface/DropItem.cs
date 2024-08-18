using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UCK
{
    public class DropItem : MonoBehaviour, IInteractable
    {
        public bool IsAutoInteract => true;
        public string Message => string.Empty;


        private Transform followTarget;

        public void Interact(CharacterBase playerCharacter)
        {
            followTarget = playerCharacter.transform;
            StartCoroutine(FollowPlayer());
        }

        private IEnumerator FollowPlayer()
        {
            Vector3 targetPosition = followTarget.position;
            while (Vector3.Distance(transform.position, targetPosition) > 0.1f)
            {
                transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * 5f);
                yield return null;
            }

            Destroy(gameObject);
        }
    }
}
