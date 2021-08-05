using RPGeeks.Items;
using UnityEngine;

namespace RPGeeks.Inventories
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "New Inventory", menuName = "Items/Inventory")]
    public class Inventory : ScriptableObject
    {
        [SerializeField] private ItemSlot testItemSlot = new ItemSlot();
        [SerializeField] private int inventorySize = 20;

        public ItemContainer ItemContainer { get; private set; } = new ItemContainer(20);
        public int Size { get => inventorySize; }

        public delegate void OnInventoryUpdated();
        public event OnInventoryUpdated onInventoryUpdated;

        private void OnValidate()
        {
            if (inventorySize != ItemContainer.Size)
            {
                ItemContainer = new ItemContainer(inventorySize);
            }
        }

        public void OnEnable()
        {
            ItemContainer.onItemsUpdated += () => onInventoryUpdated?.Invoke();
        }

        public void OnDisable()
        {
            ItemContainer.onItemsUpdated -= () => onInventoryUpdated?.Invoke();
        }

        [ContextMenu("Test Add")]
        public void TestAdd()
        {
            ItemContainer.AddItem(testItemSlot);
        }
    }
}