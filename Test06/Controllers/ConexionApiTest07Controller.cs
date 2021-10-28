﻿using Microsoft.AspNetCore.Mvc;
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

        [HttpGet("Saludar")]
        public IActionResult Saludar()
        {
            return Ok("Hola desde Test 06");
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
                Console.WriteLine(JsonConvert.SerializeObject(ex));
                return StatusCode((int)HttpStatusCode.InternalServerError, JsonConvert.SerializeObject(ex));
            }
        }
    }
}
