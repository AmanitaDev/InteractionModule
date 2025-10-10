using Project.Equip;
using Project.Interaction;
using Project.Inventory.Interfaces;
using Project.Inventory.Items;
using UnityEngine;

namespace Project
{
    public class Ground : MonoBehaviour, IAttackable
    {
        public ItemData itemData;
        public GameObject itemToSpawn;
        public GameObject hitParticle;
        public IEquipable.EQUIPTYPE equipTypeToAttack;

        // called when the player hits the resource with a shovel
        public void OnAttack(IEquipable.EQUIPTYPE equipType, Vector3 hitPoint, Vector3 hitNormal)
        {
            if (equipTypeToAttack != equipType) return;
            
            // create a hole prefab at point
            Instantiate(itemToSpawn, hitPoint, Quaternion.identity);
            Destroy(Instantiate(hitParticle, hitPoint, Quaternion.LookRotation(hitNormal, Vector3.up)), 1.0f);
        }

        public string GetInteractPrompt()
        {
            if (EquipManager.instance.GetToolType() == equipTypeToAttack)
            {
                return $"Hit to dig";
            }
            else
            {
                return $"Need {equipTypeToAttack.ToString()} to dig";
            }
        }

        public PlayerInteraction.ACTIONTYPE GetActionType()
        {
            return itemData.actionType;
        }
    }
}