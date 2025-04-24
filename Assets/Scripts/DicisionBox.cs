using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public interface IDicisionBox
{
    public void Bind(Dicision _dicision, int _index);
}

public class DicisionBox : MonoBehaviour, IDicisionBox
{
    DialogueDisplay display;
    Text text;
    Dicision dicision;
    int index;

    private void Start()
    {
        text = GetComponent<Text>();
    }

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
        
        display.EndDicision(index, new DialogueConfig(dicision.then));
    }
}
