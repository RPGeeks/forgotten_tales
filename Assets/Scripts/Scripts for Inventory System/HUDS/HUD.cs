using UnityEngine;
using Mirror;
namespace RPGeeks.HUDS
{
    public class HUD : NetworkBehaviour
    {
        [SerializeField] private HUDSlot[] hotbarSlots = new HUDSlot[10];

        public void Add(HUDItem itemToAdd)
        {
            foreach (HUDSlot hotbarSlot in hotbarSlots)
            {
                if (hotbarSlot.AddItem(itemToAdd)) { return; }
            }
        }
    }
}