using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestNumberUI : QuestUI
{

    // Update is called once per frame
    void FixedUpdate()
    {
        QuestNumber();
    }

    private void QuestNumber()
    {
        if (!(Quest is QuestNumber)) return;
        var q = (QuestNumber)Quest;
       // descriptionText.text = $"{q.Description}: {q.CurrentAmount}/{q.RequiredAmount}";
    }
}
