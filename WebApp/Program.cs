using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data;
using Data.Entities.Identity;
using Data.Initialize;
using Logging;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;

namespace WebApp
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration().BuildLoggerConfiguration();

            try
            {
                CreateHostBuilder(args).Build().Run();
                Log.Information("Starting up");
                //var host = CreateHostBuilder(args)
                //    .Build();

                //2. Find the service layer within our scope.
                //using (var scope = host.Services.CreateScope())
                //{
                //    //3. Get the instance of BoardGamesDBContext in our services layer

                //    var services = scope.ServiceProvider;
                //    var context = services.GetRequiredService<ApplicationDbContext>();
                //    var userManager = services.GetRequiredService<UserManager<User>>();
                //    var roleManager = services.GetRequiredService<RoleManager<Role>>();

                //    //4. Call the DataGenerator to create sample data

                //    await Initialize.SeedAsync(context, userManager: userManager, roleManager).ConfigureAwait(false);

                //}
                //Continue to run the application
               // host.Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Application start-up failed");
                Log.CloseAndFlush();
            }

        }

        //public static IHostBuilder CreateHostBuilder(string[] args) =>
        //    Host.CreateDefaultBuilder(args)
        //        .ConfigureAppConfiguration((hostingContext, config) =>
        //        {
        //            config.AddJsonFile("emailSettings.json", false, true);
        //            config.AddCommandLine(args);
        //        })
        //        .UseSerilog()
        //        .ConfigureWebHostDefaults(webBuilder =>
        //        {
        //            webBuilder.UseStartup<Startup>();
        //        });
        public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });
    }
}

