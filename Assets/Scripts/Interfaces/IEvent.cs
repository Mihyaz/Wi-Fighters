using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void PlayerDelegate();
public interface IEvent
{
    PlayerDelegate PlayerDelegate { get; set; }
    event PlayerDelegate OnPlayerDeath;
    event PlayerDelegate OnPlayerRespawn;
}
