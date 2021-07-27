using UnityEngine.EventSystems;
using RPGeeks.Items;


namespace RPGeeks.HUDS
{
    public class HUDItemDragHandler : ItemDragHandler
    {
        public override void OnPointerUp(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                base.OnPointerUp(eventData);

                if (eventData.hovered.Count == 0)
                {
                    (SlotUI as HUDSlot).SlotItem = null;
                }
            }
        }
    }
}