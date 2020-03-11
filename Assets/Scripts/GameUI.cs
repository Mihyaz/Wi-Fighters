using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using OnurMihyaz;
using System;

public class GameUI : UI
{
    public List<TextMeshProUGUI> GameTime;
    public List<KillFeed> KillFeeds;
    public void RefreshKillFeed(string PlayerKill, string PlayerDie)
    {
        for (int i = 0; i < KillFeeds.Count; i++)
        {
            KillFeeds[i].Text.text += PlayerKill + "      " + PlayerDie + "\n";
            KillFeeds[i].Icon[KillFeed.FunctionCounter].SetActive(true);
            if (KillFeed.FunctionCounter == 2)
            {
                KillFeed killFeed = new KillFeed
                {
                    TimeInSeconds = 5
                };
                StartCoroutine(MihyazDelay.WaitUntilThis(() =>
                {
                    if (!killFeed.Countdown())
                        return false;
                    else
                    {
                        for (int k = 0; k < 2; k++)// k actually is 4
                        {
                            for (int j = 0; j < 3; j++)
                            {
                                KillFeeds[k].Icon[j].SetActive(false);
                            }
                            KillFeeds[k].Text.text = "";
                            KillFeed.FunctionCounter = 0;
                        }
                        return true;
                    }
                }));
            }
        }
        KillFeed.FunctionCounter++;
    }
}


[System.Serializable]
public class KillFeed : ITimer
{
    public TextMeshProUGUI Text;
    public GameObject[] Icon;
    public static int FunctionCounter = 0;
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

