using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Hand : MonoBehaviour
{
    private HandedItem handedItem = null;
    private GameObject handedItemObject = null;
    //private Movement movement;
    [SerializeField] private GameObject owner;

    private void Start()
    {
        //movement = owner.GetComponent<Movement>();
    }

    public void Use(Mob _mob, int _slot)
    {
        if(handedItem != null) handedItem.Use(_mob, _slot);
    }

    public Mob InRange(Mob _mob, int _slot)
    {
        if(handedItem != null)
        {
            return handedItem.InRange(_mob, _slot);
        }

        return null;
    }

    public void GrabItem(Item _item)
    {
        if (handedItemObject != null)
        {
            Destroy(handedItemObject);
        }
        handedItemObject = Instantiate(_item.GetPrefeb());
        handedItemObject.transform.SetParent(transform, false);
        handedItem = handedItemObject.GetComponent<HandedItem>();
        handedItem.SetItem(_item);
    }

    /*public void PointOut(Vector2 _target)
    {
        //Vector2 target = new Vector2(Mathf.Abs(_target.x), _target.y);
        if (movement.isFacingRight)
        {
            transform.right = _target.normalized;
        } else
        {
            transform.right = -(_target.normalized);
        }

        *//*Vector2 newPos = target - transform.position;
        float rotZ = Mathf.Atan2(newPos.y, newPos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotZ);*//*
    }*/
}
