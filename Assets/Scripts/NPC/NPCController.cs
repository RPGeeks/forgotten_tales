//using cakeslice;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class NPCController : MonoBehaviour
{
    [SerializeField] private GameObject exclamationMark;
    [SerializeField] private Outline outline;
   
    [SerializeField] private bool questDialogue;

    private List<Quest> quests;
    private Quest activeQuest;
    private bool questCompleted;

    [SerializeField] new private string name;
    [SerializeField] private List<string> sentences;

    void Start()
    {
        quests = new List<Quest>(gameObject.GetComponents<Quest>());

        exclamationMark = transform.Find("Canvas").gameObject;
        if (quests.Count == 0)
            exclamationMark.SetActive(false);

        activeQuest = null;
        questCompleted = false;
        questDialogue = false;

        DialogueManager.onDialogueDone += DialogueDone;
        DialogueManager.onQuestAccepted += AcceptQuest;
    }

    public void Interact()
    {
        if (questCompleted)
        {
            GiveReward();
            return;
        }

        if (quests.Count <= 0)
            return;

        DialogueManager.instance.AddDialogue(sentences.ToArray(), name, questDialogue);
    }

    public void AcceptQuest(string name)
    {
        if (quests.Count <= 0 && name != gameObject.name)
            return;

        Quest quest = quests[0];

        if (!QuestManager.Instance.Add(quest))
            return;

        activeQuest = quest;
        questCompleted = false;
        exclamationMark.SetActive(false);

        quests.RemoveAt(0);
    }

    public void QuestDone()
    {
        questCompleted = true;
        // render ! over npc
        exclamationMark.SetActive(true);
    }

    public void DialogueDone(string name)
    {
        if (quests.Count > 0 && name == gameObject.name)
        {
            sentences = quests[0].Sentences;
            questDialogue = true;
        }
    }

    public void GiveReward()
    {
        activeQuest.GiveRewards();

        if (quests.Count == 0)
        {
            exclamationMark.SetActive(false);
            sentences = null;
        }

        //QuestManager.instance.Remove(activeQuest);
        questCompleted = false;
        activeQuest = null;
    }
}