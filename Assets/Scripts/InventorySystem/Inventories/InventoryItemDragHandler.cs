using UnityEngine;
using UnityEngine.EventSystems;
using RPGeeks.Items;


namespace RPGeeks.Inventories
{
    public class InventoryItemDragHandler : ItemDragHandler
    {
        [SerializeField] private ItemDestroyer itemDestroyer = null;

        protected override void Start()
        {
            base.Start();

            itemDestroyer = itemDestroyer != null 
                ? itemDestroyer 
                : FindObjectOfType<ItemDestroyer>();
        }

        public override void OnPointerUp(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                base.OnPointerUp(eventData);

                if (eventData.hovered.Count == 0)
                {
                    InventorySlot thisSlot = ItemSlotUI as InventorySlot;
                    //itemDestroyer.Activate(thisSlot.ItemSlot, thisSlot.SlotIndex);
                }
            }
        }
    }
}