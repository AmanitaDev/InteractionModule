using Project.Interaction;
using Project.Inventory.Interfaces;
using UnityEngine;

namespace Project.Inventory.Items
{
    public class ItemObject : MonoBehaviour, IInteractable
    {
        public ItemData itemData;
        
        public string GetInteractPrompt()
        {
            return $"Pickup {itemData.displayName}";
        }

        public void OnInteraction()
        {
            QuickSlotsInventory.instance.AddItem(itemData);
            Destroy(gameObject);
        }
    }
}