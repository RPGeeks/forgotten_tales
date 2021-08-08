using RPGeeks.Inventories;
using TMPro;
using UnityEngine;
using Mirror;

namespace RPGeeks.Items
{
    public class ItemDestroyer : ItemDropHandler
    {
        public override void Activate(ItemSlot itemSlot, int slotIndex)
        {
            _slotIndex = slotIndex;
            panel.Text.SetText($"Are you sure you want to delete {itemSlot}?");

            gameObject.SetActive(true);
        }

        public override void Drop()
        {
            inventory.ItemContainer.RemoveAt(_slotIndex);
            gameObject.SetActive(false);
        }
    }
}