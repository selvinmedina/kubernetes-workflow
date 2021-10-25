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
    public class ConexionApiTest07Controller : ControllerBase
    {
        private readonly RestClient _restClient;

        public ConexionApiTest07Controller(IConfiguration configuration)
        {
            _restClient = new RestClient();

            var test07Api = configuration["api:Test07"];

            _restClient.BaseUrl = new Uri(test07Api);
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerSaludo()
        {
            try
            {
                var request = new RestRequest("ObtenerSaludoDesdeOtraApi", Method.GET);

                var respuesta = await _restClient.ExecuteGetAsync(request);

                return Ok(respuesta.Content);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, JsonConvert.SerializeObject(ex));
            }
        }
    }
}
