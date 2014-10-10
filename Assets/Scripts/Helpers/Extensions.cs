using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class Extensions {

    public static I GetInterfaceComponent<I>(this MonoBehaviour mb) where I : class
    {
        return mb.GetComponent(typeof(I)) as I;
    }

    public static List<I> FindObjectsOfInterface<I>(this MonoBehaviour mb) where I : class
    {
        MonoBehaviour[] monoBehaviours = Object.FindObjectsOfType<MonoBehaviour>();
        List<I> list = new List<I>();

        foreach (MonoBehaviour behaviour in monoBehaviours)
        {
            I component = behaviour.GetComponent(typeof(I)) as I;

            if (component != null)
            {
                list.Add(component);
            }
        }

        return list;
    }

    public static T GetSafeComponent<T>(this GameObject obj) where T : Component
    {
        T component = obj.GetComponent<T>();

        if (component == null)
        {
            Debug.LogError("Expected to find component of type "
               + typeof(T) + " but found none", obj);
        }

        return component;
    }

    public static T GetSafeComponentInChildren<T>(this GameObject go) where T : Component
    {
        T component = go.GetComponentInChildren<T>();

        if (component == null)
        {
            Debug.LogError("Expected to find component of type "
               + typeof(T) + " but found none", go);
        }

        return component;
    }
}
