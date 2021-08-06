using UnityEngine;
using Mirror;
using System.Collections.Generic;

namespace RPGeeks.HUDS
{
    public class HUD : NetworkBehaviour
    {
        [SerializeField] private List<HUDSlot> hotbarSlots;
        
        private void Awake()
        {
            hotbarSlots = new List<HUDSlot>();
        }

        private void Start()
        {
            foreach (GameObject child in transform)
            {
                if (child.name.Contains("Slot"))
                {
                    hotbarSlots.Add(child.GetComponent<HUDSlot>());
                }
            }
        }

        public void Add(HUDItem itemToAdd)
        {
            foreach (HUDSlot hotbarSlot in hotbarSlots)
            {
                if (hotbarSlot.AddItem(itemToAdd)) 
                {
                    return; 
                }
            }
        }
    }
}