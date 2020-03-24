using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ClassSelection : MonoBehaviour
{
    private Player _player;
    private List<Button> _choiceButtons = new List<Button>();
    private RuntimeAnimatorController[] _runtimeAnimatorController;

    private void Awake()
    {
        _runtimeAnimatorController = new RuntimeAnimatorController[4];
        for (int i = 0; i < 4; i++)
        {
            int temp = i; //Delegates use the index as pointer. I had to realloc a new index in order to change i's address.
            _choiceButtons.Add(transform.GetChild(i).GetComponent<Button>());
            _choiceButtons[i].onClick.AddListener(delegate { CreatePlayer(temp); });
        }
        _player = gameObject.transform.root.GetComponent<Player>();
        _runtimeAnimatorController[0] = Resources.Load("Controllers/Rifle")   as RuntimeAnimatorController;
        _runtimeAnimatorController[1] = Resources.Load("Controllers/Shotgun") as RuntimeAnimatorController;
        _runtimeAnimatorController[2] = Resources.Load("Controllers/Handgun") as RuntimeAnimatorController;

    }

    public void CreatePlayer(int index)
    {
        _player.PickGunClass((GunClasses)index);
        _player.Component.Animator.runtimeAnimatorController = _runtimeAnimatorController[index];
        _player.enabled = true;
        gameObject.transform.parent.gameObject.SetActive(false);
    }
}

