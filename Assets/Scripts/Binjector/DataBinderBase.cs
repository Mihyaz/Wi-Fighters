using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataBinderBase : MonoBehaviour
{
    protected DataContainer VisualContainer
    {
        get; set;
    }

    public virtual void Binder()
    {
        VisualContainer = new DataContainer();
    }

    public delegate void MihyazSetter<in T>(float newValue);
    public delegate float MihyazGetter<out T>();

    public float MihyazTo(MihyazGetter<float> getter, MihyazSetter<float> setter, float endValue)
    {
        return (dynamic)getter;
    }
}
