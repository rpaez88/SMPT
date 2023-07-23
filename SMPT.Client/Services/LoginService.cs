using SMPT.Shared;
using SMPT.Shared.DTO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json.Nodes;

namespace SMPT.Client.Services
{
    public class LoginService : ILoginService
    {
        private readonly HttpClient _http;

        public LoginService(HttpClient http)
        {
            _http = http ?? throw new ArgumentNullException(nameof(http));
        }

        public async Task<JsonObject?> Login(int code, string password)
        {
            var credentials = new SiiauCredentialsDTO { codigo = code, pass = password };

            var result = await _http.PostAsJsonAsync("api/login", credentials);
            var resp = await result.Content.ReadFromJsonAsync<CustomResponse<JsonObject?>>();

            if (resp != null && resp.StatusCode == (int)HttpStatusCode.OK)
                return resp?.Value;
            else
                throw new Exception(resp?.Message);
        }
    }
}
