using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Web;
using System;

namespace DotNetCore
{
    public class Program
    {
        enum EnumAppType
        {
            ConsoleApplication = 0, //Run using application
            WebAPI = 1 //Run Using IISExpress
        }
        static EnumAppType AppType = EnumAppType.ConsoleApplication;

        public static void Main(string[] args)
        {
            // NLog: setup the logger first to catch all errors
            var logger = NLog.Web.NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
            try
            {
                logger.Debug("Init Main");

                if (AppType == EnumAppType.WebAPI)
                {
                    //which runs the web application and it begins listening for incoming HTTP requests.
                    CreateWebHostBuilder(args).Build().Run();
                }
                else
                {
                    Console.Write("Welcome to Console application");
                    RunConsole(); 
                    Console.ReadKey();
                }
            }
            catch (Exception ex)
            {
                //NLog: catch setup errors
                logger.Error(ex, "Stopped program because of exception");
                throw;
            }
            finally
            {
                // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
                NLog.LogManager.Shutdown();
            }
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .ConfigureLogging(logging =>
                {
                    logging.ClearProviders();
                    logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
                })
                .UseNLog();  // NLog: setup NLog for Dependency injection

        public static void RunConsole()
        {
            //DotNetCore.OOPS.Dynamic.DynamicMethod();
        }
    }
}
