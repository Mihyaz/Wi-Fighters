using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ClassSelection : MonoBehaviour
{
    private Player _player;
    private List<Button> _choiceButtons;
    private List<RuntimeAnimatorController> _animatorController;
    private void Awake()
    {
        _animatorController = new List<RuntimeAnimatorController>();

        _player = gameObject.transform.root.GetComponent<Player>();
        _animatorController.Add(Resources.Load("Controllers/Rifle")   as RuntimeAnimatorController);
        _animatorController.Add(Resources.Load("Controllers/Shotgun") as RuntimeAnimatorController);
        _animatorController.Add(Resources.Load("Controllers/Handgun") as RuntimeAnimatorController);
    }

    public void CreatePlayer(int index)
    {
        _player.PickGunClass((GunClasses)index);

        _player.Component.Animator.runtimeAnimatorController = _animatorController[index];

        gameObject.transform.parent.gameObject.SetActive(false);
    }

    [Obsolete]
    public void AddListenerToButtons()
    {
        _choiceButtons = new List<Button>();

        for (int i = 0; i < 4; i++)
        {
            int temp = i; //Delegates use the index as pointer. I had to realloc a new index in order to change i's address.
            _choiceButtons.Add(transform.GetChild(i).GetComponent<Button>());
            _choiceButtons[i].onClick.AddListener(delegate
            {
                CreatePlayer(temp);
            });
        }
    }
}

