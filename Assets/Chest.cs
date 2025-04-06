using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class Chest : Interactive
{
    [SerializeField] private DetectPlayer playerDetector;
    [SerializeField] private List<ItemConfig> items;
    // Start is called before the first frame update
    /*private void OnMouseDown()
    {
        if(playerDetector.IsPlayerDetected())
        {
            foreach (var item in items)
            {
                SpawnItemObject(item.Create());
            }
            items.Clear();
        }
    }*/
    public GameObject SpawnItemObject(Item _item)
    {
        GameObject go = Instantiate(GameManager.instance.itemObjectPrefeb,
            transform.position, transform.rotation);
        go.GetComponent<ItemObject>().SetItem(_item);
        return go;
    }

    public override void Interact(Player _player)
    {
        //if (playerDetector.IsPlayerDetected())
        //{
        foreach (var item in items)
        {
            SpawnItemObject(item.Create());
        }
        items.Clear();
        //}
        gameObject.layer = LayerMask.NameToLayer("Default");
    }

}


