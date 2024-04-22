using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Smartwyre.DeveloperTest.Data;
using Smartwyre.DeveloperTest.Services;
using Smartwyre.DeveloperTest.Types;
using System;

namespace Smartwyre.DeveloperTest.Runner;

class Program
{
    static void Main(string[] args)
    {
        var host = CreateHostBuilder(args).Build();

        // Running code using DI
        var service = host.Services.GetRequiredService<IRebateService>();

        Console.WriteLine("Calling service");
        Console.WriteLine();

        var request = new CalculateRebateRequest { ProductIdentifier = "P1", RebateIdentifier = "R1", Volume = 1 };
        var result = service.Calculate(request);

        var resultJson = System.Text.Json.JsonSerializer.Serialize(result);
        Console.WriteLine($"Results: {resultJson}");
        Console.WriteLine();

        Console.WriteLine("Press enter to end the program.");
        Console.ReadLine();
    }

    static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureServices((_, services) =>
            {
                services.AddTransient<IProductDataStore, ProductDataStore>();
                services.AddTransient<IRebateDataStore, RebateDataStore>();

                services.AddTransient<IRebateService, RebateService>();
                services.AddTransient<IRebateStrategyCalculationService, StaticRebateStrategyCalculationService>();

            });
}
