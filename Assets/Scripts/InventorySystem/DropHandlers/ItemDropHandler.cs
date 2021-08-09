using RPGeeks.Inventories;
using RPGeeks.ItemHandlers;
using UnityEngine;

namespace RPGeeks.Items
{
    public class ItemDropHandler : MonoBehaviour, IItemsHandler
    {
        [SerializeField] protected Inventory inventory = null;
        [SerializeField] protected YesNoPanelController panel = null;

        protected int _slotIndex = 0;

        public Inventory Inventory { get => inventory; }

        public void Init(Inventory inventory)
        {
            // TODO load inventory from NetworkClient.localPlayer;
            this.inventory ??= inventory;
            panel = transform.Find("YesNoPanel").GetComponent<YesNoPanelController>();

            panel.YesButton.onClick.AddListener(Drop);
            panel.NoButton.onClick.AddListener(() => gameObject.SetActive(false));

            gameObject.SetActive(false);
        }

        protected void OnDisable()
        {
            print("called");
            _slotIndex = -1;
        }

        public virtual void Activate(ItemSlot itemSlot, int slotIndex)
        {
            _slotIndex = slotIndex;

            gameObject.SetActive(true);
        }

        // TODO synchronize this method
        public virtual void Drop()
        {
            gameObject.SetActive(false);
        }
    }
}