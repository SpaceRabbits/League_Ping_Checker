using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

// Implement thread
namespace PingChecker
{
    class Program
    {
        static void Main(string[] args)
        {
            int x = 0;
            long max = 0;
            long acc = 0;
            int checkCount = 20;
            String region = "";
            String location = "";
            String input = "";
            String address = "104.160.131.3";
            Ping pingSender = new Ping();

            if (args.Length == 3)
            {
                checkCount = Int32.Parse(args[0]);
                if(args[1].Equals("NA"))
                {
                    address = "104.160.131.3";
                }else if(args[1].Equals("EUW"))
                {
                    address = "104.160.141.3";
                }else if(args[1].Equals("EUNE"))
                {
                    address = "104.160.142.3";
                }else if(args[1].Equals("OCE"))
                {
                    address = "104.160.156.1";
                }else if(args[1].Equals("LAN"))
                {
                    address = "104.160.136.3";
                }else
                {
                    address = args[1];
                }
                region = args[1];
                location = args[2];
            }
            else
            {
                Console.WriteLine("Please follow the instructions and try again.");
                System.Environment.Exit(1);
            }

            PingReply reply = pingSender.Send(address);
            Console.WriteLine("IP Address: " + reply.Address.ToString() + " "+ region);

            if (reply.Status != IPStatus.Success)
            {
                Console.WriteLine(reply.Status);
            }
            else
            {
                while (x < checkCount && reply.Status == IPStatus.Success)
                {
                    reply = pingSender.Send(address);
                    if (max < reply.RoundtripTime)
                    {
                        max = reply.RoundtripTime;
                    }
                    Console.WriteLine("Ping: " + reply.RoundtripTime);
                    acc += reply.RoundtripTime;
                    x++;

                }
            }
            Console.WriteLine("Times checked: " + x);
            Console.WriteLine("Average ping: " + acc / checkCount);
            Console.WriteLine("Maximum ping: " + max);

            Console.WriteLine("Play?: Y/N");
            input = System.Console.ReadLine().Trim().ToLower();
            if (input == "n")
            {
                System.Environment.Exit(1);
            }

            else
            {
                System.Diagnostics.Process.Start(location);
            }
        }
    }
}
