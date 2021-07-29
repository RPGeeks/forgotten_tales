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

        private int _slotIndex = 0;

        private void OnDisable() => _slotIndex = -1;

        public void Activate(ItemSlot itemSlot, int slotIndex)
        {
            _slotIndex = slotIndex;
            areYouSureText.text = $"Are you sure you want to delete {itemSlot}?";

            gameObject.SetActive(true);
        }

        public void Destroy()
        {
            inventory.ItemContainer.RemoveAt(_slotIndex);

            gameObject.SetActive(false);
        }
    }
}