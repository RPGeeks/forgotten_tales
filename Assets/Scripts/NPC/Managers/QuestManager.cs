using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    private static QuestManager instance;
    private void Awake()
    {
        instance = this;
    }

    public delegate void OnQuestsChanged();
    public event OnQuestsChanged questsChangedCallback;

    private List<Quest> quests = new List<Quest>();
    private int goalsNo = 0;
    [SerializeField] private int capacity = 30;

   
    public int GoalsNo { get => goalsNo; }
    public int Capacity { get => capacity; }
    public static QuestManager Instance { get => instance; }

    private void Start()
    {
        UpdateStatus();
    }

    public bool Add(Quest quest)
    {
        if (goalsNo + quest.GoalsCount >= capacity)
            return false;
        goalsNo += quest.GoalsCount;

        quests.Add(quest);
        UpdateStatus();

        return true;
    }
    public Quest QuestAt(int i) 
    {
        return quests[i]; 
    }
    public void Remove(Quest quest)
    {
        quests.Remove(quest);
        goalsNo -= quest.GoalsCount;
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
        questsChangedCallback?.Invoke();
    }
}