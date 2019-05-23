using Mountain_Rescue_Services.Core.Commands.Contracts;
using Mountain_Rescue_Services.Core.Contracts;
using System;
using System.Linq;
using System.Reflection;

namespace Mountain_Rescue_Services.Core
{
    public class CommandInterpreter : ICommandInterpreter
    {
        private readonly IServiceProvider provider;

        private const string Suffix = "Command";
        public CommandInterpreter(IServiceProvider provider)
        {
            this.provider = provider;
        }

        public string Read(string[] inputArgs, object obj)
        {
            string command = inputArgs[0] + Suffix;
            string[] commandParams = inputArgs.Skip(1).ToArray();

            var type = Assembly.GetCallingAssembly()
                .GetTypes()
                .FirstOrDefault(x => x.Name == command);

            if (type == null)
            {
                throw new ArgumentException("Invalid Command");
            }

            //Get the constructor
            //Get the constructor params
            //Get services



            var constructor = type.GetConstructors()
                .FirstOrDefault();

            var constructorParams = constructor
                .GetParameters()
                .Select(x => x.ParameterType)
                .ToArray();

            var services = constructorParams
                .Select(this.provider.GetService)
                .ToArray();

            var currentCommand = (ICommand)constructor.Invoke(services);

            var result = currentCommand.Execute(commandParams);

            return result;
        }
    }
}
