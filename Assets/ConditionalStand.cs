using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConditionalStand : Stand
{
    [SerializeField] private Sprite sprite;
    [SerializeField] ItemTag initalItem;
    // Start is called before the first frame update

    // Update is called once per frame
    void Start()
    {
        m_inventory = GetComponent<Inventory>();
        EventBus.get().Subscribe<StandPutItemEvent>(gameObject, Put);
        m_inventory.AddItem(initalItem.Create());
        //m_inventory = new Inventroy(1);
    }

    public override void Put(StandPutItemEvent e)
    {
        //Debug.Log(_mob.inventroy.GetItemAt(_mob.selectedSlot).type);
        if (e.item.type == ItemType.GoldenCoin)
        {
            GetComponent<SpriteRenderer>().sprite = sprite;
            base.Put(e);
        }
    }
}
