using RPGeeks.Inventories;
using RPGeeks.Items;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace RPGeeks.HUDS
{
    public class HUDSlot : SlotUI, IDropHandler
    {
        [SerializeField] private Inventory inventory = null;
        [SerializeField] private TextMeshProUGUI itemQuantityText = null;

        private HUDItem slotItem = null;

        public override HUDItem SlotItem
        {
            get { return slotItem; }
            set { slotItem = value; UpdateSlotUI(); }
        }

        protected override void Start()
        {
            // TODO load inventory from NetworkClient.localPlayer;
            inventory = inventory != null
                ? inventory
                : Resources.Load<Inventory>("Prefabs/Data/Inventory");

            itemQuantityText = itemQuantityText != null
                ? itemQuantityText
                : transform.Find("Item").Find("QuantityText").GetComponent<TextMeshProUGUI>();

            base.Start();
        }

        public bool AddItem(HUDItem item)
        {
            if (SlotItem != null) 
            { 
                return false;
            }

            SlotItem = item;
            return true;
        }

        public void UseSlot(int index)
        {
            if (index != SlotIndex) 
            {
                return; 
            }

            //TODO Use item
        }

        public override void OnDrop(PointerEventData eventData)
        {
            ItemDragHandler itemDragHandler = eventData.pointerDrag.GetComponent<ItemDragHandler>();
            if (itemDragHandler == null) { return; }

            InventorySlot inventorySlot = itemDragHandler.ItemSlotUI as InventorySlot;
            if (inventorySlot != null)
            {
                SlotItem = inventorySlot.ItemSlot.Item;
                return;
            }

            HUDSlot hotbarSlot = itemDragHandler.ItemSlotUI as HUDSlot;
            if (hotbarSlot != null)
            {
                HUDItem oldItem = SlotItem;
                SlotItem = hotbarSlot.SlotItem;
                hotbarSlot.SlotItem = oldItem;
                return;
            }
        }

        public override void UpdateSlotUI()
        {
            if (SlotItem == null)
            {
                EnableSlotUI(false);
                return;
            }

            ItemImage.sprite = SlotItem.Icon;

            EnableSlotUI(true);
            SetItemQuantityUI();
        }

        private void SetItemQuantityUI()
        {
            if (SlotItem is InventoryItem inventoryItem)
            {
                if (inventory.ItemContainer.HasItem(inventoryItem))
                {
                    int quantityCount = inventory.ItemContainer.GetTotalQuantity(inventoryItem);
                    itemQuantityText.text = quantityCount > 1 ? quantityCount.ToString() : "";
                }
                else
                {
                    SlotItem = null;
                }
            }
            else
            {
                itemQuantityText.enabled = false;
            }
        }

        protected override void EnableSlotUI(bool enable)
        {
            base.EnableSlotUI(enable);
            itemQuantityText.enabled = enable;
        }
    }
}