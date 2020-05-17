using System;
using UnityEngine;
public class PlayerConnectionException : Exception
{
    public override string Message => base.Message;
    public PlayerConnectionException(string message) : base (message)
    {
        Debug.LogError(message + " has lost connection");
    }
}
