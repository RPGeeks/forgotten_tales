using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Quest : MonoBehaviour
{
    [HideInInspector]
    public string name = "default quest";

    public List<Reward> rewards;
    public List<Goal> goals;

    public string[] sentences;

    public bool completed;

    public void CheckGoals()
    {
        completed = goals.TrueForAll(goal => goal.Completed);
        if (completed)
            gameObject.GetComponent<NPCController>().QuestDone();
    }

    public void GiveRewards()
    {
        QuestManager.instance.Remove(this);

        foreach (Goal goal in goals)
            goal.Finish();

        foreach (Reward reward in rewards)
            reward.GiveReward();
    }
}