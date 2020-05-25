using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OnurMihyaz;
using TMPro;
using System;
using Zenject;

public class GameManager : MonoBehaviour, ITimer<float>
{
    [Inject]
    [HideInInspector]
    public GameUI UI;

    public static GameManager Instance;

    public delegate void GameEvents();
    public event GameEvents OnGameFinish;
    public event GameEvents OnGameStart;

    public List<Player> AllPlayers;

    private IEnumerator _gameTimeCo;
    private IEnumerator _clientsCo;
    public float TimeInSeconds { get; set; }
    public int ConnectedClient;


    private void Awake()
    {
        if (gameObject != null)
            Instance = this;

        for (int i = 0; i < 4; i++)
            AllPlayers.Add(FindObjectsOfType<Player>()[i]);

        TimeInSeconds = 2 * 60; // 10 minutes
        ConnectedClient = 4;
    }

    private void Update()
    {
        print(ConnectedClient);
    }

    private void Start()
    {
        _gameTimeCo = MihyazDelay.WaitUntilThis(Countdown);
        _clientsCo = MihyazDelay.WaitUntilThis(CheckIfEverbodyConnected);
        StartCoroutine(_clientsCo);
        UI.IPv4Viewer.ViewIPv4(Server.GetIPv4Adress());
        UI.IPv4Viewer.StartGlowEyes();
        OnGameStart += () =>
        {
            StartCoroutine(_gameTimeCo);
            StopCoroutine(_clientsCo);
        };
        OnGameFinish += () => StopCoroutine(_gameTimeCo);
    }

    public void FinishGame()
    {
        OnGameFinish();
    }
    public void StartGame()
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
            FinishGame();
            return true;
        }
    }

    public bool CheckIfEverbodyConnected()
    {
        if (Server.ConnectedClient == ConnectedClient)
        {
            StartGame();
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
