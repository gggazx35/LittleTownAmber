using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Paper Item Data", menuName = "Scriptable Object/Paper Item Data", order = int.MaxValue)]
public class PaperItemTag : ItemTag
{
    [SerializeField] string color;
    public string Color { get { return color; } }
}