using System.Collections.Generic;
using TMPro;
using UnityEngine;
using OnurMihyaz;
using System;
using DG.Tweening;
using UnityEngine.UI;

public class GameUI : UI
{
    private int FunctionCounter;

    public List<TextMeshProUGUI> GameTime;
    public List<KillFeed> KillFeeds;
    public IPv4Viewer IPv4Viewer;

    private void Awake()
    {
        IPv4Viewer.Init();
    }

    public void RefreshKillFeed(string PlayerKill, string PlayerDie)
    {
        if (FunctionCounter == 3)
            FunctionCounter = 0;

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
            for (int i = 0; i < KillFeeds.Count; i++)
            {
                KillFeeds[i].KillerText[ct - 1].gameObject.SetActive(false);
                KillFeeds[i].DeathText[ct - 1].gameObject.SetActive(false);
                KillFeeds[i].Icon[ct - 1].SetActive(false);
            }
            FunctionCounter--;
        }));
        FunctionCounter++;
    }
}

[Serializable]
public class KillFeed : ITimer<float>
{
    public float TimeInSeconds { get; set; }
    public List<TextMeshProUGUI> KillerText = new List<TextMeshProUGUI>();
    public List<TextMeshProUGUI> DeathText = new List<TextMeshProUGUI>();
    public List<GameObject> Icon = new List<GameObject>();

    public bool Countdown()
    {
        if (TimeInSeconds > 0)
        {
            TimeInSeconds -= Time.deltaTime;
            return false;
        }
        else
        {
            return true;
        }
    }

    //This class has created in March 4th in Chocolabs with Ruby <3//
}

