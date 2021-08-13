using RPGeeks.Inventories;
using RPGeeks.ItemHandlers;
using UnityEngine;

namespace RPGeeks.Items
{
    [RequireComponent(typeof(Rigidbody))]
    public class Pickup : Interactible, IPickup
    {
        [SerializeField] private ItemSlot item;
        public ItemSlot Item { get => item; set => item = value; }

        public GameObject GameObject { get => gameObject; }

        public override void Visit(InteractionHandler handler)
    {
            if (handler as IItemsHandler != null)
            {
                InventoryLogic.PickupItem(handler as IItemsHandler, this);
            }
        }
    }
}
