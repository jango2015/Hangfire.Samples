using System;
//using Hangfire.SqlServer;
using Hangfire.Mongo;
using Microsoft.Owin;
using Owin;


[assembly: OwinStartup(typeof(Hangfire.ConsoleApplication.Startup))]

namespace Hangfire.ConsoleApplication
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseErrorPage();
            app.UseWelcomePage("/");

            //GlobalConfiguration.Configuration.UseSqlServerStorage(
            //    "DefaultConnection",
            //    new SqlServerStorageOptions { QueuePollInterval = TimeSpan.FromSeconds(1) });
            GlobalConfiguration.Configuration.UseMongoStorage("mongodb://localhost", "HanfireConsoleDemo");
            app.UseHangfireDashboard();
            app.UseHangfireServer();

            RecurringJob.AddOrUpdate(
                () => Console.WriteLine("{0} Recurring job completed successfully!", DateTime.Now.ToString()), 
                Cron.Minutely);
        }
    }
}
