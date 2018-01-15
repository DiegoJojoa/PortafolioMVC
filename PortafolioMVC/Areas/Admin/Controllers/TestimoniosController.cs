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
    public class TestimoniosController : Controller
    {

        private Testimonio Testimonio = new Testimonio();

        // GET: Admin/Testimonios
        public ActionResult Index()
        {
            return View();
        }


        public JsonResult Listar(AnexGRID grid)
        {
            return Json(Testimonio.Listar(grid, SessionHelper.GetUser()));
        }

        public ActionResult Crud(int id)
        {
            return View(Testimonio.Obtener(id));
        }


        public JsonResult Guardar(Testimonio model)
        {
            var rm = new ResponseModel();

            if (ModelState.IsValid)
            {
                rm = model.Guardar();
                if (rm.response)
                {
                    rm.href = Url.Content("~/Admin/Testimonios/");
                }
            }

            return Json(rm);
        }


        public JsonResult Eliminar(int id)
        {

            return Json(Testimonio.Eliminar(id));
        }
    }
}