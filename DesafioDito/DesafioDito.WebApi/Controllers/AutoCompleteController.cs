using System;
using System.Linq;
using DesafioDito.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DesafioDito.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AutoCompleteController : Controller
    {
        readonly AutoCompleteService autoCompleteService;
        ILogger<string> log;

        public AutoCompleteController()
        {
            autoCompleteService = new AutoCompleteService();
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

        [Produces("application/json")]
        [HttpGet("buscar")]
        public IActionResult Buscar()
        {
            
            string search = HttpContext.Request.Query["term"].ToString();
            var listaEventos = autoCompleteService.ListarEventos();
            var names = listaEventos.Where(t => t.Nome.Contains(search)).Select(s => s.Nome).ToList();

            return Ok(names);
        }

        [HttpGet]
        [Route("Index")]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("SalvarEvento")]
        public ActionResult SalvarEvento(string nome)
        {
            try
            {
                log.LogInformation("Salvando evento no banco de dados");
                if (nome != null)
                {
                    DateTime timestamp = DateTime.Now;
                    autoCompleteService.InserirEvento(nome, timestamp);
                    return Ok("Evento cadastrado com sucesso");
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            catch (Exception ex)
            {
                log.LogError($"Ocorreu um erro ao salvar no banco de dados: {ex}");
                return null;
            }
        }

        [HttpGet]
        [Route("MostrarEvento")]
        public JsonResult MostrarEvento(string nome)
        {
            try
            {

                if (nome != null)
                {
                    log.LogInformation($"Obtendo evento(s) do banco de dados");
                    var x = autoCompleteService.ObterEventos(nome);
                    
                    return Json(new { EventoViewModel = x });
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                log.LogError($"Ocorreu um erro ao buscar evento no banco de dados: {ex}");
                return null;
            }
        }

    }
}