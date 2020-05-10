using System.Collections.Generic;
using UnityEngine;

public class AllowPlayersToPlay : MonoBehaviour
{
    public List<Player> Players = new List<Player>();
    public void Start()
    {
        GameManager.Instance.OnGameStart += () =>
        {
            for (int i = 0; i < 2; i++)
            {
                Players[i].enabled = true;
            }
        };
    }
}
