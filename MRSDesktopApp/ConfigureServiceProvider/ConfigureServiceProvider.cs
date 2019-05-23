using Microsoft.Extensions.DependencyInjection;
using Mountain_Rescue_Services.Core;
using Mountain_Rescue_Services.Core.Contracts;
using System;

namespace ConfigureServiceProvider
{
    public class ConfigureServiceProvider
    {
        public static IServiceProvider ConfigureServices()
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddTransient<ICommandInterpreter, CommandInterpreter>();

            serviceCollection.AddTransient<Mapper>();

            var serviceProvider = serviceCollection.BuildServiceProvider();

            return serviceProvider;
        }
    }
}
