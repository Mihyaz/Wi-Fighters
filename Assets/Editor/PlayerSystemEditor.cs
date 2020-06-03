/*
 * Written by Onur Mihyaz
 */
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using UnityEditorInternal;


[CustomEditor(typeof(PlayerSystem)), CanEditMultipleObjects]
public class PlayerSystemEditor : Editor
{
    SerializedProperty m_MonoProperty;
    private ReorderableList _list;

    void OnEnable()
    {
        // Fetch the objects from the MyScript script to display in the inspector
        m_MonoProperty = serializedObject.FindProperty("monoScript");
        _list = new ReorderableList(serializedObject, m_MonoProperty, true, true, true, true);

        _list.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) =>
        {
            var element = _list.serializedProperty.GetArrayElementAtIndex(index);
            rect.y += 2;
            EditorGUI.PropertyField(new Rect(rect.x, rect.y, 200, EditorGUIUtility.singleLineHeight),
                element.FindPropertyRelative("System"), GUIContent.none);
        };
        _list.drawHeaderCallback = (Rect rect) =>
        {
            EditorGUI.LabelField(rect, "Systems(Mihyaz)");
        };
    }
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        _list.DoLayoutList();
        serializedObject.ApplyModifiedProperties();
    }
}
