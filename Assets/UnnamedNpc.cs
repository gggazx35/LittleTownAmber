using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnnamedNpc : Mob
{
    [SerializeField] private InteractionFetcher talkingBox;
    [SerializeField] Dialogue dialogueFirstQuestAchieved;
    [SerializeField] Dialogue dialogueFirstMeet;
    [SerializeField] QuestData findingWhitePaperQuest;
    // Start is called before the first frame update
    void Start()
    {
        m_inventroy = GetComponent<Inventory>();
        talkingBox.Subscribe(EventSubscribe);
    }

    void EventSubscribe(GameObject _g)
    {
        EventBus.get().Subscribe(_g, (EnterConversationEvent e) => {
            DialogueDisplay.get().StartDialogue(new DialoguePair(dialogueFirstMeet).Then(null).Then(
                (_choice) =>
                {
                    if(_choice == 1)
                    {
                        GameManager.get().AddQuest(findingWhitePaperQuest, (q) => { return $"0/{(q as QuestNumber).RequiredAmount}"; });

                        EventBus.get().Subscribe<QuestAchieveEvent>((q) =>
                        {
                            if(q.IsQuestAchieved(findingWhitePaperQuest))
                                DialogueDisplay.get().StartDialogue(new DialoguePair(dialogueFirstQuestAchieved));
                        });
                    }
                }
                )
                );
        });
    }


}
