using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.AzureAppConfiguration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;


namespace WebBMI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
             Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                }).ConfigureAppConfiguration((ctx, builder) =>
                {
                    var settings = builder.Build();

                    // This section can be used to pull feature flag configuration from Azure App Configuration
                    builder.AddAzureAppConfiguration(o =>
                    {
                        o.Connect("Endpoint=https://___________.azconfig.io;Id=tyC/;Secret=_________________________");
                        o.Select(KeyFilter.Any);
                        o.UseFeatureFlags(FeatureFlagconfig =>
                        {
                            FeatureFlagconfig.CacheExpirationInterval = TimeSpan.FromSeconds(1);
                        });
                    });
                });
    }
}
