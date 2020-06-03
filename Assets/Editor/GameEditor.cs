using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using Image = UnityEngine.UIElements.Image;

public class GameEditor : EditorWindow
{
    private Label m_Label;
    private IntegerField m_IntField;
    private Image m_Logo;

    [MenuItem("Window/GameEditor")]
    public static void ShowWindow()
    {
        GetWindow<GameEditor>("Game Editor");
    }

    public void OnEnable()
    {
        var root = this.rootVisualElement;

        #region Label
        m_Label = new Label()
        {
            text = "Clients"
        };
        m_Label.style.fontSize = 20f;
        m_Label.style.color = new Color(1f, 0f, 0.25f);

        #endregion
        #region Client Field
        m_IntField = new IntegerField()
        {
            bindingPath = "ClientCount"
        };
        m_IntField.style.unityTextAlign = new StyleEnum<TextAnchor>(TextAnchor.MiddleCenter);
        m_IntField.style.fontSize = 20;
        #endregion
        #region Wi-Fighters Logo
        m_Logo = new Image()
        {
            image = Resources.Load("Images/Wi-Fighters") as Texture
        };
        m_Logo.style.alignSelf = new StyleEnum<Align>(Align.Center);
        m_Logo.style.width = 300f;
        m_Logo.style.height = 50f;
        m_Logo.style.top = 7;
        #endregion

        root.Add(m_Logo);
        root.Add(m_Label);
        root.Add(m_IntField);

        Bind();
    }

    private void Bind()
    {
        SerializedObject so = new SerializedObject(FindObjectOfType<GameManager>());
        m_IntField.Bind(so);
    }
}
