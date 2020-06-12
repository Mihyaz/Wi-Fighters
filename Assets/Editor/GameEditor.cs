using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using Image = UnityEngine.UIElements.Image;
using Button = UnityEngine.UIElements.Button;
using UnityEditor.UI;

public class GameEditor : EditorWindow
{
    private Label m_Label;
    private Label m_KillFeedLabel;
    private IntegerField m_IntField;
    private Image m_Logo;
    private Button m_RefreshKillFeed;
    private TextField m_Killer;
    private TextField m_Killed;


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
        #region KillFeedLabel
        m_KillFeedLabel = new Label()
        {
            text = "Kill Feed"
        };
        m_KillFeedLabel.style.fontSize = 20f;
        m_KillFeedLabel.style.color = new Color(0.33f, 0.5f, 0.45f);

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
        #region KillFeed Section
        m_RefreshKillFeed = new Button();

        m_Killer = new TextField();
        m_Killed = new TextField();
        m_Killer.style.maxWidth = 70;
        m_Killed.style.maxWidth = 70;
        m_RefreshKillFeed.text = "Refresh Kill";
        m_RefreshKillFeed.clicked += RefreshKillFeed;
        #endregion
        root.Add(m_Logo);
        root.Add(m_Label);
        root.Add(m_IntField);

        root.Add(m_KillFeedLabel);
        root.Add(m_Killer);
        root.Add(m_Killed);
        root.Add(m_RefreshKillFeed);

        Bind();
    }

    private void RefreshKillFeed()
    {
        FindObjectOfType<KillFeedPanel>().RefreshKillFeed(m_Killer.text, m_Killed.text);
    }

    private void Bind()
    {
        SerializedObject so = new SerializedObject(FindObjectOfType<GameManager>());
        m_IntField.Bind(so);
    }
}
