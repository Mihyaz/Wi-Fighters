using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TestZone 
{
    public static T MyFunction<T>(this T t) where T : IState
    {
        t.ResetThis();
        return t;
    }
}
