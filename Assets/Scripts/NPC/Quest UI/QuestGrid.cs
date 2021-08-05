using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestsGrid : MonoBehaviour
{
    [SerializeField] private QuestManager questManager;
    [SerializeField] private GameObject prefab;
    private List<QuestSlot> slots;
    public void Init()
    {
        questManager = QuestManager.instance;
        questManager.questsChangedCallback += AddQuests;

        for (int i = 0; i < questManager.capacity; ++i)
            Instantiate(prefab, transform);

        slots = new List<QuestSlot>(GetComponentsInChildren<QuestSlot>());
    }

    void AddQuests()
    {
        for (int i = 0; i < slots.Count; i++)
        {
            if (i < questManager.goalsNo)
            {
                Quest quest = questManager.quests[i];

                i--;
                foreach (Goal goal in quest.goals)
                    slots[++i].AddItem(goal);
            }
            else
                slots[i].RemoveItem();
        }
    }
}