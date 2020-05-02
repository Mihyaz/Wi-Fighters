using TMPro;
using Zenject;

public class DataBinder : DataBinderBase
{
    public PlayerUI _playerUI;
    public Player _player;

    public override void Binder()
    {
        base.Binder();
        //VisualContainer.Bind<TextMeshProUGUI>().FromReference(ref _playerUI.Score).To(ref _player.myScore);
    }

    private void Awake()
    {
        Binder();
    }

    public void ChangeValue()
    {
    }
}
