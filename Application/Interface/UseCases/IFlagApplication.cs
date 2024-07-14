namespace FlagX0.Web.Application.Interface.UseCases
{
    public interface IFlagApplication
    {
        Task<bool> Execute(string flagName, bool isActive);
    }
}
