using Project.Inventory.Items;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Project.Equip
{
    public class EquipManager : MonoBehaviour
    {
        public Equip curEquip;
        public Transform equipParent;

        //singleton
        public static EquipManager instance;

        private void Awake()
        {
            instance = this;
        }

        void OnAttack(InputValue value)
        {
            if (curEquip == null) return;
                
            curEquip.OnAttackInput();
        }

        // called when we equip an item
        public void EquipNew(ItemData item)
        {
            UnEquip();
            curEquip = Instantiate(item.equipPrefab, equipParent).GetComponent<Equip>();
        }

        // called when we un-equip an item
        public void UnEquip()
        {
            if(curEquip != null)
            {
                Destroy(curEquip.gameObject);
                curEquip = null;
            }
        }
    }
}