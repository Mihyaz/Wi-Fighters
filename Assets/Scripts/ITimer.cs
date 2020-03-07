using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITimer
{
    bool Countdown();
    float TimeInSeconds { get; set; }
}
