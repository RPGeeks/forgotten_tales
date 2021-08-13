using UnityEngine;
using UnityEngine.EventSystems;
using RPGeeks.Items;


namespace RPGeeks.Inventories
{
    public class InventoryItemDragHandler : ItemDragHandler
    {
        [SerializeField] private ItemDropHandler dropHandler = null;
        public ItemDropHandler DropHandler { get => dropHandler; set => dropHandler = value; }

        public override void OnPointerUp(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                base.OnPointerUp(eventData);
                if (eventData.hovered.Count == 0)
                {
                    InventorySlot thisSlot = ItemSlotUI as InventorySlot;
                    dropHandler.Activate(thisSlot.ItemSlot, thisSlot.SlotIndex);
                }
            }
        }
    }
}