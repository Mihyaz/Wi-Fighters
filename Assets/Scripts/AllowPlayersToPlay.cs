using System.Collections.Generic;
using UnityEngine;

public class AllowPlayersToPlay : MonoBehaviour
{
    public List<Player> Players = new List<Player>();
    public void Start()
    {
        GameManager.Instance.OnGameStart += () =>
        {
            for (int i = 0; i < 4; i++)
            {
                if(Players[i].IsConnected)
                    Players[i].enabled = true;
            }
        };
    }
}
