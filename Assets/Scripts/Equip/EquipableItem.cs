using UnityEngine;

namespace Project.Equip
{
    public class EquipableItem : MonoBehaviour, IEquipable
    {
        public IEquipable.EQUIPTYPE equipType;
        
        public IEquipable.EQUIPTYPE GetEquipType()
        {
            return equipType;
        }

        public GameObject GetGameObject()
        {
            return gameObject;
        }

        public void OnAttackInput()
        {
            throw new System.NotImplementedException();
        }

        public void OnHit()
        {
            throw new System.NotImplementedException();
        }
    }
}