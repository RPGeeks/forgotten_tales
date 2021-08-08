using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Quest : MonoBehaviour
{
    [HideInInspector]
    public string name = "default quest";

    protected List<Reward> rewards = new List<Reward>();
    protected List<Goal> goals = new List<Goal>();

    [SerializeField] protected List<string> sentences = new List<string>(); //TODO add a function to add sentences make this private again

    protected bool completed;

    public List<Goal> Goals { get => goals; }

    public List<string> Sentences { get => sentences; }
    public int GoalsCount { get => goals.Count; }
    public void CheckGoals()
    {
        completed = goals.TrueForAll(goal => goal.Completed);
        if (completed)
            gameObject.GetComponent<NPCController>().QuestDone();
    }

    public void GiveRewards()
    {
        QuestManager.Instance.Remove(this);

        foreach (Goal goal in goals)
            goal.Finish();

        foreach (Reward reward in rewards)
            reward.GiveReward();
    }
}