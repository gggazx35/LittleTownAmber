using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Material Data", menuName = "Scriptable Object/Mat Data", order = int.MaxValue)]
public class MaterialData : ScriptableObject
{
    [SerializeField]
    private AudioClip m_sound;
    public AudioClip sound { get { return m_sound; } }
}
