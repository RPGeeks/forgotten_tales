using RPGeeks.Inventories;
using TMPro;
using UnityEngine;
using Mirror;

namespace RPGeeks.Items
{
    public class ItemDropHandler : MonoBehaviour
    {
        [SerializeField] protected Inventory inventory = null;
        [SerializeField] protected YesNoPanelController panel = null;

        protected int _slotIndex = 0;

        private void Start()
        {
            // TODO load inventory from NetworkClient.localPlayer;
            inventory = inventory != null
                ? inventory
                : Resources.Load<Inventory>("Prefabs/Data/Inventory");
            panel = transform.Find("YesNoPanel").GetComponent<YesNoPanelController>();

            panel.YesButton.onClick.AddListener(Drop);
            panel.NoButton.onClick.AddListener(() => gameObject.SetActive(false));
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

        public virtual void Drop()
        {
            gameObject.SetActive(false);
        }
    }
}