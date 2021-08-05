using RPGeeks.Inventories;
using System.Collections.Generic;

namespace RPGeeks.Items
{
    [System.Serializable]
    public class ItemContainer : ISlotHolder
    {
        private List<ItemSlot> itemSlots;
        private int _inventorySize = 0;

        public int Size { get => _inventorySize; }

        public delegate void OnItemsUpdated();
        public static event OnItemsUpdated onItemsUpdated;

        public ItemContainer(int size)
        {
            itemSlots = new List<ItemSlot>();
            for (int i = 0; i < size; i++)
            {
                itemSlots.Add(new ItemSlot());
            }

            _inventorySize = size;
        }

        public void AddStorage(int additionalSlots)
        {
            for (int i = 0; i < additionalSlots; i++)
            {
                itemSlots.Add(new ItemSlot());
            }

            _inventorySize += additionalSlots;
        }

        public ItemSlot GetSlotByIndex(int index)
        {
            return itemSlots[index];
        }

        public ItemSlot AddItem(ItemSlot itemSlot)
        {
            if (itemSlot.Quantity <= 0)
            {
                return itemSlot;
            }

            for (int i = 0; i < _inventorySize; i++)
            {
                if (itemSlots[i].MatchItem(itemSlot))
                {
                    int slotRemainingSpace = itemSlots[i].RemainingSpace;
                    int quantityToAdd = slotRemainingSpace >= itemSlot.Quantity
                        ? itemSlot.Quantity
                        : slotRemainingSpace;

                    itemSlots[i].Quantity += quantityToAdd;
                    itemSlot.Quantity -= quantityToAdd;

                    if (itemSlot.Quantity == 0)
                    {
                        onItemsUpdated?.Invoke();
                        return itemSlot;
                    }
                }
            }

            for (int i = 0; i < _inventorySize; i++)
            {
                if (itemSlots[i].IsEmpty)
                {
                    int quantityToAdd = itemSlot.Quantity <= itemSlot.Item.MaxStack
                        ? itemSlot.Quantity
                        : itemSlot.Item.MaxStack;

                    itemSlots[i].Set(itemSlot.Item, quantityToAdd);
                    itemSlot.Quantity -= quantityToAdd;

                    if (itemSlot.Quantity == 0)
                    {
                        break;
                    }
                }
            }

            onItemsUpdated?.Invoke();
            return itemSlot;
        }

        public void RemoveItem(ItemSlot itemSlot)
        {
            for (int i = 0; i < _inventorySize; i++)
            {
                ItemSlot slot = itemSlots[i];
                if (slot.MatchItem(itemSlot))
                {
                    if (itemSlots[i].Quantity < itemSlot.Quantity)
                    {
                        itemSlot.Quantity -= itemSlots[i].Quantity;
                        itemSlots[i] = new ItemSlot();
                    }
                    else
                    {
                        itemSlots[i].Quantity -= itemSlot.Quantity;

                        if (itemSlots[i].Quantity == 0)
                        {
                            itemSlots[i] = new ItemSlot();

                            onItemsUpdated?.Invoke();
                            return;
                        }
                    }
                }
            }
        }

        public void RemoveAt(int slotIndex)
        {
            if (slotIndex < 0 || slotIndex > _inventorySize - 1) 
            { 
                return; 
            }

            itemSlots[slotIndex].Set(item: null, quantity: 0);
            onItemsUpdated?.Invoke();
        }

        public void Swap(int indexOne, int indexTwo)
        {
            ItemSlot firstSlot = itemSlots[indexOne];
            ItemSlot secondSlot = itemSlots[indexTwo];

            if (firstSlot == secondSlot) 
            {
                return;
            }

            if (firstSlot.MatchItem(secondSlot))
            {
                int secondSlotRemainingSpace = secondSlot.Item.MaxStack - secondSlot.Quantity;
                if (secondSlotRemainingSpace > 0)
                {
                    int quantityToAdd = firstSlot.Quantity <= secondSlotRemainingSpace
                        ? firstSlot.Quantity
                        : secondSlotRemainingSpace;

                    itemSlots[indexTwo].Quantity += quantityToAdd;
                    itemSlots[indexOne].Quantity -= quantityToAdd;

                    if (itemSlots[indexOne].Quantity == 0)
                    {
                        itemSlots[indexOne].Item = null;
                    }

                    onItemsUpdated?.Invoke();
                    return;
                }
            }

            itemSlots[indexOne] = secondSlot;
            itemSlots[indexTwo] = firstSlot;

            onItemsUpdated?.Invoke();
        }

        public bool HasItem(InventoryItem item)
        {
            return !(itemSlots.Find(itemSlot => itemSlot.Contains(item)) is null);

            //foreach (ItemSlot itemSlot in itemSlots)
            //{
            //    if (itemSlot.Item == null) { continue; }
            //    if (itemSlot.Item != item) { continue; }

            //    return true;
            //}

            //return false;
        }

        public int GetTotalQuantity(InventoryItem item)
        {
            int totalCount = 0;

            foreach (ItemSlot itemSlot in itemSlots)
            {
                if (itemSlot.Contains(item)) 
                { 
                   totalCount += itemSlot.Quantity;
                }
            }

            return totalCount;
        }
    }
}