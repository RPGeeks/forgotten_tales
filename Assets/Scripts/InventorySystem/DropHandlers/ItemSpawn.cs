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
            GameObject prefab = inventory.ItemContainer.GetSlotByIndex(_slotIndex).Item.Prefab;
            GameObject player = NetworkClient.localPlayer.gameObject;
            Instantiate(prefab, player.transform.position, Quaternion.identity);

            inventory.ItemContainer.RemoveAt(_slotIndex);

            gameObject.SetActive(false);
        }
    }
}