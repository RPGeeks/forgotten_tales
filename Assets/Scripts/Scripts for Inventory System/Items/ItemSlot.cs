using RPGeeks.Inventories;
using System;

namespace RPGeeks.Items
{
    [Serializable]
    public class ItemSlot
    {
        public InventoryItem Item { get; set; }
        public int Quantity { get; set; }

        public ItemSlot()
        {
            Item = null;
            Quantity = 0;
        }
        public ItemSlot(InventoryItem item, int quantity)
        {
            Item = item;
            Quantity = quantity;
        }



        public static bool operator ==(ItemSlot a, ItemSlot b) { return a.Equals(b); }
        public static bool operator !=(ItemSlot a, ItemSlot b) { return !a.Equals(b); }
    }
}

