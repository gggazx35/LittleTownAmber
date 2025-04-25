using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHasInventory
{
    Inventroy inventroy { get; }
    int selectedSlot { get; }
}

public interface IMobStat
{
    float maxHealth { get; }
    float health { get; set; }
    float strength { get; }
}

public interface IHumanMob : IHasInventory
{
    Hand hand { get; }
    void HoldItemAt(int i);
}

public class MobDeathEvent : IEvent
{
    private Mob m_death;
    private Mob m_cause; // opt
    private DamageReason m_reason;

    public Mob death { get => m_death; }
    public Mob cause { get => m_cause; }
    public DamageReason reason { get => m_reason; }

    public MobDeathEvent(Mob death, Mob cause, DamageReason reason)
    {
        m_death = death;
        m_cause = cause;
        m_reason = reason;
    }
}

public enum DamageReason
{
    None = 0,
    Fall,
    FallingStone,
    Explosion,
    Stab
}

public interface IDamageable
{
    public void TakeDamage(Mob _cause, float _amount);
    public void TakeDamage(DamageReason _reason, float _amount);
}

public class Mob : MonoBehaviour, IHumanMob
{
    [SerializeField] protected float m_defaultReach = 3.0f;
    protected Inventroy m_inventroy;
    protected int m_selectedSlot;

    [SerializeField] protected float m_strength = 1.0f;


    [SerializeField] protected LayerMask m_enemyMask;
    
    [SerializeField] protected Hand m_hand;
    [SerializeField] private Mob m_recentDamageCause;
    // properties
    public Inventroy inventroy { get => m_inventroy; }
    public Hand hand { get => m_hand; }
    public int selectedSlot { get => m_selectedSlot; }
    //public float maxHealth { get => m_maxHealth; }
    //public float health 
    //{ 
    //    get => m_health; 
    //    set
    //    {
    //        m_health = Mathf.Clamp(value, 0.0f, maxHealth);
    //    } 
    //}
    public float strength { get => m_strength; }


    [SerializeField] protected Movement movement;

    // IHumanMob
    public void HoldItemAt(int i)
    {
        hand.GrabItem(inventroy.GetItemAt(i));
        m_selectedSlot = i;
    }

    // IDamageable
    //public void TakeDamage(Mob _cause, float _amount)
    //{
    //    m_health = Mathf.Clamp(health - _cause.CalcuateDamage(_amount), 0.0f, maxHealth);
    //    m_recentDamageCause = _cause;
    //    if (m_health <= 0.0f)
    //    {
    //        EventBus.get().Publish(new MobDeathEvent(this, m_recentDamageCause, DamageReason.None));
    //    }
    //}

    //public void TakeDamage(DamageReason _reason, float _amount)
    //{
    //    m_health = Mathf.Clamp(health - _amount, 0.0f, maxHealth);
    //    if (m_health <= 0.0f) {
    //        EventBus.get().Publish(new MobDeathEvent(this, m_recentDamageCause, _reason));
    //    }
    //    m_recentDamageCause = null;
    //}

    public int GetEnemyMask()
    {
        return m_enemyMask; 
    }

    public float CalcuateDamage(float _damage)
    {
        return strength + _damage;
    }

    

    public Mob InRange()
    {
        //Debug.Log("FSAIJJOFJNHIOAUGBYIUS");
        if (hand.handedItem == null) return null;
        if (hand.handedItem is IDetectRange)
        {
            return (hand.handedItem as IDetectRange).InRange(this);
        }
        return null;
    }

    public Item GetHoldingItem()
    {
        return inventroy.GetItemAt(selectedSlot);
    }

    public void UseItem()
    {
        hand.Use(this);
    }

    public RaycastHit2D RaycastAt(float distance, int layer)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, movement.GetDirection(), m_defaultReach * distance, layer);
        Debug.DrawRay(transform.position, movement.GetDirection(), Color.green, m_defaultReach * distance);
        return hit;
    }
    public RaycastHit2D RaycastAt(int _layer)
    {
        return RaycastAt(1.0f, _layer);
    }

    public void MoveHoldingItem(Inventroy _targetInventory)
    {
        if(!_targetInventory.IsFull()) _targetInventory.AddItem(DropHoldingItem());
    }

    public Item DropHoldingItem()
    {
        if (inventroy.GetItemAt(selectedSlot) != null)
        {
            Item item = hand.handedItem.Unhand(this);
            inventroy.RemoveItemAt(selectedSlot);
            return item;
        }

        return null;
    }

    public void ThrowHoldingItemAsObject()
    {
        DropHoldingItem()?.SpawnItemObject(transform);
    }

    
}
