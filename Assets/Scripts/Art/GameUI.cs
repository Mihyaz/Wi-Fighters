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
    private int FunctionCounter;

    private void Awake()
    {
        for (int i = 0; i < 2; i++)
        {
            KillFeeds[i].Init();
        }
        IPv4Viewer.Init();
    }

    public void RefreshKillFeed(string PlayerKill, string PlayerDie)
    {
        if (FunctionCounter == 3)
        {
            FunctionCounter = 0;
        }
        for (int i = 0; i < KillFeeds.Count; i++)
        {
            KillFeeds[i].KillerText[FunctionCounter].text = PlayerKill;
            KillFeeds[i].DeathText[FunctionCounter].text = PlayerDie;
            KillFeeds[i].KillerText[FunctionCounter].gameObject.SetActive(true);
            KillFeeds[i].DeathText[FunctionCounter].gameObject.SetActive(true);
            KillFeeds[i].Icon[FunctionCounter].SetActive(true);
        }
        StartCoroutine(MihyazDelay.Delay(5f, () =>
        {
            int ct = FunctionCounter;
            for (int j = 0; j < KillFeeds.Count; j++)
            {
                KillFeeds[j].KillerText[ct - 1].gameObject.SetActive(false);
                KillFeeds[j].DeathText[ct - 1].gameObject.SetActive(false);
                KillFeeds[j].Icon[ct - 1].SetActive(false);
            }
            FunctionCounter--;
        }));
        FunctionCounter++;
    }
}
[System.Serializable]
public class KillFeed : ITimer<float>, IComposable
{
    public float TimeInSeconds { get; set; }
    public TextMeshProUGUI[] KillerText;
    public TextMeshProUGUI[] DeathText;
    public GameObject[] Icon;
    private bool _finished;

    public void Init()
    {
        string KillFeedText_ = "KillFeedText_";
        for (int i = 0; i < 3; i++)
        {
            KillerText[i] = GameObject.Find(KillFeedText_ + (i + 1)).GetComponent<TextMeshProUGUI>();
            DeathText[i] = GameObject.Find(KillFeedText_ + (i + 1) + "(Death)").GetComponent<TextMeshProUGUI>();
            Icon[i] = GameObject.Find("KillFeedIcon_" + (i + 1));
        }
    }
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

    public void ResetThis()
    {
        throw new NotImplementedException();
    }
    //This class has created in March 4th in Chocolabs with Ruby <3//
}
[System.Serializable]
public class IPv4Viewer : IComposable
{
    public TextMeshProUGUI[] IPv4 = new TextMeshProUGUI[2];
    public TextMeshProUGUI ConnectedPlayers;
    public Text WaitingForPlayers;
    public GameObject AdressScreen;
    public GameObject Icon;
    public Image Eyes;

    public void Init()
    {
        for (int i = 0; i < IPv4.Length; i++)
        {
            IPv4[i] = GameObject.Find("IP_" + (i + 1)).GetComponent<TextMeshProUGUI>();
        }
        ConnectedPlayers  = GameObject.Find("ConnectedPlayers").GetComponent<TextMeshProUGUI>();
        WaitingForPlayers = GameObject.Find("Dots").GetComponent<Text>();
        AdressScreen      = GameObject.Find("IPAdressScreen");
        Icon = GameObject.Find("GameIcon");
        Eyes = GameObject.Find("Eyes").GetComponent<Image>();
    }

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
        AdressScreen.SetActive(false);
    }

    public void ResetThis()
    {
        throw new NotImplementedException();
    }
}

