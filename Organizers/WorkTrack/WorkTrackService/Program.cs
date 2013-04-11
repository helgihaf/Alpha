using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Description;

namespace WorkTrackService
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting service host...");
            ServiceHost serviceHost = new ServiceHost(typeof(WorkTrackService));
            serviceHost.Open();

            Console.WriteLine(serviceHost.Description.Endpoints[0].Contract.Name + " listening on:");
            foreach (var endpoint in serviceHost.Description.Endpoints)
            {
                Console.WriteLine("  " + endpoint.Address + " (" + endpoint.Binding.Name + ")");
            }

            Console.WriteLine();
            Console.WriteLine("Press any key to stop service.");

            Console.ReadKey();

            Console.WriteLine();
            Console.WriteLine("Closing service host...");
            serviceHost.Close();
            Console.WriteLine("Done.");
        }
    }
}
