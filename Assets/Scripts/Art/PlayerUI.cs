using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class PlayerUI : UI, IComposable
{
    public Image HealthBar;
    public DeathDimmer DeathDimmer;
    public TextMeshProUGUI Ammo;
    public TextMeshProUGUI Score;
    public TextMeshProUGUI KillFeed;

    private float _health;
    public float Health
    {
        get => _health;
        set
        {
            _health = value;
            HealthBar.fillAmount -= _health;
            if (HealthBar.fillAmount < 0.50f && HealthBar.fillAmount >= 0.25f)
                HealthBar.color = Color.yellow;
            else if (HealthBar.fillAmount < 0.25f && HealthBar.fillAmount >= 0f)
                HealthBar.color = Color.red;
            else
                HealthBar.color = Color.green;
        }
    }
    private int _currentAmmo;
    public int Clip;
    public int CurrentAmmo
    {
        get => _currentAmmo;
        set
        {
            _currentAmmo = value;
            Ammo.text = _currentAmmo.ToString() + "/" + Clip.ToString();
        }
    }


    private void Start()
    {
        Init();
    }

    public void ResetThis()
    {
        HealthBar.fillAmount = 1;
        HealthBar.color = Color.green;
        DeathDimmer.gameObject.SetActive(false);
    }

    public void SetUI()
    {
        DeathDimmer.gameObject.SetActive(true);
    }

    public void Init(int ammo, int clip)
    {
        Clip = clip;
        CurrentAmmo = ammo;
    }

    public void Init()
    {
        DeathDimmer = GetComponentInChildren<DeathDimmer>(true);
        HealthBar.fillAmount = 1;
    }
}
