using System;
using System.Collections.Generic;
using DesafioDito.Adapter.Model;
using DesafioDito.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DesafioDito.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ManipulacaoDadosController : Controller
    {
        readonly ManipulacaoDadosService manipulacaoDadosService;
        ILogger log;
        public ManipulacaoDadosController()
        {
            manipulacaoDadosService = new ManipulacaoDadosService();
            var loggerFactory = LoggerFactory.Create(builder =>
            {
                builder
                    .AddFilter("Microsoft", LogLevel.Warning)
                    .AddFilter("System", LogLevel.Warning)
                    .AddFilter("LoggingConsoleApp.Program", LogLevel.Debug)
                    .AddConsole()
                    .AddEventLog();
            });
            log = loggerFactory.CreateLogger<string>();
        }

        [HttpGet]
        public IEnumerable<EventoCompra> Index()
        {
            try
            {
                log.LogInformation("Buscando dados da Dito");
                return manipulacaoDadosService.ManipularDadosDito();
            }
            catch (Exception ex)
            {
                log.LogError($"Ocorreu um erro ao buscar dados da Dito: {ex}");
                return null;
            }
        }
    }
}