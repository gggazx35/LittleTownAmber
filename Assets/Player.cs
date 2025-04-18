using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class Player : Mob
{
    bool OnLadder;

    // Start is called before the first frame update
    void Start()
    {
        m_inventroy = new Inventroy(16);
        OnLadder = false;
        GameManager.instance.inventoryUI.SetInventory(inventroy);
        movement = GetComponent<Movement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (movement == null) Debug.Log("iudkojdfsif");
        movement.Move(Input.GetAxisRaw("Horizontal"));
        if (Input.GetMouseButtonDown(0))
        {
            UseItem();
        }

        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit2D hit = RaycastAt(LayerMask.GetMask("Interactive"));
            if (hit)
            {
                Interactions(hit.transform.gameObject);
            }
        }

        if(Input.GetButtonDown("Jump"))
        {
            movement.Jump();
        }

        if (OnLadder)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                transform.Translate(0.0f, -1.4f * Time.deltaTime, 0.0f);
                if (movement.IsGrounded())
                {
                    SwitchLadder();
                }
            } else
            {
                if (Input.GetKey(KeyCode.Space))
                {
                    transform.Translate(0.0f, 1.4f * Time.deltaTime, 0.0f);
                    if (movement.IsGrounded())
                    {
                        SwitchLadder();
                    }
                }
            }
            
        }
    }

    private void Interactions(GameObject _object)
    {
        switch (_object.tag)
        {
            case "Chest":
                Debug.LogWarning($"interacting... {_object.GetInstanceID()}");
                EventBus.get().Publish<ChestOpenEvent>(_object, null);
                break;
            case "ItemObject":
                EventBus.get().Publish<ItemPickupEvent>(_object, new ItemPickupEvent(inventroy));
                break;
            case "Ladder":
                EventBus.get().Publish<ClimbLadderEvent>(_object, new ClimbLadderEvent(this));
                break;
        }
    }
    public void Ladder(Vector2 _position)
    {
        transform.position = _position;
        SwitchLadder();
    }

    private void SwitchLadder()
    {
        if (OnLadder)
        {
            OnLadder = false;
            gameObject.layer = LayerMask.NameToLayer("Player");
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None | RigidbodyConstraints2D.FreezeRotation;
        }
        else
        {
            OnLadder = true;
            gameObject.layer = LayerMask.NameToLayer("PlayerOnLadder");
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezePositionX;
        }
    }

    private void FixedUpdate()
    {
        Vector3 mousePosition = Input.mousePosition;

        movement.Target(Camera.main.ScreenToWorldPoint(mousePosition));
        movement.UpdateFlip();
    }

    
    
}
