using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillGoal : Goal
{
    private string enemyName;
    private Quest quest;

    public KillGoal(Quest quest, string enemyName, int ammount)
    {
        this.quest = quest;

        this.enemyName = enemyName;
        this.text = "Kill " + ammount.ToString() + " " + enemyName + "s";

        this.ammount = ammount;
        this.currAmount = 0;

        this.completed = false;
    }

    public override void Finish()
    {

    }

    
    void EnemyDied(string enemy)
    {
        if (enemy.Contains(enemyName))
        {
            if (++currAmount >= ammount)
            {
                completed = true;
                quest.CheckGoals();
            }

            QuestManager.Instance.UpdateStatus();
        }
    }
}