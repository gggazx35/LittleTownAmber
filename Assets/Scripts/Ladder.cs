using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbLadderEvent : IEvent
{
    public Player player { get; }
    public ClimbLadderEvent(Player _player)
    {
        player = _player;
    }
}

public class Ladder : MonoBehaviour
{
    [SerializeField] Transform playerTf;

    public void Start()
    {
        EventBus.get().Subscribe<ClimbLadderEvent>(gameObject, Climb);
    }

    public void Climb(ClimbLadderEvent e)
    {
        /*if (!(_mob is Player)) return;
        Player player = _mob as Player;*/
        e.player.Ladder(playerTf.position);
    }
}
