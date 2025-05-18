using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestUI : MonoBehaviour
{
    [SerializeField] private QuestData quest;
    [SerializeField] protected Text descriptionText;
    public QuestData Quest { get => quest; set { quest = value; } }

    protected virtual void Visualize()
    {
        descriptionText.text = quest.Description;
    }
}
