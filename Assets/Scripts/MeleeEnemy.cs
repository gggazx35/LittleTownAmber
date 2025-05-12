using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : Mob
{
    [SerializeField] private ItemTag initalItem;
    // Start is called before the first frame update
    void Start()
    {
        m_inventroy = GetComponent<Inventory>();
        movement = GetComponent<Movement>();

        inventroy.AddItem(initalItem.Create());
        HoldItemAt(0);
    }

    private void FixedUpdate()
    {
        movement.PlayerFlip();
    }
}
