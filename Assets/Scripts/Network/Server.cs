﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Networking;
using System.IO;
using System.Threading.Tasks;

public class Server : MonoBehaviour
{
    #region private members 	
    /// <summary> 	
    /// TCPListener to listen for incomming TCP connection 	
    /// requests. 	
    /// </summary> 	
    private TcpListener _tcpListener;
    /// <summary> 
    /// Background thread for TcpServer workload. 	
    /// </summary> 	
    private Thread _tcpListenerThread;
    /// <summary>	
    /// Create handle to connected tcp client. 	
    /// </summary> 	
    private TcpClient _connectedTcpClient;
    #endregion

    private static IPAddress ___IPv4___;

    public static int ConnectedClient;

    public Player Player_1;
    public Player Player_2;
    public Player Player_3;
    public Player Player_4;

    void Start()
    {
        _tcpListenerThread = new Thread(new ThreadStart(ListenForIncommingRequests))
        {
            //IsBackground = true
        };
        _tcpListenerThread.Start();
    }
    private void Update()
    {
        
        Player_1.CommandManager.Executer(Player_1.Command);
        Player_2.CommandManager.Executer(Player_2.Command);
        Player_3.CommandManager.Executer(Player_3.Command);
        Player_4.CommandManager.Executer(Player_4.Command);
    }

    private void ListenForIncommingRequests()
    {
        try
        {
            // Create listener on localhost port 8000. 	
            _tcpListener = new TcpListener(IPAddress.Any, 8000);
            _tcpListener.Start();
            Debug.Log("<color=yellow>Server is </color><color=red>listening </color>");

            while (true)
            {
                _connectedTcpClient = _tcpListener.AcceptTcpClient();
                ThreadPool.QueueUserWorkItem(ThreadProcAsync, _connectedTcpClient);
                Debug.Log("Client: " + (ConnectedClient + 1));
            }
        }
        catch (SocketException socketException)
        {
            Debug.Log("SocketException " + socketException.ToString());
        }
    }

    private void ThreadProcAsync(object obj)
    {
        var connectedTcpClient = (TcpClient)obj;
        ConnectedClient++;
        PlayerProfile player = new PlayerProfile("Player" + ConnectedClient);

        // Get a stream object for reading 
        if (player.Name == "Player1")
        {
            using (StreamReader stream = new StreamReader(connectedTcpClient.GetStream()))
            {
                // Read incomming stream into byte arrary. 	
                Player_1.OnPlayerCreated += delegate { Event_OnPlayerCreated(Player_1); };
                while (connectedTcpClient.Connected)
                {
                    Player_1.Command = stream.ReadLine();
                }
            }
        }

        if (player.Name == "Player2")
        {
            using (StreamReader stream = new StreamReader(connectedTcpClient.GetStream()))
            {
                // Read incomming stream into byte arrary.
                Player_2.OnPlayerCreated += delegate { Event_OnPlayerCreated(Player_2); };
                while (connectedTcpClient.Connected)
                {
                    Player_2.Command = stream.ReadLine();
                }
            }
        }

        if (player.Name == "Player3")
        {
            using (StreamReader stream = new StreamReader(connectedTcpClient.GetStream()))
            {
                // Read incomming stream into byte arrary. 		
                Player_3.OnPlayerCreated += delegate { Event_OnPlayerCreated(Player_3); };
                while (connectedTcpClient.Connected)
                {
                    Player_3.Command = stream.ReadLine();
                }
            }
        }

        if (player.Name == "Player4")
        {
            using (StreamReader stream = new StreamReader(connectedTcpClient.GetStream()))
            {
                // Read incomming stream into byte arrary. 		
                Player_4.OnPlayerCreated += delegate { Event_OnPlayerCreated(Player_4); };
                while (connectedTcpClient.Connected)
                {
                    Player_4.Command = stream.ReadLine();
                }
            }
        }
    }

    private void Event_OnPlayerCreated(Player player)
    {
        player.IsConnected = true;
    }

    /// <summary> 	
    /// Send message to client using socket connection. 	
    /// </summary> 	
    public void SendMessageToClient(string message)
    {
        if (_connectedTcpClient == null)
        {
            // Connection Failed
            return;
        }
        //else
        //Connection Established
        try
        {
            // Get a stream object for writing. 			
            NetworkStream stream = _connectedTcpClient.GetStream();
            if (stream.CanWrite)
            {
                string serverMessage = message;
                // Convert string message to byte array.                 
                byte[] serverMessageAsByteArray = Encoding.ASCII.GetBytes(serverMessage);
                // Write byte array to socketConnection stream.               
                stream.Write(serverMessageAsByteArray, 0, serverMessageAsByteArray.Length);
            }
        }
        catch (SocketException socketException)
        {
            Debug.Log("Socket exception: " + socketException);
        }
    }

    [Obsolete]
    public static string GetIpAdress()
    {
        string strHostName = Dns.GetHostName();
        IPHostEntry ipEntry = Dns.GetHostEntry(strHostName);
        IPAddress[] addr = ipEntry.AddressList;
        ___IPv4___ = addr[1];
        return ___IPv4___.MapToIPv4().ToString();
    }

    public static string GetIPv4Adress()
    {
        IPAddress[] IPv4Addresses = Array.FindAll(Dns.GetHostEntry(string.Empty).AddressList,
        a => a.AddressFamily == AddressFamily.InterNetwork);

        return IPv4Addresses[1].ToString();
    }

}
