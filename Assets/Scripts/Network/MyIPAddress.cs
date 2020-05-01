using System.Globalization;
using System.Net;
using UnityEngine;

public class MyIPAddress : MonoBehaviour
{
    private IPAddress IP;
    private string[] _entry;
    private string[] _newEntry = new string[2];  
    private string[] _encodedHex = new string[2];
    private string[] _decodedHex = new string[2];

    private void Start()
    {
       
        IPHostEntry ipEntry = Dns.GetHostEntry(Dns.GetHostName());
        IP = ipEntry.AddressList[ipEntry.AddressList.Length - 1];
        _entry = IP.ToString().Split('.');

        _newEntry[0] = _entry[2];
        _newEntry[1] = _entry[3];

        _encodedHex = EncodeIP(_newEntry);
        _decodedHex = DecodeIP(_encodedHex);

    }


    public string[] EncodeIP(string[] _entryToEncoded)
    {
        for (int i = 0; i < _entryToEncoded.Length; i++)
        {
            _encodedHex[i] = int.Parse(_entryToEncoded[i]).ToString("X");
        }
        return _encodedHex;
    }

    public string[] DecodeIP(string[] _entryToDecode)
    {
        for (int i = 0; i < _entryToDecode.Length; i++)
        {
            _decodedHex[i] = int.Parse(_encodedHex[i], NumberStyles.HexNumber).ToString();
        }
        return _decodedHex;
    }

}
