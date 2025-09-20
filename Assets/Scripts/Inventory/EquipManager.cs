using Project.Inventory.Items;
using UnityEngine;

namespace Survival.Player
{
    public class EquipManager : MonoBehaviour
    {
        public Equip curEquip;
        public Transform equipParent;

        //private PlatformerController controller;

        //singleton
        public static EquipManager instance;

        private void Awake()
        {
            instance = this;
            //controller = GetComponent<PlatformerController>();
        }

        /*// called when we press the Left Mouse Button - managed by the Input System
        public void OnAttackInput(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Performed && curEquip != null)
            {
                curEquip.OnAttackInput();
            }
        }
        
        // called when we press the Right Mouse Button - managed by the Input System
        public void OnAltAttackInput (InputAction.CallbackContext context)
        {
            if(context.phase == InputActionPhase.Performed && curEquip != null)
            {
                curEquip.OnAltAttackInput();
            }
        }*/

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