using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;

namespace DesafioDito.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Route("IrParaDesafio2")]
        public ActionResult IrParaDesafio2()
        {
            return RedirectToAction("Index", "ManipulacaoDados");
        }

        [HttpPost]
        [Route("IrParaDesafio1")]
        public ActionResult IrParaDesafio1()
        {
            return RedirectToAction("Index", "AutoComplete");
        }
    }
}