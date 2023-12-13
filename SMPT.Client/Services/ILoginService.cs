using System.Text.Json.Nodes;

namespace SMPT.Client.Services
{
    public interface ILoginService
    {
        Task<string?> Login(int code, string password);
        JsonObject GetUser();
    }
}
