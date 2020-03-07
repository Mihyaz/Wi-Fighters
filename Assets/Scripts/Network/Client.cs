using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class Client : MonoBehaviour
{
    [Inject]
    MyIPAddress _myIPAddress;

    #region private members 	
    private TcpClient socketConnection;
    private Thread clientReceiveThread;
    private IPAddress _myIpAddress;
    #endregion

    string[] IPText = new string[2];
    public string MyName;
    public bool Connected;
    public InputField[] Field;
    public Button Submit;

    // Use this for initialization 	
    void Start()
    {
        //ConnectToTcpServer();
    }

    public void ConnectToServer()
    {

        //IPText[0] = Field[0].text;
        ConnectToTcpServer();
        MyName = Field[0].text;
        //IPText = _myIPAddress.DecodeIP(IPText);


    }

    /// <summary> 	
    /// Setup socket connection. 	
    /// </summary> 	
    private void ConnectToTcpServer()
    {
        try
        {
            clientReceiveThread = new Thread(new ThreadStart(ListenForData))
            {
                //IsBackground = true
            };
            clientReceiveThread.Start();
        }
        catch (Exception e)
        {
            Debug.Log("On client connect exception " + e);
        }
    }
    /// <summary> 	
    /// Runs in background clientReceiveThread; Listens for incomming data. 	
    /// </summary>     
    private void ListenForData()
    {
        try
        {
            //socketConnection = new TcpClient("192.168." + IPText[0] + "." + IPText[1], 8000);
            socketConnection = new TcpClient("192.168.1.15", 8000);
            Byte[] bytes = new Byte[1024];
            while (true)
            {
                // Get a stream object for reading 				
                using (NetworkStream stream = socketConnection.GetStream())
                {
                    int length;
                    // Read incomming stream into byte arrary. 					
                    while ((length = stream.Read(bytes, 0, bytes.Length)) != 0)
                    {
                        var incommingData = new byte[length];
                        Array.Copy(bytes, 0, incommingData, 0, length);
                        // Convert byte array to string message. 						
                        string serverMessage = Encoding.ASCII.GetString(incommingData);
                        Debug.Log("server message received as: " + serverMessage);
                    }
                }
            }
        }
        catch (SocketException socketException)
        {
            Debug.Log("Socket exception: " + socketException);
        }
    }
    /// <summary> 	
    /// Send message to server using socket connection. 	
    /// </summary> 	
    public async virtual void SendMessageToServer(string clientMessage, string type)
    {
        if (socketConnection == null)
        {
            return;
        }
        try
        {
            // Get a stream object for writing. 			
            NetworkStream stream = socketConnection.GetStream();
            StreamWriter writer = new StreamWriter(socketConnection.GetStream());
            writer.AutoFlush = true; //Either this, or you Flush manually every time you send something.

            if (stream.CanWrite)
            {
               await writer.WriteLineAsync(clientMessage + "+" + type);
            }
        }
        catch (SocketException socketException)
        {
            Debug.Log("Socket exception: " + socketException);
        }
    }

    public virtual void SendImageToServer(UnityEngine.UI.Image x)
    {
        if (socketConnection == null)
        {
            return;
        }
        try
        {
            // Get a stream object for writing. 			
            NetworkStream stream = socketConnection.GetStream();
            StreamWriter writer = new StreamWriter(socketConnection.GetStream());
            writer.AutoFlush = true; //Either this, or you Flush manually every time you send something.

            //ImageConverter _imageConverter = new ImageConverter();
            //byte[] xByte = (byte[])_imageConverter.ConvertTo(x, typeof(byte[]));

            if (stream.CanWrite)
            {
                //stream.Write(xByte, 0, xByte.Length);
            }
        }
        catch (SocketException socketException)
        {
            Debug.Log("Socket exception: " + socketException);
        }
    }
    public virtual void SendMessageToServer(string clientMessage)
    {
        if (socketConnection == null)
        {
            return;
        }
        try
        {
            // Get a stream object for writing. 			
            NetworkStream stream = socketConnection.GetStream();
            StreamWriter writer = new StreamWriter(socketConnection.GetStream());
            writer.AutoFlush = true; //Either this, or you Flush manually every time you send something.

            if (stream.CanWrite)
            {
                // Convert string message to byte array. 
                //byte[] clientMessageAsByteArray = Encoding.ASCII.GetBytes(clientMessage);
                // Write byte array to socketConnection stream.
                writer.WriteLine(clientMessage);

                //stream.WriteAsync(clientMessageAsByteArray, 0, clientMessageAsByteArray.Length);
            }
        }
        catch (SocketException socketException)
        {
            Debug.Log("Socket exception: " + socketException);
        }
    }
}