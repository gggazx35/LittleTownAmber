using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldTeleporter : MonoBehaviour
{
    public Stand stand;
   
    void Update()
    {
        Item item = stand.GetItem();
        if (item == null || !(item.GetItemTag() is BookItemTag))
            return;
        BookItemTag bookItemTag = item.GetItemTag() as BookItemTag;
        GameManager.instance.LoadScene(bookItemTag.connectedChapterName);
    }
}
