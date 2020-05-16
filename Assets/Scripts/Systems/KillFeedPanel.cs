using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using OnurMihyaz;

public class KillFeedPanel : MonoBehaviour
{
    private Player _player;
   
    public List<GameObject> FeedPanels;
    public List<TextMeshProUGUI> KillerText = new List<TextMeshProUGUI>();
    public List<TextMeshProUGUI> DeathText = new List<TextMeshProUGUI>();

    private void Awake()
    {
        _player = transform.root.GetComponent<Player>();
        _player.OnKillFeedRefreshed += RefreshKillFeed;
    }

    public void RefreshKillFeed(string playerKill, string playerDie)
    {
        for (int i = 0; i < FeedPanels.Count; i++)
        {
            if (!FeedPanels[i].activeInHierarchy)
            {
                KillerText[i].text = playerKill;
                DeathText[i].text = playerDie;
                FeedPanels[i].SetActive(true);

                StartCoroutine(MihyazDelay.Delay(5f, () =>
                {
                    FeedPanels[i].SetActive(false);
                }));

                break;
            }
        }
    }
}
