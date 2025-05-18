using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnnamedNpc : MonoBehaviour
{
    [SerializeField] private InteractionFetcher talkingBox;
    [SerializeField] Dialogue dialogueFirstQuestAchieved;
    [SerializeField] Dialogue dialogueFirstMeet;
    [SerializeField] QuestData findingWhitePaperQuest;

    // Start is called before the first frame update
    void Start()
    {
        //m_inventroy = GetComponent<Inventory>();
        //talkingBox.Subscribe(EventSubscribe);
        //if (!GameManager.get().player.GetComponent<QuestListener>().HasQuest(findingWhitePaperQuest))
        //    DialogueDisplay.get().StartDialogue(gameObject, new DialoguePair(dialogueFirstMeet));
        DialogueDisplay.get().StartDialogue(gameObject, new DialoguePair(dialogueFirstMeet));
        EventBus.get().Subscribe(gameObject, (ChoiceEvent e) =>
        {
            if(e.Choice == EDecision.FirstMeet_UnnamedNpc_Yes)
            {
                GameManager.get().player.GetComponent<QuestListener>().AddQuest(findingWhitePaperQuest);
            }
        });
    }

    void EventSubscribe(GameObject _g)
    {
        EventBus.get().Subscribe(_g, (EnterConversationEvent e) => {
            if(!GameManager.get().player.GetComponent<QuestListener>().HasQuest(findingWhitePaperQuest))
                DialogueDisplay.get().StartDialogue(gameObject, new DialoguePair(dialogueFirstMeet));
        });

        EventBus.get().Subscribe(gameObject, (ChoiceEvent e) =>
        {
            if(e.Choice == 0)
            {
                GameManager.get().player.GetComponent<QuestListener>().AddQuest(findingWhitePaperQuest);
            }
        });
    }


}
