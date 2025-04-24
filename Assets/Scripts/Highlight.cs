using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Highlight : MonoBehaviour
{
    SpriteRenderer sp;
    public bool highlighted = false;
    // Start is called before the first frame update
    private void Start()
    {
        sp = GetComponent<SpriteRenderer>();
        /*EventBus.get().Subscribe(gameObject, (ObjectHighlight obj) => {
            highlighted = true;
            sp.color = Color.yellow;
        });

        EventBus.get().Subscribe(gameObject, (ObjectHighlightLeave obj) => {
            highlighted = false;
            sp.color = Color.white;
        });*/
    }



    /*private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            sp.color = Color.Lerp(Color.white, Color.red, 1.0f);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            sp.color = Color.white;
        }
    }*/
}
