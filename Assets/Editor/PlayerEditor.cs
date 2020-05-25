using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;

[CustomEditor(typeof(GameEditor)), CanEditMultipleObjects]
public class PlayerEditor : Editor
{

    public override VisualElement CreateInspectorGUI()
    {
        var visualTree = Resources.Load("player_inspector_uxml") as VisualTreeAsset;
        var uxmlVE = visualTree.CloneTree();
        uxmlVE.styleSheets.Add(AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Resources/player_inspector_styles.uss"));
        return uxmlVE;
    }
}
