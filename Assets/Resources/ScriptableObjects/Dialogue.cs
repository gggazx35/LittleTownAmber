using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dialogue Data", menuName = "Scriptable Object/Dialogue Data", order = int.MaxValue)]
public class Dialogue : ScriptableObject
{
    public Sentence[] lines;
    public Dicision[] dicisions;

    public IEnumerator Continue()
    {
        int line = 0;

        while (line < lines.Length)
        {
            yield return lines[line];
            line++;
        }
    }

    public bool BindDicisions(IDicisionBox[] _dicisionBoxes)
    {
        if (dicisions == null || dicisions.Length == 0) { return false; }
        for (int i = 0; i < dicisions.Length; i++)
        {
            _dicisionBoxes[i]?.Bind(dicisions[i], i);
        }
        return true;
    }
}
