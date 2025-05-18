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

    public bool Run(ref Dialogue _dialogue)
    {
        dialogue = _dialogue;
        return next != null;
    }
}

[Serializable]
public class ChoiceEvent : IEvent
{
    [SerializeField] private EDecision choice;

    public EDecision Choice => choice;
}

public class DialogueDisplay : MonoBehaviour
{
    private Queue<DialoguePair> dialogueQueue = new Queue<DialoguePair>();
    private DialoguePair currentDialogue;
    [SerializeField] private DecisionBox[] decisionTexts;
    private Text text;
    private bool hasDecisions;
    IEnumerator coroutine;
    [SerializeField] private DialogueInfo info;
    [SerializeField] private Dialogue dialogue;
    [SerializeField] private GameObject choiceListener; 

    private static DialogueDisplay instance;

    private void Awake()
    {
        instance = this;
        info = new DialogueInfo();

        hasDecisions = false;
        text = GetComponent<Text>();
        foreach (var decision in decisionTexts)
        {
            decision.SetDisplay(this);
        }
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

        choiceListener = _gameObject;

        if (count == 0) { NextPage(); Next(); }

        return;
    }

    public void Next()
    {
        if (hasDecisions) { return; }

        if (coroutine.MoveNext())
        {
            hasDecisions = currentDialogue.dialogue.BindDicisions(decisionTexts);
            StartCoroutine(FormatReader.TypeText(text, ((Sentence)coroutine.Current).text, info));
        } else
            NextPage();
    }

    public void EndDecision(Dicision _decision)
    {
        foreach (var item in decisionTexts)
        {
            item.gameObject.SetActive(false);
        }
        hasDecisions = false;
        if (_decision.then != null)
        {
            currentDialogue.dialogue = _decision.then;
            coroutine = currentDialogue.dialogue.Continue();
        }
        EventBus.get().Publish(choiceListener, _decision.choiceEvent);
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
