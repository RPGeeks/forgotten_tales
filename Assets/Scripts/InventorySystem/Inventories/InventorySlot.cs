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

        public override HUDItem SlotItem { get => ItemSlot.Item; set{} }

        public ItemSlot ItemSlot { get => inventory.ItemContainer.GetSlotByIndex(SlotIndex); }

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

        private void OnEnable()
        {
            inventory.onInventoryUpdated += UpdateSlotUI;
        }

        private void OnDisable()
        {
            inventory.onInventoryUpdated -= UpdateSlotUI;
        }

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