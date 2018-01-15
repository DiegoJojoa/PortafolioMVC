using Helper;
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
    public class HabilidadesController : Controller
    {

        private Habilidad Habilidad = new Habilidad();
        // GET: Admin/Habilidades
        public ActionResult Index()
        {
            return View();
        }


        public JsonResult Listar(AnexGRID grid)
        {
            return Json(Habilidad.Listar(grid, SessionHelper.GetUser()));
        }

        public ActionResult Crud( int id = 0)
        {
            if (id == 0) Habilidad.usuario_id = SessionHelper.GetUser(); //GetUser me trae el id del usuario correcto
            
            else Habilidad = Habilidad.Obtener(id);

            return View(Habilidad);
        }


        public JsonResult Guardar(Habilidad model)
        {
            var rm = new ResponseModel();

            if (ModelState.IsValid)
            {
                rm = model.Guardar();
                if (rm.response)
                {
                    rm.href = Url.Content("~/Admin/Habilidades/");
                }
            }

            return Json(rm);
        }


        public JsonResult Eliminar(int id)
        {

            return Json(Habilidad.Eliminar(id));
        }
    }
}