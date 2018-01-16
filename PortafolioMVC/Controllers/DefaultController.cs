using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PortafolioMVC.App_Start;

namespace PortafolioMVC.Controllers
{
    public class DefaultController : Controller
    {

        private Usuario usuario = new Usuario();
        // GET: Default
        public ActionResult Index()
        {
            return View(usuario.Obtener(FronOfficeStartUp.UsuarioVisualizando(), true));
        }
    }
}