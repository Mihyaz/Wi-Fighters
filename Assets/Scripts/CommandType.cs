using System.Collections.Generic;
using UnityEngine;

public enum CommandTypes
{
    Move,
    Shoot,
    Reload,
    Stop
}

public class CommandType : MonoBehaviour
{
    public Dictionary<CommandTypes, string> CommandsDic = new Dictionary<CommandTypes, string>();

    private void Awake()
    {
        CommandsDic.Add(CommandTypes.Move, "Move");
        CommandsDic.Add(CommandTypes.Shoot, "Shoot");
        CommandsDic.Add(CommandTypes.Reload, "Reload");
        CommandsDic.Add(CommandTypes.Stop, "Stop");
    }

}
