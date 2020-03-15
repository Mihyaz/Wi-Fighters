using System.Collections.Generic;
using UnityEngine;

public enum CommandTypes
{
    Move,
    Shoot,
    Reload,
    Stop
}

public class CommandType
{
    public Dictionary<CommandTypes, string> CommandsDic = new Dictionary<CommandTypes, string>();
    public CommandType()
    {
        CommandsDic.Add(CommandTypes.Move, "Move");
        CommandsDic.Add(CommandTypes.Shoot, "Shoot");
        CommandsDic.Add(CommandTypes.Reload, "Reload");
        CommandsDic.Add(CommandTypes.Stop, "Stop");
    }

}
