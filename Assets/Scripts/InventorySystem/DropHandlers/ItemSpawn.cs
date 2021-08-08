using RPGeeks.Inventories;

namespace RPGeeks.Items
{
    public class ItemSpawn : ItemDropHandler
    {
        public override void Activate(ItemSlot itemSlot, int slotIndex)
        {
            _slotIndex = slotIndex;
            panel.Text.SetText($"Are you sure you want to drop {itemSlot}?");

            gameObject.SetActive(true);
        }

        public override void Drop()
        {
            ItemSlot itemSlot = inventory.ItemContainer.GetSlotByIndex(_slotIndex);
            InventoryLogic.DropSlot(this, itemSlot);
            inventory.ItemContainer.RemoveAt(_slotIndex);

            gameObject.SetActive(false);
        }
    }
}