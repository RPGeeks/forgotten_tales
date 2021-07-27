using RPGeeks.Inventories;
using System;

namespace RPGeeks.Items
{
    [Serializable]
    public class ItemContainer : SlotHolder
    {
        private ItemSlot[] itemSlots = new ItemSlot[0];

        public Action OnItemsUpdated = delegate { };

        public ItemContainer(int size) => itemSlots = new ItemSlot[size];

        public ItemSlot GetSlotByIndex(int index) => itemSlots[index];

        public ItemSlot AddItem(ItemSlot itemSlot)
        {
            for (int i = 0; i < itemSlots.Length; i++)
            {
                if (itemSlots[i].Item != null)
                {
                    if (itemSlots[i].Item == itemSlot.Item)
                    {
                        int slotRemainingSpace = itemSlots[i].Item.MaxStack - itemSlots[i].Quantity;

                        if (itemSlot.Quantity <= slotRemainingSpace)
                        {
                            itemSlots[i].Quantity += itemSlot.Quantity;

                            itemSlot.Quantity = 0;

                            OnItemsUpdated.Invoke();

                            return itemSlot;
                        }
                        else if (slotRemainingSpace > 0)
                        {
                            itemSlots[i].Quantity += slotRemainingSpace;

                            itemSlot.Quantity -= slotRemainingSpace;
                        }
                    }
                }
            }

            for (int i = 0; i < itemSlots.Length; i++)
            {
                if (itemSlots[i].Item == null)
                {
                    if (itemSlot.Quantity <= itemSlot.Item.MaxStack)
                    {
                        itemSlots[i] = itemSlot;

                        itemSlot.Quantity = 0;

                        OnItemsUpdated.Invoke();

                        return itemSlot;
                    }
                    else
                    {
                        itemSlots[i] = new ItemSlot(itemSlot.Item, itemSlot.Item.MaxStack);

                        itemSlot.Quantity -= itemSlot.Item.MaxStack;
                    }
                }
            }

            OnItemsUpdated.Invoke();

            return itemSlot;
        }

        public void RemoveItem(ItemSlot itemSlot)
        {
            for (int i = 0; i < itemSlots.Length; i++)
            {
                if (itemSlots[i].Item != null)
                {
                    if (itemSlots[i].Item == itemSlot.Item)
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

                                OnItemsUpdated.Invoke();

                                return;
                            }
                        }
                    }
                }
            }
        }

        public void RemoveAt(int slotIndex)
        {
            if (slotIndex < 0 || slotIndex > itemSlots.Length - 1) { return; }

            itemSlots[slotIndex] = new ItemSlot();

            OnItemsUpdated.Invoke();
        }

        public void Swap(int indexOne, int indexTwo)
        {
            ItemSlot firstSlot = itemSlots[indexOne];
            ItemSlot secondSlot = itemSlots[indexTwo];

            if (firstSlot == secondSlot) { return; }

            if (secondSlot.Item != null)
            {
                if (firstSlot.Item == secondSlot.Item)
                {
                    int secondSlotRemainingSpace = secondSlot.Item.MaxStack - secondSlot.Quantity;

                    if (firstSlot.Quantity <= secondSlotRemainingSpace)
                    {
                        itemSlots[indexTwo].Quantity += firstSlot.Quantity;

                        itemSlots[indexOne] = new ItemSlot();

                        OnItemsUpdated.Invoke();

                        return;
                    }
                }
            }

            itemSlots[indexOne] = secondSlot;
            itemSlots[indexTwo] = firstSlot;

            OnItemsUpdated.Invoke();
        }

        public bool HasItem(InventoryItem item)
        {
            foreach (ItemSlot itemSlot in itemSlots)
            {
                if (itemSlot.Item == null) { continue; }
                if (itemSlot.Item != item) { continue; }

                return true;
            }

            return false;
        }

        public int GetTotalQuantity(InventoryItem item)
        {
            int totalCount = 0;

            foreach (ItemSlot itemSlot in itemSlots)
            {
                if (itemSlot.Item == null) { continue; }
                if (itemSlot.Item != item) { continue; }

                totalCount += itemSlot.Quantity;
            }

            return totalCount;
        }
    }
}