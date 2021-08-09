using Mirror;
using RPGeeks.ItemHandlers;
using RPGeeks.Items;
using UnityEngine;

namespace RPGeeks.Inventories
{
    public class InventoryLogic
    {
        public static void PickupItem(IItemsHandler handler, IPickup pickup)
        {
            ItemSlot remaining = handler.Inventory.ItemContainer.AddItem(pickup.Item);

            if (remaining != null && (remaining.Quantity == 0 || remaining.IsEmpty))
            {
                Object.Destroy(pickup.GameObject);
                // TODO maybe add specific sound
            }
        }

        public static void DropSlot(IItemsHandler handler, ItemSlot itemSlot)
        {
            GameObject player = NetworkClient.localPlayer.gameObject;
            
            GameObject prefab = itemSlot.Item.Prefab;
            GameObject itemDrop = Object.Instantiate(prefab, player.transform.position, Quaternion.identity);
            
            Pickup pickup = itemDrop.AddComponent<Pickup>();
            pickup.Item = new ItemSlot(itemSlot.Item, itemSlot.Quantity);
        }
    }
}
