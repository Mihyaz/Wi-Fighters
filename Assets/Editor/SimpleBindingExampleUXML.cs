using UnityEditor;
using UnityEngine;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

public class SimpleBindingExampleUXML : EditorWindow
{
    [MenuItem("Window/UIElementsExamples/Simple Binding Example UXML")]
    public static void ShowDefaultWindow()
    {
        var wnd = GetWindow<SimpleBindingExampleUXML>();
        wnd.titleContent = new GUIContent("Simple Binding UXML");
    }

    Player m_Player;
    public void OnEnable()
    {
        m_Player = GameObject.FindObjectOfType<Player>();
        if (m_Player == null)
            return;

        var inspector = new InspectorElement(m_Player);
        rootVisualElement.Add(inspector);
    }
}
