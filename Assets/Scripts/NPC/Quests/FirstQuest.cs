using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstQuest : Quest
{
    private int killAmmount = 1;
    private int rewardAmmount = 100;
    
    void Start()
    {
        completed = false;
        this.name = "Kill the Goblins";

        goals.Add(new KillGoal(this, "Goblins", killAmmount));
      
        rewards.Add(new GoldReward(rewardAmmount));
    }
}