using Farsiman.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Net;
using System.Threading.Tasks;
using System.Linq;

namespace Test07.Controllers
{
    [ApiController]
    [Route("ObtenerSaludoDesdeOtraApi")]
    public class ConexionApiTest06Controller : ControllerBase
    {
        private readonly RestClient _restClient;

        public ConexionApiTest06Controller(IConfiguration configuration)
        {
            _restClient = new RestClient();

            var test06Api = configuration.GetFromENV("Api:Test06");

            var variables = Environment.GetEnvironmentVariables();

            foreach (string key in variables.Keys)
            {
                Console.WriteLine(key);
                if (!key.StartsWith("FS_"))
                {
                    continue;
                }

                Console.WriteLine();
                Console.WriteLine(key);

                Console.WriteLine(variables[key].ToString());
            }

            Console.WriteLine($"Api: {test06Api}");

            _restClient.BaseUrl = new Uri(test06Api);
        }

        [HttpGet("Saludar")]
        public IActionResult Saludar()
        {
            return Ok("Hola desde Test 07");
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerSaludo()
        {
            try
            {
                Console.WriteLine($"Obteniendo Saludo: {_restClient.BaseUrl}ObtenerSaludoDesdeOtraApi/Saludar");
                var request = new RestRequest("ObtenerSaludoDesdeOtraApi/Saludar", Method.GET);
                //Console.WriteLine(JsonConvert.SerializeObject(request));
                var respuesta = await _restClient.ExecuteGetAsync(request);
                Console.WriteLine(respuesta.StatusCode);
                return Ok(respuesta.Content);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, JsonConvert.SerializeObject(ex));
            }
        }
    }
}
