using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class LeaderboardSystem : MonoBehaviour
{
    [SerializeField] private List<Player> _players = new List<Player>();

    [SerializeField] private List<Attributes> _attributes = new List<Attributes>();

    private void Awake()
    {
        GameManager.Instance.OnGameFinish += Show;
        GameManager.Instance.OnGameStart  += Close;
    }

    private void Show()
    {
        gameObject.SetActive(true);
        var RankedList = _players.OrderByDescending(p => p.State.Score).ToList();
        for (int i = 0; i < RankedList.Count; i++)
        {
            _attributes[i].Name.text = RankedList[i].Name;
            _attributes[i].Kill.text = RankedList[i].State.Score.ToString();
            _attributes[i].Death.text = RankedList[i].State.Death.ToString();
        }
    }

    private void Close()
    {
        gameObject.SetActive(false);
    }

    [System.Serializable]
    public class Attributes
    {
        public TextMeshProUGUI Name;
        public TextMeshProUGUI Kill;
        public TextMeshProUGUI Death;
    }
}
