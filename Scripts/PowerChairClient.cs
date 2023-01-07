using DilmerGames.Core.Singletons;//Some awesome Dilmer code for a Singleton
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading;
using UnityEngine;

public class PowerChairClient : Singleton<PowerChairClient>
{
    public static Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
    private static readonly ConstantsToExclude constantsToExclude = new ConstantsToExclude();
    public static IPAddress broadcast = IPAddress.Parse(constantsToExclude.IPAddresLocal);
    public static IPEndPoint ep = new IPEndPoint(broadcast, constantsToExclude.localPort);

    public bool pauseActive { set; get;}


    public Socket getSock()
    {
        return s;
    }

    public IPEndPoint getEP()
    {
        return ep;
    }
}
