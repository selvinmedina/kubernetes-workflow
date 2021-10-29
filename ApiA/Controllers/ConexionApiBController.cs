using Farsiman.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Test06.Controllers
{
    [ApiController]
    [Route("ObtenerSaludoDesdeOtraApi")]
    public class ConexionApiBController : ControllerBase
    {
        private readonly RestClient _restClient;

        public ConexionApiBController(IConfiguration configuration)
        {
            _restClient = new RestClient();

            var test07Api = configuration.GetFromENV("Api:B");

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

            Console.WriteLine($"Api: {test07Api}");

            _restClient.BaseUrl = new Uri(test07Api);
        }

        [HttpGet("Saludar")]
        public IActionResult Saludar()
        {
            return Ok("Hola desde Api A");
        }


        [HttpGet]
        public async Task<IActionResult> ObtenerSaludo()
        {
            try
            {
                Console.WriteLine($"Obteniendo Saludo: {_restClient.BaseUrl}ObtenerSaludoDesdeOtraApi/Saludar");
                
                var request = new RestRequest("ObtenerSaludoDesdeOtraApi/Saludar", Method.GET);
                var respuesta = await _restClient.ExecuteGetAsync(request);
                
                Console.WriteLine(respuesta.StatusCode);
                
                return Ok(respuesta.Content);
            }
            catch (Exception ex)
            {
                Console.WriteLine(JsonConvert.SerializeObject(ex));
                return StatusCode((int)HttpStatusCode.InternalServerError, JsonConvert.SerializeObject(ex));
            }
        }
    }
}
