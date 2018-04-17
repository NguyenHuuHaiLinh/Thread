using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Client
{
    class Program
    {
        static Socket sock;
        static void HandleConnection()
        {
            while (true)
            {
                byte[] data = new byte[1024];
                int recv = sock.Receive(data, SocketFlags.None);
                string stringData = Encoding.ASCII.GetString(data, 0, recv);
                Console.WriteLine(stringData);
            }
        }
        static void Main(string[] args)
        {
            sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream,
 ProtocolType.Tcp);
            IPEndPoint iep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 9050);
            byte[] data = new byte[1024];
            string stringData;
            int recv;
            sock.Connect(iep);
            Console.WriteLine("Connected to server");
            recv = sock.Receive(data);
            stringData = Encoding.ASCII.GetString(data, 0, recv);
            Console.WriteLine("Received: {0}", stringData);
            while (true)
            {

                Thread newthread = new Thread(new ThreadStart(HandleConnection));
                newthread.Start();
                stringData = Console.ReadLine();
                if (stringData == "exit")
                    break;
                data = Encoding.ASCII.GetBytes(stringData);
                sock.Send(data, data.Length, SocketFlags.None);
            }
            sock.Close();
        }
    }
}
