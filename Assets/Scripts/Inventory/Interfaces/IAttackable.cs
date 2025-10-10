using Project.Equip;
using Project.Interaction;
using UnityEngine;

namespace Project.Inventory.Interfaces
{
    public interface IAttackable
    {
        string GetInteractPrompt();
        PlayerInteraction.ACTIONTYPE GetActionType();
        void OnAttack(IEquipable.EQUIPTYPE equipType, Vector3 hitPoint, Vector3 hitNormal);
    }
}