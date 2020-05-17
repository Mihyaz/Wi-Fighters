using UnityEngine;
using Mihyaz;

public class CommandSystem : MonoBehaviour, ICommand
{
    private Commands _commandType;
    private Commands.Executables _executables;
    private Converter.StringToVector2 _stringToVector2;

    private string _commandMovement = "";
    private string _commandRotation = "";
    private string _commandShooting = "";
    private string _commandReloading = "";


    private void Awake()
    {
        _commandType = new Commands();
        _executables = new Commands.Executables(false, false);
        _stringToVector2 = new Converter.StringToVector2();
    }

    public void Executer(string clientMessage)
    {
        try
        {
            if (clientMessage.Contains("+"))
            {
                if (clientMessage.Substring(clientMessage.IndexOf('+'), 5) == "+" + _commandType.CommandsDictionary[CommandTypes.Move])
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
        if (_commandShooting == _commandType.CommandsDictionary[CommandTypes.Shoot])
            return true;
        else if (_commandShooting == _commandType.CommandsDictionary[CommandTypes.Stop])
            return false;
        else
            return false;
    }

    public bool Reload()
    {
        if (_commandReloading == _commandType.CommandsDictionary[CommandTypes.Reload])
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
        if(!_executables.NameExecuted)
        {
            GetComponent<Player>().Name = name;
            _executables.NameExecuted = true;
        }
    }

    public void AssignClass(string index)
    {
        if(!_executables.ClassExecuted)
        {
            GunClasses gun;
            switch (index)
            {
                case "Rifle":
                    gun = GunClasses.Rifle;
                    break;
                case "Shotgun":
                    gun = GunClasses.Shotgun;
                    break;
                case "Handgun":
                    gun = GunClasses.Handgun;
                    break;
                case "Laser":
                    gun = GunClasses.Laser;
                    break;
                default:
                    gun = GunClasses.Rifle;
                    break;
            }

            GetComponent<Player>().InvokePlayerCreated(gun);
           _executables.ClassExecuted = true;
        }
    }

}
