using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace DotNetCore
{
    public class Program
    {
        enum AppType
        {
            ConsoleApplication = 0, //Run using application
            WebAPI = 0 //Run Using IISExpress
        }
        static AppType appType = AppType.WebAPI;

        public static void Main(string[] args)
        {
            if (appType == AppType.WebAPI)
            {
                //which runs the web application and it begins listening for incoming HTTP requests.
                CreateWebHostBuilder(args).Build().Run();
            }
            else
            {
                DotNetCore.OOPS.Dynamic.DynamicMethod();

                Console.Read();

                Console.Write("Welcome to Console application");
                Console.ReadKey();
            }
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
