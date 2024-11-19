namespace Gateway.API.Interfaces
{
    public interface IFmsProxy
    {
        Task<IResult> CheckCredentials(string email, string password);
    }
}
