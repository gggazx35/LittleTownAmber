using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestListener : MonoBehaviour
{
    List<QuestData> quests = new List<QuestData>();
    
    public void AddQuest(QuestData data)
    {
        if(!HasQuest(data))
            quests.Add(data);
    }
    public bool HasQuest(QuestData questData)
    {
        return quests.Contains(questData);
    }
}
