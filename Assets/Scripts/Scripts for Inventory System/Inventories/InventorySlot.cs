using RPGeeks.HUDS;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using RPGeeks.Items;

namespace RPGeeks.Inventories
{
    public class InventorySlot : SlotUI, IDropHandler
    {
        [SerializeField] private Inventory inventory = null;
        [SerializeField] private TextMeshProUGUI itemQuantityText = null;

        public override HUDItem SlotItem
        {
            get { return ItemSlot.Item; }
            set { }
        }

        public ItemSlot ItemSlot => inventory.ItemContainer.GetSlotByIndex(SlotIndex);

        public override void OnDrop(PointerEventData eventData)
        {
            ItemDragHandler itemDragHandler = eventData.pointerDrag.GetComponent<ItemDragHandler>();

            if (itemDragHandler == null) { return; }

            if ((itemDragHandler.ItemSlotUI as InventorySlot) != null)
            {
                inventory.ItemContainer.Swap(itemDragHandler.ItemSlotUI.SlotIndex, SlotIndex);
            }
        }

        public override void UpdateSlotUI()
        {
            if (ItemSlot.Item == null)
            {
                EnableSlotUI(false);
                return;
            }

            EnableSlotUI(true);

            ItemImage.sprite = ItemSlot.Item.Icon;
            itemQuantityText.text = ItemSlot.Quantity > 1 ? ItemSlot.Quantity.ToString() : "";
        }

        protected override void EnableSlotUI(bool enable)
        {
            base.EnableSlotUI(enable);
            itemQuantityText.enabled = enable;
        }
    }
}