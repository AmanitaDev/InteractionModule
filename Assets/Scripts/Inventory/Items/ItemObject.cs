using Project.Inventory.Interfaces;
using UnityEngine;

namespace Project.Inventory.Items
{
    public class ItemObject : MonoBehaviour, IInteractable
    {
        public ItemData item;
        
        public string GetInteractPrompt()
        {
            return string.Format("Pickup {0}", item.displayName);
        }

        public void OnInteraction()
        {
            QuickSlotsInventory.instance.AddItem(item);
            Destroy(gameObject);
        }
    }
}