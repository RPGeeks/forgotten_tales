using RPGeeks.Inventories;
using TMPro;
using UnityEngine;
using Mirror;

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
            GameObject prefab = itemSlot.Item.Prefab;
            
            GameObject player = NetworkClient.localPlayer.gameObject;
            GameObject itemDrop = Instantiate(prefab, player.transform.position, Quaternion.identity);
            Pickup pickup = itemDrop.AddComponent<Pickup>();
            pickup.Item = new ItemSlot(itemSlot.Item, itemSlot.Quantity);

            inventory.ItemContainer.RemoveAt(_slotIndex);

            gameObject.SetActive(false);
        }
    }
}