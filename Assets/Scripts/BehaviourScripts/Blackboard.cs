using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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


public abstract class BlackboardProperty
{
}

public class BlackboardIntProperty : BlackboardProperty
{
    private int _value;

    public BlackboardIntProperty(int value)
    {
        _value=value;
    }

    public int Get()
    {
        return _value;
    }

    public void Set(int value)
    {
        _value = value;
    }
}

public class BlackboardBoolProperty : BlackboardProperty
{
    private bool _value;

    public BlackboardBoolProperty(bool value)
    {
        _value=value;
    }

    public bool Get()
    {
        return _value;
    }

    public void Set(bool value)
    {
        _value = value;
    }
}

public class BlackboardFloatProperty : BlackboardProperty
{
    private float _value;

    public BlackboardFloatProperty(float value)
    {
        _value = value;
    }

    public float Get()
    {
        return _value;
    }

    public void Set(float value)
    {
        _value = value;
    }
}

public class BlackboardStringProperty : BlackboardProperty
{
    private string _value;

    public string Get()
    {
        return _value;
    }

    public void Set(string value)
    {
        _value = value;
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

    public Blackboard()
    {
        properties = new Dictionary<TypedID, BlackboardProperty> ();
    }

    public BlackboardProperty GetProperty(TypedID id)
    {
        Debug.Log($"Refed {id.id}, {id.type}");
        return properties[id];
    }

    public T GetProperty<T>(TypedID id) where T : BlackboardProperty
    {
        var result = GetProperty(id);
        return result as T;
    }

    public void AddProperty(TypedID id, BlackboardProperty property)
    {
        properties.Add(id, property);
    }

    public void AddProperty<T>(string name, T val) where T : BlackboardProperty
    {
        AddProperty(new TypedID(name, typeof(T)), val);
    }

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
