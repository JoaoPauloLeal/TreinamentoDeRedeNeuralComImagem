using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IAg1.Models;

namespace IAg1.Controllers
{
    public class TesteController : Controller
    {
        // GET: Teste
        public ActionResult Teste()
        {
            Binarios.testando();
            ViewBag.matriz = Binarios.matriz1;
            return View();
        }
    }
}