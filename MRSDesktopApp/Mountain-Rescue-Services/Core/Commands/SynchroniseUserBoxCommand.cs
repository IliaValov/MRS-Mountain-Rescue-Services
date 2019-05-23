using Mountain_Rescue_Services.Core.Commands.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mountain_Rescue_Services.Core.Commands
{
    public class SynchroniseUserBoxCommand : ICommand
    {
        private readonly object obj;

        public SynchroniseUserBoxCommand(object obj)
        {
            this.obj = obj;
        }

        public string Execute(string[] args)
        {
            throw new NotImplementedException();
        }
    }
}
