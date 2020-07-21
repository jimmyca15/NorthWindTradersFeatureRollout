using System;
using Azure.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace Northwind.BikeShop
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostContext, builder) => {

                    IConfiguration intermediate = builder.Build();

                    builder.AddAzureAppConfiguration(o => {

                        o.Connect(new Uri(intermediate["AppConfigurationEndpoint"]), new DefaultAzureCredential());

                        o.UseFeatureFlags();
                    });
                
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
