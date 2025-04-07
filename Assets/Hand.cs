using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

interface IHoldItem
{
    HandedItem handedItem { get; }
    GameObject handedItemObject { get; }
}

public class Hand : MonoBehaviour, IHoldItem
{

    // members
    private HandedItem m_handedItem = null;
    private GameObject m_handedItemObject = null;

    // properties
    public HandedItem handedItem { get => m_handedItem; }
    public GameObject handedItemObject { get => m_handedItemObject; }


    private void Start()
    {
        //movement = owner.GetComponent<Movement>();
    }

    public void Use(Mob _mob, int _slot)
    {
        if(handedItem != null) handedItem.Use(_mob, _slot);
    }

    

    public void GrabItem(Item _item)
    {
        if (m_handedItemObject != null)
        {
            Destroy(handedItemObject);
        }
        m_handedItemObject = Instantiate(_item.GetPrefeb());
        m_handedItemObject.transform.SetParent(transform, false);
        
        m_handedItem = handedItemObject.GetComponent<HandedItem>();
        m_handedItem.SetItem(_item);
    }

}
