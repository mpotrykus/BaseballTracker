using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR.Client;

namespace MK.BaseballTracker.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Which operation do you wish to perform?");
            Console.WriteLine("Connection to the Channel (c)");
            Console.WriteLine("Send message to Channel (s)");
            Console.WriteLine("Exit (x)");

            string operation = Console.ReadLine();

            while (operation != "x")
            {
                switch (operation)
                {
                    case "c":
                        Console.WriteLine("Connect");
                        ConnectToChannel();
                        break;
                    case "s":
                        Console.WriteLine("Send Message");
                        break;

                }


                Console.WriteLine("Which operation do you wish to perform?");
                Console.WriteLine("Connection to the Channel (c)");
                Console.WriteLine("Send message to Channel (s)");
                Console.WriteLine("Exit (x)");


                operation = Console.ReadLine();
            }

        }

        static void ConnectToChannel()
        {
            // Set connection
            var hubConnection = new HubConnection("https://localhost:44376/");
            var myHub = hubConnection.CreateHubProxy("Channel1Hub");

            string name = "Mitch";

            // Start the connection
            hubConnection.Start().ContinueWith(task =>
            {
                if (task.IsFaulted)
                {
                    // There was an error.  Writeline
                    Console.WriteLine("There was an error opening the connection: {0}", task.Exception.GetBaseException());
                }
                else
                {
                    // Connected.
                    Console.WriteLine("Connected...");

                    // Set up a recieve message function
                    myHub.On<string, string>("addMessage", (who, message) =>
                    {
                        Console.WriteLine(who + ": " + message);
                    });

                    // Set up a send message function
                    while (true)
                    {
                        string message = Console.ReadLine();

                        if (string.IsNullOrEmpty(message))
                        {
                            break;
                        }

                        myHub.Invoke<string>("Send", name, message).ContinueWith(task1 =>
                        {
                            if (task1.IsFaulted)
                            {
                                // There was an error.  Writeline
                                Console.WriteLine("There was an error opening the connection: {0}", task1.Exception.GetBaseException());
                            }
                            else
                            {
                                Console.WriteLine(task1.Result);
                            }
                        });
                    }

                }
            }).Wait();

            hubConnection.Stop();

        }
    }
}
