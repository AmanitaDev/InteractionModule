using System;
using Project.Inventory.Items;
using Project.Inventory.UI;
using Survival.Player;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Project.Inventory
{
    public class QuickSlotsInventory : MonoBehaviour
    {
        public ItemSlotUI[] uiSlots;
        public ItemSlot[] slots;

        public Transform dropPosition;

        [Header("Selected Item")] private ItemSlot selectedItem;
        private ItemSlotUI selectedSlot;
        private int selectedItemIndex;

        private int curEquipIndex;

        // singleton
        public static QuickSlotsInventory instance;

        void Awake()
        {
            instance = this;
        }

        private void Start()
        {
            slots = new ItemSlot[uiSlots.Length];
            // initialize the slots
            for (int x = 0; x < slots.Length; x++)
            {
                slots[x] = new ItemSlot();
                uiSlots[x].index = x;
                uiSlots[x].Clear();
            }

            selectedSlot = uiSlots[0];
            selectedSlot.Select();
        }

        /// <summary>
        /// This input is coming from PlayerInput component
        /// </summary>
        /// <param name="value"></param>
        void OnPrevious(InputValue value)
        {
            SelectPreviousSlot();
        }

        /// <summary>
        /// This input is coming from PlayerInput component
        /// </summary>
        /// <param name="value"></param>
        void OnNext(InputValue value)
        {
            SelectNextSlot();
        }

        /// <summary>
        /// This input is coming from PlayerInput component
        /// </summary>
        /// <param name="value"></param>
        void OnScrollWheel(InputValue value)
        {
            int scrollY = (int)value.Get<Vector2>().y;
            if (scrollY < 0)
            {
                SelectNextSlot();
            }
            else if (scrollY > 0)
            {
                SelectPreviousSlot();
            }
        }

        /// <summary>
        /// Select Next slot from UiSlots
        /// </summary>
        void SelectNextSlot()
        {
            selectedSlot.Deselect();
            selectedItemIndex++;
            selectedItemIndex = Mathf.Clamp(selectedItemIndex, 0, uiSlots.Length - 1);
            selectedSlot = uiSlots[selectedItemIndex];
            selectedSlot.Select();
            selectedItem = selectedSlot.GetCurrrentSlot();
            UpdateEquip();
        }

        /// <summary>
        /// Select Previous slot from UiSlots
        /// </summary>
        void SelectPreviousSlot()
        {
            selectedSlot.Deselect();
            selectedItemIndex--;
            selectedItemIndex = Mathf.Clamp(selectedItemIndex, 0, uiSlots.Length - 1);
            selectedSlot = uiSlots[selectedItemIndex];
            selectedSlot.Select();
            selectedItem = selectedSlot.GetCurrrentSlot();
            UpdateEquip();
        }

        /// <summary>
        /// adds the requested item to the player's inventory
        /// </summary>
        /// <param name="item"> item to add </param>
        public void AddItem(ItemData item)
        {
            // does this item have a stack it can be added to?
            if (item.canStack)
            {
                ItemSlot slotToStackTo = GetItemStack(item);
                if (slotToStackTo != null)
                {
                    slotToStackTo.quantity++;
                    UpdateUI();
                    return;
                }
            }

            ItemSlot emptySlot = GetEmptySlot();
            // do we have an empty slot for the item?
            if (emptySlot != null)
            {
                emptySlot.item = item;
                emptySlot.quantity = 1;
                UpdateUI();
                UpdateEquip();
                return;
            }

            // if the item can't stack and there are no empty slots - throw it away
            ThrowItem(item);
        }

        /// <summary>
        /// spawns the item in front of the player
        /// </summary>
        /// <param name="item"> item to spawn </param>
        void ThrowItem(ItemData item)
        {
            Instantiate(item.dropPrefab, dropPosition.position,
                Quaternion.Euler(Vector3.one * UnityEngine.Random.value * 360.0f));
        }

        /// <summary>
        /// updates the UI slots
        /// </summary>
        void UpdateUI()
        {
            for (int x = 0; x < slots.Length; x++)
            {
                if (slots[x].item != null)
                    uiSlots[x].Set(slots[x]);
                else
                    uiSlots[x].Clear();
            }
        }

        /// <summary>
        /// Updates the equiped item if its exist
        /// </summary>
        void UpdateEquip()
        {
            selectedItem = selectedSlot.GetCurrrentSlot();
            
            if (selectedItem != null)
            {
                Equip();
            }
            else
            {
                UnEquip();
            }
        }

        // returns the item slot that the requested item can be stacked on
        // returns null if there is no stack available
        ItemSlot GetItemStack(ItemData item)
        {
            for (int x = 0; x < slots.Length; x++)
            {
                if (slots[x].item == item && slots[x].quantity < item.maxStackAmount)
                    return slots[x];
            }

            return null;
        }

        // returns an empty slot in the inventory
        // if there are no empty slots - return null
        ItemSlot GetEmptySlot()
        {
            for (int x = 0; x < slots.Length; x++)
            {
                if (slots[x].item == null)
                    return slots[x];
            }

            return null;
        }

        // called when we click on an item slot
        public void SelectItem(int index)
        {
            if (slots[index].item == null)
                return;

            selectedItem = slots[index];
            selectedItemIndex = index;
        }

        // called when the "Equip" button is pressed
        public void Equip()
        {
            uiSlots[selectedItemIndex].selected = true;
            curEquipIndex = selectedItemIndex;
            EquipManager.instance.EquipNew(selectedItem.item);
            UpdateUI();

            SelectItem(selectedItemIndex);
        }

        // unequips the requested item
        void UnEquip()
        {
            EquipManager.instance.UnEquip();
            UpdateUI();
        }

        /*

         // called when the "Drop" button is pressed
         public void OnDropButton()
         {
             ThrowItem(selectedItem.item);
             RemoveSelectedItem();
         }*/

        // does the player have "quantity" amount of "item"s?
        public bool HasItems(ItemData item, int quantity)
        {
            int amount = 0;
            for (int i = 0; i < slots.Length; i++)
            {
                if (slots[i].item == item)
                    amount += slots[i].quantity;
                if (amount >= quantity)
                    return true;
            }

            return false;
        }
    }

    // stores information about an item slot in the inventory
    [Serializable]
    public class ItemSlot
    {
        public ItemData item;
        public int quantity;
    }
}