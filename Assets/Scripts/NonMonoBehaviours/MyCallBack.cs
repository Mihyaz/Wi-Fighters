using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MyCallBack
{
    public delegate bool TheCallback();
    public delegate void Action<T>();
    public static void OnCompleted<T>(this T t, Action action)
    {
        action.Invoke();
    }

    public static T OnUpdated<T>(this T t, Action action)
    {
        if (t == null)
        {
            return t;
        }
        return t;
    }
}
