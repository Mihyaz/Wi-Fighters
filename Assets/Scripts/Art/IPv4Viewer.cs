using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System;
using DG.Tweening;
using UnityEngine.UI;

[Serializable]
public class IPv4Viewer : IComposable
{
    private List<TextMeshProUGUI> IPv4 = new List<TextMeshProUGUI>();
    public TextMeshProUGUI ConnectedPlayers;
    public Text WaitingForPlayers;
    public GameObject AdressScreen;
    public GameObject Icon;
    public Image Eyes;

    public void Init()
    {
        for (int i = 0; i < 2; i++)
        {
            IPv4.Add(GameObject.Find("IP_" + (i + 1)).GetComponent<TextMeshProUGUI>());
        }
        ConnectedPlayers = GameObject.Find("ConnectedPlayers").GetComponent<TextMeshProUGUI>();
        WaitingForPlayers = GameObject.Find("Dots").GetComponent<Text>();
        AdressScreen = GameObject.Find("IPAdressScreen");
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
                Icon.transform.DOScale(0, 0.25f).OnComplete(() =>
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