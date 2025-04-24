using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class MobStep : IEvent
{
    private Mob m_mob;
    public Mob mob { get { return m_mob; } }

    public MobStep(Mob mob)
    {
        this.m_mob = mob;
    }
}

public class MobFall : MobStep
{
    private float m_fallingSpeed;
    public float fallingSpeed { get { return m_fallingSpeed; } }

    public MobFall(Mob _mob, float _fallingSpeed) : base(_mob) {
        m_fallingSpeed = _fallingSpeed;
    }
}

public class MobUseDaggerEvent : MobUseItemEvent
{
    private Mob other;
    public Mob Other { get { return other; } }

    public MobUseDaggerEvent(Item _item, Mob _mob, Mob _other) : base(_item, _mob)
    {
        other = _other;
    }
}

public class ObjectHighlight : IEvent
{
    public ObjectHighlight() { }
}
public class ObjectHighlightLeave : IEvent
{
    public ObjectHighlightLeave() { }
}

public class Player : Mob
{
    bool OnLadder;
    bool Attack;
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        m_inventroy = new Inventroy(16);
        OnLadder = false;
        Attack = false;
        GameManager.instance.inventoryUI.SetInventory(inventroy);
        movement = GetComponent<Movement>();
    }

    public void AttackAnim(float _speed)
    {
        if (animator != null)
        {
            animator.SetTrigger("Attack");
            animator.SetFloat("AttackSpeed", _speed);
            Attack = true;
        }
    }

    public void EndAttack()
    {
        Attack = false;
    }


    // Update is called once per frame
    void Update()
    {
        if (movement == null) Debug.Log("iudkojdfsif");
        if (Attack == true) return;
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
            animator.SetFloat("LadderSpeed", 0.0f);
            if (Input.GetKey(KeyCode.LeftShift))
            {
                animator.SetFloat("LadderSpeed", 1.0f);
                transform.Translate(0.0f, -1.4f * Time.deltaTime, 0.0f);
                if (movement.IsGrounded())
                {
                    SwitchLadder();
                }
            }
            else
            {
                if (Input.GetKey(KeyCode.Space))
                {
                    animator.SetFloat("LadderSpeed", 1.0f);
                    transform.Translate(0.0f, 1.4f * Time.deltaTime, 0.0f);
                    if (movement.IsGrounded())
                    {
                        SwitchLadder();
                    }
                }
            }

        }
    }

    /*public override void UseItem()
    {
        Item item = GetHoldingItem();
        switch(item.type)
        {
            case ItemType.Dagger:
                EventBus.get().Publish(gameObject, new MobUseDaggerEvent(item, this, ));
                animator.SetTrigger("Attack");
                break;
        }
    }*/

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
            default:
                Debug.Log("Nothing");
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
            animator.SetBool("Ladder", false);
            gameObject.layer = LayerMask.NameToLayer("Player");
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None | RigidbodyConstraints2D.FreezeRotation;
        }
        else
        {
            OnLadder = true;
            animator.SetBool("Ladder", true);
            gameObject.layer = LayerMask.NameToLayer("PlayerOnLadder");
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezePositionX;
        }
    }

    GameObject hihit;

    private void FixedUpdate()
    {
        Vector3 mousePosition = Input.mousePosition;

        movement.Target(Camera.main.ScreenToWorldPoint(mousePosition));
        movement.PlayerFlip();
        if (hihit == null) {
            RaycastHit2D hit = RaycastAt(LayerMask.GetMask("Interactive"));
            if (hit) { hihit = hit.transform.gameObject; EventBus.get().Publish(hihit, new ObjectHighlight()); }
            
        }

        if (hihit)
        {
            
            RaycastHit2D r = RaycastAt(LayerMask.GetMask("Interactive"));
            if(!r)
            {
                EventBus.get().Publish(hihit, new ObjectHighlightLeave());
                hihit = null;
            }
        }
    }

    
    
}
