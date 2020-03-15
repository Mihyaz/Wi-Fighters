using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OnurMihyaz;
using TMPro;
using System;
using Zenject;

public class GameManager : MonoBehaviour, ITimer<float>
{
    [Inject][HideInInspector]
    public GameUI UI;

    public static GameManager Instance;

    public delegate void GameEvents();
    public event GameEvents OnGameFinish;
    public event GameEvents OnGameStart;

    private IEnumerator _gameTimeCo;
    private IEnumerator _clientsCo;
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
        _clientsCo = MihyazDelay.WaitUntilThis(CheckIfEverbodyConnected);
        StartCoroutine(_clientsCo);
        UI.IPv4Viewer.ViewIPv4(Server.GetIpAdress());
        UI.IPv4Viewer.StartGlowEyes();
        OnGameStart += () =>
        {
            StartCoroutine(_gameTimeCo);
            StopCoroutine(_clientsCo);
        };
        OnGameFinish += () => StopCoroutine(_gameTimeCo);
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
    public bool CheckIfEverbodyConnected()
    {
        if (Server.ConnectedClient == 2)
        {
            GameStart();
            UI.IPv4Viewer.CloseIPv4();
            return true;
        }
        else
        {
            UI.IPv4Viewer.RefreshConnectedPlayers(Server.ConnectedClient);
            return false;
        }
            
    }
}
