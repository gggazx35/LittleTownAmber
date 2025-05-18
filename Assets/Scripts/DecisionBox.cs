using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public interface IDicisionBox
{
    public void Bind(Dicision _dicision, int _index);
}

public class DecisionBox : MonoBehaviour, IDicisionBox
{
    DialogueDisplay display;
    [SerializeField] private Text text;
    Dicision dicision;
    int index;


    public void Bind(Dicision _dicision, int _index)
    {
        dicision = _dicision;
        text.text = dicision.text;
        index = _index;

        gameObject.SetActive(true);
    }

    public void SetDisplay(DialogueDisplay _display)
    {
        display = _display;
    }

    public void Dicide()
    {        
        display.EndDecision(dicision);
    }
}
