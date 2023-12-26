namespace SMPT.Client.Services
{
    public interface ILoginService
    {
        Task<string?> Login(int code, string password);
    }
}
