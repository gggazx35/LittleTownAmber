using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Book Item Data", menuName = "Scriptable Object/BookItem Data", order = int.MaxValue)]
public class BookItemTag : ItemTag
{
    public string connectedChapterName;

}