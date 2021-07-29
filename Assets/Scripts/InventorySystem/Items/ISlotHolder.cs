using RPGeeks.Inventories;

namespace RPGeeks.Items
{
    public interface ISlotHolder
    {
        ItemSlot AddItem(ItemSlot itemSlot);
        void AddStorage(int additionalSlots);
        void RemoveItem(ItemSlot itemSlot);
        void RemoveAt(int slotIndex);
        void Swap(int indexOne, int indexTwo);
        bool HasItem(InventoryItem item);
        int GetTotalQuantity(InventoryItem item);
    }
}