using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class FormatReader
{

    public static IEnumerator TypeText(Text _source, string _input, DialogueInfo info)
    {
        StringBuilder stringBuilder = new StringBuilder();
        info.ended = false;
        for (int i = 0; i < _input.Length; i++)
        {
            stringBuilder.Append(_input[i]);
            _source.text = stringBuilder.ToString();
            
            yield return new WaitForSeconds(0.1f);
        }
        info.ended = true;
    }
}

[System.Serializable]
public class DialogueInfo
{
    public bool ended;
}

public class DialoguePair
{
    public Dialogue dialogue;
    public Action<int> action;
    public DialoguePair next;
    public DialoguePair(Dialogue _dialogue)
    {
        dialogue = _dialogue;
        action = null;

        next = null;
        //action = new Queue<Action<int>>();
        //action.Enqueue(_action);
    }

    public DialoguePair(Action<int> _action)
    {
        dialogue = null;
        action = _action;

        next = null;
        //action = new Queue<Action<int>>();
        //action.Enqueue(_action);
    }

    public DialoguePair Then(Action<int> _action)
    {
        next = new DialoguePair(_action);
        return this;
    }
    //public DialoguePair Then(DialoguePair _dialoguePair)
    //{
    //    next = _dialoguePair;
    //    return next;
    //}

    public DialoguePair Add(Action<int> _action)
    {
        action += _action;
        return this;
    }

    public bool Run(ref DialoguePair _dialoguep, int _i, Dialogue _dialogue)
    {
        _dialoguep = next;
        if(next.dialogue == null) next.dialogue = _dialogue;
        return next != null;
    }
}

public class ChoiceEvent : IEvent
{
    private int choice;
    public int Choice => choice;
    public ChoiceEvent(int _choice)
    {
        choice = _choice;
    }
}

public class DialogueDisplay : MonoBehaviour
{
    private Queue<DialoguePair> dialogueQueue = new Queue<DialoguePair>();
    private DialoguePair currentDialogue;
    [SerializeField] private DicisionBox[] dicisionTexts;
    private Text text;
    private bool hasDicisions;
    IEnumerator coroutine;
    [SerializeField] private DialogueInfo info;
    [SerializeField] private Dialogue dialogue;
    [SerializeField] private GameObject choiceListener; 

    private static DialogueDisplay instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        hasDicisions = false;
        text = GetComponent<Text>();
        foreach (var dicision in dicisionTexts)
        {
            dicision.SetDisplay(this);
        }
        
        //
    }

    private void Update()
    {
        if (info.ended)
        {
            Next();
        }
    }

    public void StartDialogue(GameObject _gameObject, DialoguePair _dialoguePair)
    {
        int count = dialogueQueue.Count;
        dialogueQueue.Enqueue(_dialoguePair);
        if (count == 0) { NextPage(); Next(); }
        //currentDialogue = _dialoguePair;
        /*
        foreach (var item in _dialogue.lines)
        {
            displayString = item.text;
            for(int i = 0; i < item.dicisions.Length; i++)
            {
                dicisionTexts[i].Bind(this, item.dicisions[i]);
            }
        }*/
        return;
    }

    public void Next()
    {
        if (hasDicisions) { return; }

        if (coroutine.MoveNext())
        {
            hasDicisions = currentDialogue.dialogue.BindDicisions(dicisionTexts);
            StartCoroutine(FormatReader.TypeText(text, ((Sentence)coroutine.Current).text, info));
        } else
            NextPage();
    }

    public void EndDicision(int _dicision, Dialogue _dialogueConfig)
    {
        foreach (var item in dicisionTexts)
        {
            item.gameObject.SetActive(false);
        }
        hasDicisions = false;
        if (currentDialogue.Run(ref currentDialogue, _dicision, _dialogueConfig)) 
        {

            coroutine = currentDialogue.dialogue.Continue();
        }
        EventBus.get().Publish(choiceListener, new ChoiceEvent(_dicision));
            //.Run(_dicision);
    }

    public void NextPage()
    {
        
        if (dialogueQueue.Count > 0)
        {
            currentDialogue = dialogueQueue.Dequeue();
            coroutine = currentDialogue.dialogue.Continue();
        }
    }

    public static DialogueDisplay get()
    {
        return instance;
    }
}
