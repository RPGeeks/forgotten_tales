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
        questManager = QuestManager.Instance;
        questManager.questsChangedCallback += AddQuests;

        for (int i = 0; i < questManager.Capacity; ++i)
            Instantiate(prefab, transform);

        slots = new List<QuestSlot>(GetComponentsInChildren<QuestSlot>());
    }

    void AddQuests()
    {
        for (int i = 0; i < slots.Count; i++)
        {
            if (i < questManager.GoalsNo)
            {
                Quest quest = questManager.QuestAt(i);

                i--;
                foreach (Goal goal in quest.Goals)
                    slots[++i].AddItem(goal);
            }
            else
                slots[i].RemoveItem();
        }
    }
}