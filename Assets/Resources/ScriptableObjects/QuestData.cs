using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Quest Data", menuName = "Scriptable Object/Quest Data", order = int.MaxValue)]
public class QuestData : ScriptableObject
{
    [SerializeField] private string description;
    [SerializeField] private GameObject ui;
    public string Description => description;
    public void OnVisualize()
    {
        if (ui != null)
        {
            Instantiate(ui, Vector3.zero, Quaternion.identity, GameManager.get().questWindow.transform);
            var qui = ui.GetComponent<QuestUI>();
            qui.Quest = this;
        }
    }
}
