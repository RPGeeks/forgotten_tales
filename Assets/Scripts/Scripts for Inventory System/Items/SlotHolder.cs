using RPGeeks.Inventories;

namespace RPGeeks.Items
{
    public interface SlotHolder
    {
        ItemSlot AddItem(ItemSlot itemSlot);
        void RemoveItem(ItemSlot itemSlot);
        void RemoveAt(int slotIndex);
        void Swap(int indexOne, int indexTwo);
        bool HasItem(InventoryItem item);
        int GetTotalQuantity(InventoryItem item);
    }
}