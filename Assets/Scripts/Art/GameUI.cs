using System.Collections.Generic;
using TMPro;
using UnityEngine;
using OnurMihyaz;
using System;
using DG.Tweening;
using UnityEngine.UI;
using System.IO.IsolatedStorage;

public class GameUI : UI
{
    private int FunctionCounter;

    public List<TextMeshProUGUI> GameTime;
    public IPv4Viewer IPv4Viewer;

    private void Awake()
    {
        IPv4Viewer.Init();
    }


    [Serializable]
    [Obsolete]
    public class KillFeed : ITimer<float>
    {
        public float TimeInSeconds { get; set; }
        public List<GameObject> FeedPanels;
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
}

