using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestReceiveEvent : IEvent
{
    [SerializeField] private QuestData quest;
    public QuestData Quest => quest;

    public QuestReceiveEvent(QuestData _quest)
    {
        quest = _quest;
    }
}

public class QuestListener : MonoBehaviour
{
    List<QuestData> quests = new List<QuestData>();
    
    public void AddQuest(QuestData data)
    {
        if (!HasQuest(data))
        {
            quests.Add(data);
            EventBus.get().Publish(gameObject, new QuestReceiveEvent(data));
        }
    }
    public bool HasQuest(QuestData questData)
    {
        return quests.Contains(questData);
    }
}
