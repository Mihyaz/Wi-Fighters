using System.Collections.Generic;
using UnityEngine;

public class AllowPlayersToPlay : MonoBehaviour
{
    public void Awake()
    {
        GameManager.Instance.OnGameStart += () =>
        {
            for (int i = 0; i < GameManager.Instance.ClientCount; i++)
            {
                GameManager.Instance.AllPlayers[i].enabled = true;
            }
        };
    }
}