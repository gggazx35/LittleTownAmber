using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;

public struct TypedID {
    public Type type;
    public int id;

    public TypedID(int _id, Type _type)
    {
        id = _id;
        type = _type;
    }

    public TypedID(string _str, Type _type)
    {
        id = _str.GetHashCode();
        type = _type;
    }
}

public interface IBlackboardMatchable<T>
{
    public bool IsMatch(T _other);
}

public class TypedIDComparser : IEqualityComparer<TypedID>
{

    public bool Equals(TypedID x, TypedID y)
    {
        return x.type == y.type && x.id == y.id;

    }

    public int GetHashCode(TypedID id)
    {
        return id.type.GetHashCode() ^ id.id.GetHashCode();
    }

}


public abstract class BlackboardProperty : IEvent
{
}

public abstract class BlackboardPropertyBasicTypeBase : BlackboardProperty
{
    protected string name;
    protected Animator linkedAnim;

    public void LinkAnimator(string _name, Animator _animator)
    {
        linkedAnim = _animator;
        name = _name;
    }
}

public class BlackboardIntProperty : BlackboardPropertyBasicTypeBase, IBlackboardMatchable<BlackboardIntProperty>, 
    IBlackboardMatchable<int>
{
    private int _value;

    public BlackboardIntProperty(int value)
    {
        _value=value;
        linkedAnim = null;
    }

    public int Get()
    {
        return _value;
    }

    public void Set(int value)
    {
        if(linkedAnim != null) linkedAnim.SetInteger(name, value);
        _value = value;
    }

    public bool IsMatch(BlackboardIntProperty _other)
    {
        if (_value == _other.Get()) return true;
        return false;
    }

    public bool IsMatch(int _other)
    {
        if (_value == _other) return true;
        return false;
    }
}

[Serializable]
public class BlackboardBoolProperty : BlackboardPropertyBasicTypeBase, IBlackboardMatchable<BlackboardBoolProperty>,
    IBlackboardMatchable<bool>
{
    [SerializeField] private bool _value;

    public BlackboardBoolProperty(bool value)
    {
        _value=value;
        linkedAnim = null;
    }

    public bool Get()
    {
        return _value;
    }

    public void Set(bool value)
    {
        if (linkedAnim != null) linkedAnim.SetBool(name, value);
        _value = value;
    }

    public bool IsMatch(BlackboardBoolProperty _other)
    {
        if(_value == _other.Get()) { return true; }
        return false;
    }

    public bool IsMatch(bool _other)
    {
        if (_value == _other) { return true; }
        return false;
    }
}

public class BlackboardFloatProperty : BlackboardPropertyBasicTypeBase
{
    private float _value;

    public BlackboardFloatProperty(float value)
    {
        _value = value;
        linkedAnim = null;
    }

    public float Get()
    {
        return _value;
    }

    public void Set(float value)
    {
        if (linkedAnim != null) linkedAnim.SetFloat(name, value);
        _value = value;
    }
}

public class BlackboardStringProperty : BlackboardProperty, IBlackboardMatchable<BlackboardStringProperty>,
    IBlackboardMatchable<string>
{
    private string _value;

    public BlackboardStringProperty(string value)
    {
        _value=value;
    }

    public string Get()
    {
        return _value;
    }

    public void Set(string value)
    {
        _value = value;
    }

    public bool IsMatch(BlackboardStringProperty _other)
    {
        if(_value == _other.Get()) return true;
        return false;
    }

    public bool IsMatch(string _other)
    {
        if (_value == _other) return true;
        return false;
    }
}

public class BlackboardGameObjectProperty : BlackboardProperty
{
    private GameObject _value;

    public BlackboardGameObjectProperty(GameObject value)
    {
        _value=value;
    }

    public GameObject Get()
    {
        return _value;
    }

    public void Set(GameObject value)
    {
        Debug.LogWarning($"ex: {_value} now: {value}");
        _value = value;
    }
}


public class Blackboard
{
    private Dictionary<TypedID, BlackboardProperty> properties;
    private Animator animator;

    public Blackboard()
    {
        properties = new Dictionary<TypedID, BlackboardProperty>();
    }

    public BlackboardProperty GetProperty(TypedID id)
    {
        Debug.Log($"Refed {id.id}, {id.type}");
        return properties[id];
    }

    public void PullAnimator()
    {
        if (animator != null) {

        }
    }

    public T GetProperty<T>(TypedID id) where T : BlackboardProperty
    {
        var result = GetProperty(id);
        return result as T;
    }

    public T GetProperty<T>(string name) where T : BlackboardProperty
    {
        var result = GetProperty(new TypedID(name, typeof(T)));
        return result as T;
    }

    private void AddProperty(TypedID id, BlackboardProperty property)
    {
        properties.Add(id, property);
    }

    public void AddProperty<T>(string name, T val) where T : BlackboardProperty
    {
        AddProperty(new TypedID(name, typeof(T)), val);
    }

    public void AddAnimatorLinkedProperty<T>(string name, T val, Animator animator) where T : BlackboardPropertyBasicTypeBase
    {
        val.LinkAnimator(name, animator);
        AddProperty(new TypedID(name, typeof(T)), val);
    }

    ////public T MakeProperty<T>(string name, T val) where T : BlackboardProperty
    ////{
    ////    var id = new TypedID(name, typeof(T));
    ////    if(!HasProperty(id)) AddProperty(id, val);
    ////    return GetProperty<T>(id);
    ////}

    public bool HasProperty(TypedID id)
    {
        return properties.ContainsKey(id);
    }


    //public bool GetProperty(string name, Type type, out BlackboardProperty property)
    //{
    //    var key = new TypedID(name, type);
    //    if (properties.ContainsKey(key))
    //    {
    //        property = properties[key];
    //        return true;
    //    }
    //    property = null;
    //    return false;
    //}


    //public T GetProperty<T>(string name) where T : BlackboardProperty
    //{
    //    Type type = typeof(T);
    //    return properties[new TypedID(name, type)] as T;
    //}
    //public void AddProperty<T>(string name, T value) where T : BlackboardProperty
    //{
    //    Type type = typeof(T);
    //    properties.Add(new TypedID(name, type), value);
    //}

    //public bool HasProperty<T>(string name) where T : BlackboardProperty
    //{
    //    Type type = typeof(T);
    //    return properties.ContainsKey(new TypedID(name, type));
    //}
}
