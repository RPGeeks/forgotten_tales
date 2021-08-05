using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager instance;
    private void Awake()
    {
        instance = this;
    }

    public delegate void OnQuestsChanged();
    public event OnQuestsChanged questsChangedCallback;

    public List<Quest> quests = new List<Quest>();
    public int goalsNo = 0;
    public int capacity = 30;

    private void Start()
    {
        UpdateStatus();
    }

    public bool Add(Quest quest)
    {
        if (goalsNo + quest.goals.Count >= capacity)
            return false;
        goalsNo += quest.goals.Count;

        quests.Add(quest);
        UpdateStatus();

        return true;
    }

    public void Remove(Quest quest)
    {
        quests.Remove(quest);
        goalsNo -= quest.goals.Count;
        UpdateStatus();
    }

    public void Render(bool type)
    {
        if (type == false)
            return;

        UpdateStatus();
    }

    public void UpdateStatus()
    {
        questsChangedCallback.Invoke();
    }
}