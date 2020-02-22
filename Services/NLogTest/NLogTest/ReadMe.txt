22 Feb 2020
===========

Try NLog

ref: https://github.com/NLog/NLog/wiki/Getting-started-with-ASP.NET-Core-3

(#) Visual Studio 2019
	-> New Project -> ASP.NET Core Web Application
	-> API -> No Docker/No Auth/No HTTPS

Note: This comes with a default logger, by class Microsoft.Extensions.Logging.Logger
However, we want to try NLog's logger, i.e. NLog.Logger

(#) Nuget -> Install -> NLog.Web.AspNetCore
	Nuget -> Install -> NLog

(#) Create nlog.config (lowercase all) file in the root of your project.
	File Properties -> Ensure 
	-> Build Action = Content (default)
	-> Copy to output folder = Copy if newer (default)

(#) In Program.cs, add UseNLog to HostBuilder like so:
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
            .UseNLog(); // You need to add: using NLog.Web;
    If you miss adding this, your program will continue using the Microsoft logger
    You can also obtain a logger by calling var loggerTemp = LogManager.GetLogger("foo");
    And this will also return you a NLog.Logger (LogManager comes from NLog DLL)

(#) After adding UseNLog(), the ASP.NET DI system will return NLog.Logger when you ask for ILogger<T>

That's it !

Explore more about the nlog.config file on Google .. by and large there are targets and rules



