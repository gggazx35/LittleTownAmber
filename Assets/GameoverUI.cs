using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameoverEvent : IEvent
{
    private string m_text;
    public GameoverEvent(string text)
    {
        this.m_text = text;
    }
    public string text { get { return m_text; } }
}

public class GameoverUI : MonoBehaviour
{
    public Text gameoverText;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
        EventBus.get().Subscribe<GameoverEvent>( (e) => { 
            gameObject.SetActive(true);
            gameoverText.text = e.text;
            
        } );
    }

    void Respawn()
    {
    }
}
