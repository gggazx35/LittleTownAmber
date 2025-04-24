using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightDetecter : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Highlight h = collision.GetComponent<Highlight>();
        if(h != null)
        {
            h.highlighted = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Highlight h = collision.GetComponent<Highlight>();
        if (h != null)
        {
            h.highlighted = false;
        }
    }
}
