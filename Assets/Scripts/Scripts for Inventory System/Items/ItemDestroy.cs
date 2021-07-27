using RPGeeks.Inventories;
using TMPro;
using UnityEngine;
using Mirror;

namespace RPGeeks.Items
{
    public class ItemDestroyer : NetworkBehaviour
    {
        [SerializeField] private Inventory inventory = null;
        [SerializeField] private TextMeshProUGUI areYouSureText = null;

        private int slotIndex = 0;

        private void OnDisable() => slotIndex = -1;

        public void Activate(ItemSlot itemSlot, int slotIndex)
        {
            this.slotIndex = slotIndex;

            areYouSureText.text = $"Are you sure you want to delete {itemSlot.Quantity}x {itemSlot.Item.Name}?";

            gameObject.SetActive(true);
        }

        public void Destroy()
        {
            inventory.ItemContainer.RemoveAt(slotIndex);

            gameObject.SetActive(false);
        }
    }
}