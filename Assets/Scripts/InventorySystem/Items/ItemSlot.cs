using RPGeeks.Inventories;
using System;
using UnityEngine;

namespace RPGeeks.Items
{
    [Serializable]
    public class ItemSlot
    {
        [SerializeField] private InventoryItem item;
        [SerializeField] private int quantity;

        public InventoryItem Item { get => item; set => item = value; }
        public int Quantity { get => quantity; set => quantity = value; }

        public int RemainingSpace { get => Item.MaxStack - Quantity; }
        public bool IsEmpty { get => Item == null; }

        public ItemSlot()
        {
            Set(null, 0);
        }

        public ItemSlot(InventoryItem item, int quantity)
        {
            Set(item, quantity);
        }

        public void Set(InventoryItem item, int quantity)
        {
            Item = item;
            Quantity = quantity;
        }

        public bool MatchItem(ItemSlot other)
        {
            return !IsEmpty && Item == other.Item;
        }

        public bool Contains(InventoryItem item)
        {
            return !IsEmpty && Item == item;
        }

        public static bool operator ==(ItemSlot a, ItemSlot b)
        {
            return a.Item == b.Item && a.Quantity == b.Quantity;
        }
        public static bool operator !=(ItemSlot a, ItemSlot b)
        {
            return a.Item != b.Item || a.Quantity != b.Quantity;
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return $"{Quantity}x {Item}";
        }
    }
}

