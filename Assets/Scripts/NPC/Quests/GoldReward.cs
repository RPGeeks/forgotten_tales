using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldReward : Reward
{
    public GoldReward(int ammount)
    {
        this.ammount = ammount;
    }

    public override void GiveReward()
    {
        // player.add gold 
        Gold gold = ScriptableObject.CreateInstance<Gold>();
        gold.UpdateValue(ammount);
        InventoryManager.instance.Add(gold);
        Debug.Log("give gold");
    }
}