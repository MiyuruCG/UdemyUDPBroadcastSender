/* 
 * Udemy :: UDP Socket Programming For Distributed Computing in C# .NET
 */
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

            string strUserInput = string.Empty;


            //need to create an endpoint object , so we will create IPendpoint object
            IPEndPoint ipEpSender = new IPEndPoint(IPAddress.Any, 0);
            //1 ::this endpoint will work with any network interface that is attached to this machine
            //2 :: to use any freely available port no

            //converting into a endpoint 
            EndPoint epSender = (EndPoint)ipEpSender;

            int countReceived = 0;
            string txtReceived = string.Empty;

            try
            {
                //we need to bind the socket in this application to receive data (to a endpoint)
                sockBroadCaster.Bind(new IPEndPoint(IPAddress.Any, 0));
                //para 1 :: is accecible through any network interface available on this machine
                //para 2 :: the system will assign any available port no to this program

                while (true)
                {

                    Console.WriteLine("Please enter string to broad cast, Type  <Exit> to close ...... ");
                    strUserInput = Console.ReadLine();

                    if (strUserInput.Equals("<Exit>"))
                    {
                        break;
                    }

                    //to send the string we need convert it in to a byte[]
                    broadcastBuffer = Encoding.ASCII.GetBytes(strUserInput);//string to byte[]

                    sockBroadCaster.SendTo(broadcastBuffer, broadcastEP); //send operation
                    //1 :: the array we are sending 
                    //2 :: end point where the data we want to go

                    if (strUserInput.Equals("<Echo>"))
                    {
                        /*
                     //receive methord
                     countReceived = sockBroadcastReceiver.Receive(receiverBuffer);
                   */
                        //change the receive methord to get the ip address of the sender 
                        countReceived = sockBroadCaster.ReceiveFrom(broadcastBuffer, ref epSender);



                        txtReceived = Encoding.ASCII.GetString(broadcastBuffer, 0, countReceived);
                        //1 :: the received buffer
                        //2 :: the starting point index of the getString operation
                        //3 :: no of bytes we need to convert in to text

                        Console.WriteLine("Number of byted recieved : " + countReceived);
                        Console.WriteLine(" Received :: " + txtReceived);
                        Console.WriteLine("Received from :: " + epSender.ToString());

                       
                    }


                }//while ends here

                sockBroadCaster.Shutdown(SocketShutdown.Both); // disable all operation of the socket object
                sockBroadCaster.Close();// free any resourses that are associated with this socket object

            }//try ends here
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

        }
    }
}

    