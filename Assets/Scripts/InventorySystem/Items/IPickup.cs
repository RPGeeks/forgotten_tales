using UnityEngine;

namespace RPGeeks.Items
{
    public interface IPickup
    {
        public ItemSlot Item { get; }
        public GameObject GameObject { get; }
    }
}
