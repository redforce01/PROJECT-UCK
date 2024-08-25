using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UCK
{
    public class Interaction_UI : MonoBehaviour
    {
        public static Interaction_UI Instance { get; private set; }


        [SerializeField] private Transform listItemRoot;
        [SerializeField] private Interaction_UI_ListItem listItemPrefab;

        private List<Interaction_UI_ListItem> createdItems = new List<Interaction_UI_ListItem>();
        private int selectedIndex = -1;

        private void Awake()
        {
            Instance = this;

            listItemPrefab.gameObject.SetActive(false);
        }

        public void AddInteractionData(IInteractable interactionData)
        {
            Interaction_UI_ListItem newItem = Instantiate(listItemPrefab, listItemRoot);
            newItem.gameObject.SetActive(true);
            newItem.InteractableData = interactionData;
            newItem.Message = interactionData.Message;
            newItem.IsSelected = false;

            createdItems.Add(newItem);

            if (createdItems.Count == 1)
            {
                selectedIndex = 0;
                newItem.IsSelected = true;
            }
        }

        public void RemoveInteractionData(IInteractable interactionData)
        {
            int index = createdItems.FindIndex(x => x.InteractableData == interactionData);
            if (index >= 0)
            {
                Destroy(createdItems[index].gameObject);
                createdItems.RemoveAt(index);
            }

            if (createdItems.Count > 0)
            {
                if (selectedIndex < createdItems.Count)
                {

                }
                else
                {
                    selectedIndex = 0;
                }
                createdItems[selectedIndex].IsSelected = true;
            }
            else
            {
                selectedIndex = -1;
            }
        }

        public void ExecuteInteractionData()
        {
            if (selectedIndex < 0)
                return;

            if (selectedIndex < createdItems.Count)
            {
                createdItems[selectedIndex].InteractableData.Interact(PlayerCharacter.Instance);
            }
        }

        public void ChangeSelection(float mouseWheel)
        {
            if (createdItems.Count <= 0)
                return;

            createdItems[selectedIndex].IsSelected = false;

            if (mouseWheel > 0)
            {
                selectedIndex--;
                if (selectedIndex < 0)
                {
                    selectedIndex = createdItems.Count - 1;
                }
            }
            else if (mouseWheel < 0)
            {
                selectedIndex++;
                if (selectedIndex >= createdItems.Count)
                {
                    selectedIndex = 0;
                }
            }

            createdItems[selectedIndex].IsSelected = true;
        }
    }
}
