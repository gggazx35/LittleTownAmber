using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestUI : MonoBehaviour
{
    [SerializeField] QuestData quest;
    [SerializeField] protected Text descriptionText;
    [SerializeField] private Func<QuestData, string> visualizeMethod;
    public Func<QuestData, string> VisualizeMethod { set { visualizeMethod = value; } get => visualizeMethod; }
    public QuestData Quest { get => quest; set { quest = value; } }

    private void Start()
    {
        descriptionText.text = quest.Description;
    }

    private void FixedUpdate()
    {
        if (visualizeMethod == null) return;
        descriptionText.text = visualizeMethod(quest);
    }
}
