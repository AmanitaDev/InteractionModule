using UnityEngine;

namespace Project.Equip
{
    [RequireComponent(typeof(Animator))]
    public class EquipableFood : MonoBehaviour, IEquipable
    {
        public IEquipable.EQUIPTYPE equipType;
        
        // called when we press the attack input
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
            // TODO check inventory count
            // consume food
            // remove one from inventory
            // add energy
            Debug.LogError("Add energy");
        }

        public void OnHit()
        {
            
        }
    }
}