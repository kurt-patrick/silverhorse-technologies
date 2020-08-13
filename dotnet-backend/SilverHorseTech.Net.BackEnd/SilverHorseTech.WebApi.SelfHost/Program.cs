using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin.Hosting;

namespace SilverHorseTech.WebApi.SelfHost
{
    class Program
    {
        static void Main(string[] args)
        {
            using (WebApp.Start<Startup>("http://localhost:12345"))
            {
                Console.WriteLine("Web server is running");
                Console.WriteLine("Press enter to quit");
                Console.ReadLine();
            }

        }
    }
}
