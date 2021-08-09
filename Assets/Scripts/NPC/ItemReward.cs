using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPGeeks.Inventories;
using RPGeeks.Items;

public class ItemReward : Reward
{
    private ItemContainer ic;
    private ItemSlot itemRew;
    private InventoryItem it;
    
    public ItemReward(int ammount)
    {
        this.ammount = ammount;
    }

    public override void GiveReward()
    {
        //it.Icon = 
        itemRew = new ItemSlot(it, ammount);
        ic.AddItem(itemRew);
    }
}