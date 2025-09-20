using Project.Inventory;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Project.Inventory.UI
{
    public class ItemSlotUI : MonoBehaviour
    {
        public Button button;
        public Image icon;
        public TextMeshProUGUI quantityText;
        private ItemSlot curSlot;
        private Outline outline;

        public int index;
        public bool selected;

        void Awake()
        {
            outline = GetComponent<Outline>();
        }

        void OnEnable()
        {
            outline.enabled = selected;
        }

        public bool IsOccupied()
        {
            Debug.Log(curSlot != null);
            return curSlot != null;
        }

        public ItemSlot GetCurrrentSlot()
        {
            return curSlot;
        }

        public void Select()
        {
            selected = true;
            outline.enabled = true;
        }

        public void Deselect()
        {
            selected = false;
            outline.enabled = false;
        }

        // sets the item to be displayed in the slot
        public void Set(ItemSlot slot)
        {
            curSlot = slot;
            icon.gameObject.SetActive(true);
            icon.sprite = slot.item.icon;
            quantityText.text = slot.quantity > 1 ? slot.quantity.ToString() : string.Empty;
            if (outline != null)
                outline.enabled = selected;
        }

        // clears the item slot
        public void Clear()
        {
            curSlot = null;
            icon.gameObject.SetActive(false);
            quantityText.text = string.Empty;
        }
    }
}