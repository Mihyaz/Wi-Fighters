using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using OnurMihyaz;
using System;
using DG.Tweening;
using UnityEngine.UI;

public class GameUI : UI
{
    public List<TextMeshProUGUI> GameTime;
    public List<KillFeed> KillFeeds;
    public IPv4Viewer IPv4Viewer;

    private int FunctionCounter = 0;

    public void RefreshKillFeed(string PlayerKill, string PlayerDie)
    {
        if (FunctionCounter == 3)
        {
            FunctionCounter = 0;
        }
        for (int i = 0; i < KillFeeds.Count; i++)
        {
            KillFeeds[i].Text[FunctionCounter].text = PlayerKill + "      " + PlayerDie;
            KillFeeds[i].Text[FunctionCounter].gameObject.SetActive(true);
            KillFeeds[i].Icon[FunctionCounter].SetActive(true);
        }
        StartCoroutine(MihyazDelay.Delay(5f, () =>
        {
            int ct = FunctionCounter;
            for (int j = 0; j < KillFeeds.Count; j++)
            {
                KillFeeds[j].Text[ct - 1].gameObject.SetActive(false);
                KillFeeds[j].Icon[ct - 1].SetActive(false);
            }
            FunctionCounter--;
        }));
        FunctionCounter++;
    }
}
[System.Serializable]
public class KillFeed : ITimer
{
    public TextMeshProUGUI[] Text;
    public GameObject[] Icon;
    public int FunctionCounter = 0;
    private bool _finished = false;
    public float TimeInSeconds { get; set; }
    public bool Countdown()
    {
        if (TimeInSeconds > 0)
        {
            TimeInSeconds -= Time.deltaTime;
            return _finished;
        }
        else
        {
            return !_finished;
        }
    }
    //This class has created in March 4th in Chocolabs with Ruby <3//
}

[System.Serializable]
public class IPv4Viewer
{
    [Header("IPv4 List")]
    public TextMeshProUGUI[] IPv4;
    public TextMeshProUGUI ConnectedPlayers;
    public Text WaitingForPlayers;
    public GameObject Background;
    public GameObject Icon;
    public Image Eyes;

    public void StartGlowEyes()
    {
        Eyes.DOColor(Color.red, 1.5f).SetLoops(-1, LoopType.Yoyo);
        WaitingForPlayers.DOText("...", 3f).SetLoops(-1, LoopType.Restart);
    }
    public void RefreshConnectedPlayers(int connectedPlayers)
    {
        ConnectedPlayers.text = connectedPlayers + "/" + "2";
    }
    public void ViewIPv4(string ipv4)
    {
        string[] addresses = ipv4.Split('.');
        IPv4[0].text = addresses[2];
        IPv4[1].text = addresses[3];
    }

    public void CloseIPv4()
    {
        Icon.transform.parent.transform.SetParent(Icon.transform.parent.root);
        Icon.transform.DOScale(10, 0.5f);
        Icon.transform.DOLocalMoveY(0, 0.5f).OnComplete(() =>
        {
            Eyes.DOKill(false);
            Eyes.DOColor(Color.red, 0.5f).SetLoops(6, LoopType.Yoyo).OnComplete(() =>
            {
                Icon.transform.DOScale(0, 0.75f).OnComplete(() =>
                {
                    Icon.transform.parent.gameObject.SetActive(false);
                });
            });
        });
        Background.SetActive(false);
    }
}

