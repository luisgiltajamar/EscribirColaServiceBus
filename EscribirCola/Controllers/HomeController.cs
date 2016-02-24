using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EscribirCola.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(String info)
        {
            var cc = ConfigurationManager.AppSettings["conexion"];
            ManejadorCola.Instance.CrearCola(cc, "incidencias", 1024, 86400);
            var d=new Dictionary<String,String>()
            {
                { "incidencia",info},
                {"fecha",DateTime.Now.ToLongTimeString() }

            };

            ManejadorCola.Instance.Enviar(cc,"incidencias",d,"Incidencia");


            return RedirectToAction("Index");
        }
    }
}