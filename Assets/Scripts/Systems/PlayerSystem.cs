using System.Collections.Generic;
using UnityEngine;

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
