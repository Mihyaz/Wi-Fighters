using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

#if UNITY_EDITOR
public class PlayerSystem : MonoBehaviour
{
    public List<ScriptStruct> monoScript = new List<ScriptStruct>();

    private void Awake()
    {
        for (int i = 0; i < monoScript.Count; i++)
        {
            gameObject.AddComponent(monoScript[i].Mono.GetClass());
        }
    }
}
#endif
