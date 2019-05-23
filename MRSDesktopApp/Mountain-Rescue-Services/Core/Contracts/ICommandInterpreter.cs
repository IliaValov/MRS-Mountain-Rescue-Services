namespace Mountain_Rescue_Services.Core.Contracts
{
    public interface ICommandInterpreter
    {
        string Read(string[] inputArgs, object obj);
    }
}
