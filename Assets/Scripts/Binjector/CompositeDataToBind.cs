using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public unsafe class CompositeDataToBind<T> where T : TextMeshProUGUI
{
    public T Data;
    public int* p;
    
    public CompositeDataToBind(ref T Data)
    {
        this.Data = Data;

    }
    public void To(ref int data)
    {

    }
    public void To(float data)
    {

    }
}
