using UnityEngine;
using UnityEngine.UI;
public class ClassSelection : MonoBehaviour
{
    private Player _player;
    private Button[] _choiceButton;
    private RuntimeAnimatorController[] _runtimeAnimatorController;

    private void Awake()
    {
        _choiceButton = new Button[4];
        _runtimeAnimatorController = new RuntimeAnimatorController[4];
        for (int i = 0; i < 4; i++)
        {
            int temp = i; //Delegates use the index as pointer. I had to realloc a new index in order to change i's address.
            _choiceButton[i] = transform.GetChild(i).GetComponent<Button>();
            _choiceButton[i].onClick.AddListener(delegate { SetChoice(temp); });
        
        }
        _player = gameObject.transform.root.GetComponent<Player>();
        _runtimeAnimatorController[0] = Resources.Load("Controllers/Rifle") as RuntimeAnimatorController;
        _runtimeAnimatorController[1] = Resources.Load("Controllers/Shotgun") as RuntimeAnimatorController;
        _runtimeAnimatorController[2] = Resources.Load("Controllers/Handgun") as RuntimeAnimatorController;

    }

    public void SetChoice(int index)
    {
        _player.CharacterClass = (GunClasses)index;
        _player.Gun = _player.PickGunClass((GunClasses)index);
        _player.Animator.runtimeAnimatorController = _runtimeAnimatorController[index];
    }
}

