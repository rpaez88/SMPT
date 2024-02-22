using System.Text.Json;

namespace SMPT.Client.Services
{
    public interface ILoginService
    {
        Task<string> Login(long code, string password);
    }
}
