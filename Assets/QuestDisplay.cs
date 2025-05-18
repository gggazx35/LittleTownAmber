using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestDisplay : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        EventBus.get().Subscribe(GameManager.get().player.gameObject, (QuestReceiveEvent e) =>
        {
            e.Quest.OnVisualize();
        });
    }
}
