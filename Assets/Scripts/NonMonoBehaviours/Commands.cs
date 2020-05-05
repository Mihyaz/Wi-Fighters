using System.Collections.Generic;
public enum CommandTypes
{
    Move,
    Shoot,
    Reload,
    Stop
}
public class Commands
{
    public Dictionary<CommandTypes, string> CommandsDictionary = new Dictionary<CommandTypes, string>();
    public Commands()
    {
        CommandsDictionary.Add(CommandTypes.Move, "Move");
        CommandsDictionary.Add(CommandTypes.Shoot, "Shoot");
        CommandsDictionary.Add(CommandTypes.Reload, "Reload");
        CommandsDictionary.Add(CommandTypes.Stop, "Stop");
    }

    public struct Executables
    {
        public bool NameExecuted;
        public bool ClassExecuted;

        public Executables(bool Name, bool Class)
        {
            NameExecuted = Name;
            ClassExecuted = Class;
        }
    }
}
