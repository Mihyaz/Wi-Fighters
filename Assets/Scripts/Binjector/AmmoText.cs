using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;

public class AmmoText : VisualElement
{
    public new class UxmlFactory : UxmlFactory<AmmoText> { }

    private string _ammo;
    public string Ammo { get; set; }
    public AmmoText()
    {
        _ammo = string.Empty;
    }
}