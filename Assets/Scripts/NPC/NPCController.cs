//using cakeslice;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Mirror;

public class NPCController : Interactible
{
    [SerializeField] private GameObject exclamationMark;

    // TODO remove this after demo
    [SerializeField] private GameObject dialogue;

    private List<Quest> quests;
    private Quest activeQuest;
    private bool questCompleted;
    private bool questDialogue;

    [SerializeField] new private string name;
    [SerializeField] private List<string> sentences;

    void Start()
    {
        quests = new List<Quest>(gameObject.GetComponents<Quest>());

        Transform canvas = transform.Find("NPC Canvas");
        exclamationMark = canvas.Find("Image").gameObject;
        dialogue = canvas.Find("Dialogue").gameObject;
        dialogue.SetActive(false);

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

    public override void Visit(InteractionHandler handler)
    {
        // TODO use dialogue manager to print something
        if (!questCompleted)
        {
            questCompleted = true;
            exclamationMark.SetActive(false);
            dialogue.SetActive(true);
        }
    }
}