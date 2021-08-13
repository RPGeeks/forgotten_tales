using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstQuest : Quest
{
    private int killAmmount = 2;
    private int rewardAmmount = 100;

    void Start()
    {
        completed = false;
        sentences[0] = "I need your help!";
        sentences[1] = "Those Goblins are agonizing me!";
        sentences[2] = "Will you get rid of them?";
        this.name = "Kill the Goblins";

        goals.Add(new KillGoal(this, "Goblin", killAmmount));
      
        rewards.Add(new GoldReward(rewardAmmount));
    }
    
    

}