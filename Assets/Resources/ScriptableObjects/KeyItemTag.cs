using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Key Item Data", menuName = "Scriptable Object/KeyItem Data", order = int.MaxValue)]
public class KeyItemTag : ItemTag
{
    private int id;

    public int Id { get { return id; } }

    public int GetId()
    {
        return id;
    }
}