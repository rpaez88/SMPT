using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SMPT.Shared.DTO;
using SMPT.Server.Controllers;
using System.Net;
using System.Net.Http.Headers;
using System.Text.Json.Nodes;
using Microsoft.AspNetCore.Cors;
using System.Net;

namespace SMPT.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private static readonly HttpClient client = new HttpClient();
        private readonly ILogger<LoginController> _logger;

        public LoginController(ILogger<LoginController> logger)
        {
            _logger = logger;
        }


        // GET: api/<LoginController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<LoginController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<LoginController>
        [HttpPost]
        public async Task<CustomResponse<JsonObject>> Post([FromBody] SiiauCredentialsDTO credentials)
        {
            var respApi = new CustomResponse<JsonObject>();

            respApi.StatusCode = (int)HttpStatusCode.BadRequest;
            respApi.Message = "Provided credentials error";
            respApi.Value = null;

            if (credentials != null)
            {
                if (!string.IsNullOrWhiteSpace(credentials.pass))
                {
                    if (client.BaseAddress == null)
                    {
                        client.BaseAddress = new Uri("http://148.202.89.11/d_alum/api/");
                    }
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage resp = await client.PostAsJsonAsync("siiau-validate", credentials);
                    var content = await resp.Content.ReadFromJsonAsync<JsonObject>();
                    if (resp.StatusCode == HttpStatusCode.OK)
                    {

                        if (content != null && !content.ContainsKey("respuesta"))
                        {
                            respApi.StatusCode = (int)HttpStatusCode.OK;
                            respApi.Message = "Login success";
                            respApi.Value = content;
                        }
                        else
                        {
                            respApi.StatusCode = (int)HttpStatusCode.NotFound;
                            respApi.Message = "Login fail";
                            respApi.Value = content;
                        }
                    }
                    else
                    {
                        respApi.StatusCode = (int)HttpStatusCode.ServiceUnavailable;
                        respApi.Message = "CUValles auth server error";
                        respApi.Value = null;
                    }
                }
            }
            
            return respApi;
        }
    }
}
