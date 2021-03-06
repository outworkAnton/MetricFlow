﻿using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace MetricFlow.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseHealthChecks("/healthcheck")
                .UseUrls("http://*:5000")
                .UseStartup<Startup>();
    }
}