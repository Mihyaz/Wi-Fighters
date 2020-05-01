using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FromUIBinderGeneric<UIContracts> where UIContracts : TextMeshProUGUI
{
    public CompositeDataToBind<UIContracts> FromReference(ref UIContracts contract)
    {
        return new CompositeDataToBind<UIContracts>(ref contract);
    }
}
