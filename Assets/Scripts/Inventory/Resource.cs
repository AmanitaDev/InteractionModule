using Project.Equip;
using Project.Interaction;
using Project.Inventory.Interfaces;
using Project.Inventory.Items;
using UnityEngine;

namespace Project.Inventory
{
    public class Resource : MonoBehaviour, IAttackable
    {
        public ItemData itemData;
        public ItemData itemToGive;
        public int quantityPerHit = 1;
        public int capacity;
        public GameObject hitParticle;
        public IEquipable.EQUIPTYPE equipTypeToGather;

        // called when the player hits the resource with an axe
        public void OnAttack(IEquipable.EQUIPTYPE equipType, Vector3 hitPoint, Vector3 hitNormal)
        {
            if (equipTypeToGather != equipType) return;
            
            // give the player "quantityPerHit" of the resource
            for (int i = 0; i < quantityPerHit; i++)
            {
                if (capacity <= 0)
                    break;

                capacity -= 1;
                QuickSlotsInventory.instance.AddItem(itemToGive);
            }

            Destroy(Instantiate(hitParticle, hitPoint, Quaternion.LookRotation(hitNormal, Vector3.up)), 1.0f);

            // if we're empty, destroy the resource
            if (capacity <= 0) 
                Destroy(gameObject);
        }

        public string GetInteractPrompt()
        {
            if (EquipManager.instance.GetToolType() == equipTypeToGather)
            {
                return $"Hit to get {itemToGive.displayName}";
            }
            else
            {
                return $"Need {equipTypeToGather.ToString()} to gather";
            }
        }

        public PlayerInteraction.ACTIONTYPE GetActionType()
        {
            return itemData.actionType;
        }
    }
}