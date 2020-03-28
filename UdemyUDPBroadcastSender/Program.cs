using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace UdemyUDPBroadcastSender
{
    class Program
    {
        static void Main(string[] args)
        {
            Socket sockBroadCaster = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            //AddressFamily.InterNetwork == using IP v4
            //SocketType.Dgram == sending datagram data
            //ProtocolType.Udp = using UDP protocal

            sockBroadCaster.EnableBroadcast = true;  //because we are using this socket to send a broadcast

            IPEndPoint broadcastEP = new IPEndPoint(IPAddress.Parse("255.255.255.255"), 23000);
            //first input :: IP address ~ since we are broadcastig set the IP to 1.1.1.1
            //secound input :: port no of the reciver 

            //the array of data that we want to broadcast (we can only send byte arrays)
            byte[] broadcastBuffer = new byte[] { 0x0D, 0x0A}; // stands for /r/n

            try
            {
                sockBroadCaster.SendTo(broadcastBuffer, broadcastEP); //send operation
                //1 :: the array we are sending 
                //2 :: end point where the data we want to go

                sockBroadCaster.Shutdown(SocketShutdown.Both); // disable all operation of the socket object
                sockBroadCaster.Close();// free any resourses that are associated with this socket object

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}
