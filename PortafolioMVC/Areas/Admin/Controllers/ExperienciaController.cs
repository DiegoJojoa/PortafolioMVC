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
    public class ExperienciaController : Controller
    {
        private Experiencia experiencia = new Experiencia();
        // GET: Admin/Experiencia
        public ActionResult Index(int tipo=1)
        {
            ViewBag.tipo = tipo;
            ViewBag.Title = tipo == 1 ? "Trabajos realizados" : "Estudios previos";
            return View();
        }

        public JsonResult Listar (AnexGRID grid, int tipo)
        {
            return Json(experiencia.Listar(grid, tipo, SessionHelper.GetUser()));
        }

        public ActionResult Crud (byte tipo=0, int id = 0)
        {
            if (id == 0) {
                if (tipo == 0) return Redirect("~/Admin/Experiencia");
                experiencia.tipo = tipo;
                experiencia.usuario_id = SessionHelper.GetUser(); //GetUser me trae el id del usuario correcto
            }     
            else experiencia = experiencia.Obtener(id);
            
            return View(experiencia);
        }


        public JsonResult Guardar(Experiencia model)
        {
            var rm = new ResponseModel();

            if (ModelState.IsValid)
            {
                rm = model.Guardar();
                if (rm.response)
                {
                    rm.href = Url.Content("~/Admin/Experiencia/?tipo=" + model.tipo);
                }
            }

            return Json(rm);
        }


        public JsonResult Eliminar(int id)
        {
          
            return Json(experiencia.Eliminar(id));
        }
    }
}