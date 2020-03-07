using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OnurMihyaz;
using TMPro;
using System;
using Zenject;

public class GameManager : MonoBehaviour, ITimer
{
    [Inject][HideInInspector]
    public GameUI UI;

    public static GameManager Instance;

    public delegate void GameEvents();
    public event GameEvents OnGameFinish;
    public event GameEvents OnGameStart;

    private IEnumerator _gameTimeCo;
    public float TimeInSeconds { get; set; }
    private void Awake()
    {
        if (gameObject != null)
            Instance = this;
        TimeInSeconds = 10 * 60; // 10 minutes
    }

    private void Start()
    {
        _gameTimeCo = MihyazDelay.WaitUntilThis(Countdown);
        OnGameStart += () => StartCoroutine(_gameTimeCo);
        OnGameFinish += () => StopCoroutine(_gameTimeCo);
        GameStart();
    }

    public void GameFinish()
    {
        OnGameFinish();
    }
    public void GameStart()
    {
        OnGameStart();
    }

    public bool Countdown()
    {
        if (TimeInSeconds > 0)
        {
            TimeInSeconds -= Time.deltaTime;
            for (int i = 0; i < UI.GameTime.Count; i++)
            {
                UI.GameTime[i].text = TimeSpan.FromSeconds(TimeInSeconds).ToString(@"mm\:ss");
            }
            return false;
        }
        else
        {
            GameFinish();
            return true;
        }
    }
}
