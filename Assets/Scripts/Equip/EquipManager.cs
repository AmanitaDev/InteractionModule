using Project.Inventory.Items;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

namespace Project.Equip
{
    public class EquipManager : MonoBehaviour
    {
        [FormerlySerializedAs("curEquip")] public IEquipable curEquipable;
        public Transform equipParent;

        //singleton
        public static EquipManager instance;
        //public Animator _animator;

        private void Awake()
        {
            instance = this;
        }

        public IEquipable.EQUIPTYPE GetToolType()
        {
            if (curEquipable == null)
            {
                return IEquipable.EQUIPTYPE.NULL;
            }
            else
            {
                 return curEquipable.GetEquipType();
            }
        }

        void OnAttack(InputValue value)
        {
            if (curEquipable == null) return;
                
            curEquipable.OnAttackInput();
            //_animator.SetTrigger("OnHit");
            //_animator.SetTrigger("OnDig");
        }

        // called when we equip an item
        public void EquipNew(ItemData item)
        {
            UnEquip();
            curEquipable = Instantiate(item.equipPrefab, equipParent).GetComponent<IEquipable>();
        }

        // called when we un-equip an item
        public void UnEquip()
        {
            if(curEquipable != null)
            {
                Destroy(curEquipable.GetGameObject());
                curEquipable = null;
            }
        }
    }
}