﻿using System.Collections.Generic;
public enum CommandTypes
{
    Move,
    Shoot,
    Reload,
    Stop
}
public class Commands
{
    public Dictionary<CommandTypes, string> CommandsDic = new Dictionary<CommandTypes, string>();
    public Commands()
    {
        CommandsDic.Add(CommandTypes.Move, "Move");
        CommandsDic.Add(CommandTypes.Shoot, "Shoot");
        CommandsDic.Add(CommandTypes.Reload, "Reload");
        CommandsDic.Add(CommandTypes.Stop, "Stop");
    }
}