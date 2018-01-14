using Model;
using PortafolioMVC.Areas.Admin.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Helper;

namespace PortafolioMVC.Areas.Admin.Controllers
{
    [Autenticado]
    public class UsuarioController : Controller
    {
        private Usuario usuario = new Usuario();

        // GET: Admin/Usuario
        public ActionResult Index()
        {
            return View(usuario.Obtener(SessionHelper.GetUser()));
        }

        public JsonResult Guardar(Usuario model, HttpPostedFileBase foto)
        {
            var rm = new ResponseModel();
            //Retiramos la validación de la propiedad password 
            ModelState.Remove("password");

            if (ModelState.IsValid)
            {
                rm = model.Guardar(foto);
            }

            return Json(rm);
        }
    }
}