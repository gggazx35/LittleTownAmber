using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class Player : Mob
{

    // Start is called before the first frame update
    void Start()
    {
        m_inventroy = new Inventroy(16);
        GameManager.instance.inventoryUI.SetInventory(inventroy);
        movement = GetComponent<Movement>();
    }

    // Update is called once per frame
    void Update()
    {
        movement.MoveByAxis(Input.GetAxisRaw("Horizontal"));
        if (Input.GetMouseButtonDown(0))
        {
            UseItem();
        }

        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit2D hit = RaycastAt(LayerMask.GetMask("Interactive"));
            if (hit)
            {
                hit.transform.gameObject.GetComponent<Interactive>().Interact(this);
            }
        }

        if(Input.GetButtonDown("Jump"))
        {
            movement.Jump();
        }
    }

    private void FixedUpdate()
    {
        Vector3 mousePosition = Input.mousePosition;

        movement.Target(Camera.main.ScreenToWorldPoint(mousePosition));
        movement.UpdateFlip();
    }

   
    
}
