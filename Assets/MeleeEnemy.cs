using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : Mob
{

    // Start is called before the first frame update
    void Start()
    {
        inventroy = new Inventroy(1);
        movement = GetComponent<Movement>();

        ItemConfig item;
        item.type = ItemType.Dagger.ToString();
        item.configPath = "Iron";
        inventroy.AddItem(item.Create());
        SelectItemAt(0);
    }
}
