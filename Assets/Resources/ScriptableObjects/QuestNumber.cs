using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Quest Number Data", menuName = "Scriptable Object/Quest Number Data", order = int.MaxValue)]
public class QuestNumber : QuestData
{
    [SerializeField] private int requiredAmount;
    public int RequiredAmount { get { return requiredAmount; } }

    public override bool Achieve()
    {
        return false;
    }

}
