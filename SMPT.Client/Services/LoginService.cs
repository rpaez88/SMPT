using SMPT.Entities.Dtos;
using SMPT.Entities.Dtos.User;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;

namespace SMPT.Client.Services
{
    public class LoginService : ILoginService
    {
        private readonly HttpClient _http;

        public UserDto? User { get; set; }

        public LoginService(HttpClient http)
        {
            _http = http ?? throw new ArgumentNullException(nameof(http));
        }

        public async Task<string> Login(long code, string password)
        {
            var credentials = new SiiauCredentials { Codigo = code, Pass = password };

            var result = await _http.PostAsJsonAsync("login", credentials);
            var resp = await result.Content.ReadFromJsonAsync<ApiResponse>();

            if (resp?.StatusCode == HttpStatusCode.OK)
            {
                return resp.Data.ToString();
            }
            
            throw new Exception(resp?.ErrorMessage[0].Value);
        }

    }
}
