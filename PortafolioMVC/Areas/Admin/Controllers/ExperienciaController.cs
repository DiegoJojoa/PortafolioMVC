using Model;
using PortafolioMVC.Areas.Admin.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PortafolioMVC.Areas.Admin.Controllers
{
    [Autenticado]
    public class ExperienciaController : Controller
    {
        private Experiencia experiencia = new Experiencia();
        // GET: Admin/Experiencia
        public ActionResult Index(int tipo)
        {
            ViewBag.tipo = tipo;
            ViewBag.Title = tipo == 1 ? "Trabajos realizados" : "Estudios previos";
            return View();
        }

        public ActionResult Crud (byte tipo, int id = 0)
        {
            if (id == 0)
            {
                experiencia.tipo = tipo;
            }
            else
            {

            }
            return View(experiencia);
        }
    }
}