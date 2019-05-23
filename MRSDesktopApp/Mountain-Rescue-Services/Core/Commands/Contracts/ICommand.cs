using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mountain_Rescue_Services.Core.Commands.Contracts
{
    public interface ICommand
    {
        string Execute(string[] args);
    }
}
