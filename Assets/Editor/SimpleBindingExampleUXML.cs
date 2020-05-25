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

    GameManager m_GM;
    public void OnEnable()
    {
        m_GM = GameObject.FindObjectOfType<GameManager>();
        if (m_GM == null)
            return;

        var inspector = new InspectorElement(m_GM);
        rootVisualElement.Add(inspector);
    }
}
