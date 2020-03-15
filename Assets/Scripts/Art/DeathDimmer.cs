using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using OnurMihyaz;

public class DeathDimmer : MonoBehaviour, ITimer<float>
{
    private Image _dimmer;
    private Image _icon;
    private TextMeshProUGUI _timer;

    public float TimeInSeconds { get; set; }

    private void Awake()
    {
        _dimmer = GetComponent<Image>();
        _icon = GetComponentsInChildren<Image>()[1];
        _timer = GetComponentInChildren<TextMeshProUGUI>();
        if (gameObject.activeInHierarchy)
            gameObject.SetActive(false);
    }
    private void OnEnable()
    {
        _dimmer.DOFade(0.45f, 0.5f);
        _icon.transform.DOScale(1.9f, 0.5f);
        TimeInSeconds = 3f;
        StartCoroutine(MihyazDelay.WaitUntilThis(Countdown));
    }

    private void OnCountdownEnd()
    {
        _dimmer.DOFade(0f, 0.1f);
        _icon.transform.DOScale(0.5f, 0.1f);
        TimeInSeconds = 3f;
    }

    public bool Countdown()
    {
        if (TimeInSeconds > 0)
        {
            TimeInSeconds -= Time.deltaTime;
            _timer.text = ((int)TimeInSeconds).ToString();
            return false;
        }
        else
        {
            OnCountdownEnd();
            return true;
        }
    }
}
