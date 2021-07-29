using RPGeeks.HUDS;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Mirror;

namespace RPGeeks.Items
{
    [System.Serializable]
    [RequireComponent(typeof(Image))]
    public abstract class SlotUI : NetworkBehaviour, IDropHandler
    {
        [SerializeField] protected Sprite sprite;
        protected Image ItemImage = null;

        public int SlotIndex { get; private set; } = -1;
        public abstract HUDItem SlotItem { get; set; }

        private void OnEnable()
        {
            if (SlotIndex > 0)
            {
               UpdateSlotUI();
            }
        }

        private void Awake()
        {
            ItemImage = GetComponent<Image>();
            ItemImage.sprite = sprite;
        }

        protected virtual void Start()
        {
            Init();
        }

        public void Init()
        {
            SlotIndex = transform.GetSiblingIndex();
            gameObject.name += $"_{SlotIndex}";
            UpdateSlotUI();
        }

        public abstract void OnDrop(PointerEventData eventData);

        public abstract void UpdateSlotUI();

        protected virtual void EnableSlotUI(bool enable)
        {
            ItemImage.enabled = enable;
        }
    }

}
