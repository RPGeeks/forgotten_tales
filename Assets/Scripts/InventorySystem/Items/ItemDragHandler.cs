using UnityEngine;
using UnityEngine.EventSystems;
using Mirror;

namespace RPGeeks.Items
{
    [RequireComponent(typeof(CanvasGroup))]
    public class ItemDragHandler : NetworkBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] protected SlotUI itemSlotUI = null;
        public delegate void OnMouseHover(HUDItem item);
        public static event OnMouseHover onMouseStartHoverItem;

        public delegate void OnMouseEnd();
        public static event OnMouseEnd onMouseEndHoverItem;

        private CanvasGroup canvasGroup = null;
        private Transform originalParent = null;
        private bool isHovering = false;

        public SlotUI ItemSlotUI { get => itemSlotUI; }

        private void Awake()
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }

        protected virtual void Start()
        {
            itemSlotUI = itemSlotUI != null 
                ? itemSlotUI 
                : transform.parent.GetComponent<SlotUI>();
        }

        private void OnDisable()
        {
            if (isHovering)
            {
                onMouseEndHoverItem?.Invoke();
                isHovering = false;
            }
        }

        public virtual void OnPointerDown(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                onMouseEndHoverItem?.Invoke();
                originalParent = transform.parent;
                transform.SetParent(transform.parent.parent);
                canvasGroup.blocksRaycasts = false;
            }
        }

        public virtual void OnDrag(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                transform.position = Input.mousePosition;
            }
        }

        public virtual void OnPointerUp(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                transform.SetParent(originalParent);
                transform.localPosition = Vector3.zero;
                canvasGroup.blocksRaycasts = true;
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            onMouseStartHoverItem?.Invoke(ItemSlotUI.SlotItem);
            isHovering = true;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            onMouseEndHoverItem?.Invoke();
            isHovering = false;
        }
    }
}