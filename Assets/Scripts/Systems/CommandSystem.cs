using UnityEngine;
using Mihyaz;

public class CommandSystem : MonoBehaviour, ICommand
{
    private Commands _commandType;
    private Converter.StringToVector2 _stringToVector2;

    private string _commandMovement = "";
    private string _commandRotation = "";
    private string _commandShooting = "";
    private string _commandReloading = "";

    public bool Executed { get; private set; }

    private void Awake()
    {
        _commandType = new Commands();
        _stringToVector2 = new Converter.StringToVector2();
    }

    public void Executer(string clientMessage)
    {
        try
        {
            if (clientMessage.Contains("+"))
            {
                if (clientMessage.Substring(clientMessage.IndexOf('+'), 5) == "+" + _commandType.CommandsDic[CommandTypes.Move])
                {
                    string newClientMessage = clientMessage.Substring(0, clientMessage.LastIndexOf('+'));
                    string[] sArray = newClientMessage.Split('#');
                    _commandMovement = sArray[0];
                    _commandRotation = sArray[1];
                    _commandShooting = sArray[2];
                    _commandReloading = sArray[3];
                    AssignName(sArray[4]);
                    AssignClass(sArray[5]);
                }
            }
        }
        catch
        {
            throw new PlayerConnectionException(GetComponent<Player>().Name);
        }
    }

    public bool Shoot()
    {
        if (_commandShooting == _commandType.CommandsDic[CommandTypes.Shoot])
            return true;
        else if (_commandShooting == _commandType.CommandsDic[CommandTypes.Stop])
            return false;
        else
            return false;
    }

    public bool Reload()
    {
        if (_commandReloading == _commandType.CommandsDic[CommandTypes.Reload])
            return true;
        else
            return false;
    }

    public Vector2 GetRotation()
    {
        return _stringToVector2.Rotation(_commandRotation);
    }

    public Vector2 GetMovement()
    {
        return _stringToVector2.Movement(_commandMovement);
    }

    public void AssignName(string name)
    {
        if(!Executed)
        {
            GetComponent<Player>().Name = name;
            Executed = true;
        }
    }

    public void AssignClass(string index)
    {
    }

}
