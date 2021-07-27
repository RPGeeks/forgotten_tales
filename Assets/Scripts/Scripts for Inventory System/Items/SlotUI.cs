using RPGeeks.HUDS;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Mirror;

namespace RPGeeks.Items
{
    public abstract class SlotUI : NetworkBehaviour, IDropHandler
    {
        [SerializeField] protected Image ItemImage = null;

        public int SlotIndex { get; private set; }

        public abstract HUDItem SlotItem { get; set; }

        private void OnEnable() => UpdateSlotUI();

        protected virtual void Start()
        {
            SlotIndex = transform.GetSiblingIndex();
            UpdateSlotUI();
        }

        public abstract void OnDrop(PointerEventData eventData);

        public abstract void UpdateSlotUI();

        protected virtual void EnableSlotUI(bool enable) => ItemImage.enabled = enable;
    }

}
