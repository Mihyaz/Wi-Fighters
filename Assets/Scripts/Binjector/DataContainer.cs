using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DataContainer
{
    public FromUIBinderGeneric<UIContracts> Bind<UIContracts>() where UIContracts : TextMeshProUGUI
    {
       return new FromUIBinderGeneric<UIContracts>();
    }
}
