using System.Collections.Generic;
using UnityEngine;

public class AllowPlayersToPlay : MonoBehaviour
{
    public void Start()
    {
        GameManager.Instance.OnGameStart += () =>
        {
            for (int i = 0; i < 4; i++)
            {
                if (GameManager.Instance.AllPlayers[i].IsConnected)
                    GameManager.Instance.AllPlayers[i].enabled = true;
            }
        };
    }
}