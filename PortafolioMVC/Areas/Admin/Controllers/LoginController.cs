using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Helper;
using PortafolioMVC.Areas.Admin.Filters;

namespace PortafolioMVC.Areas.Admin.Controllers
{
    
    public class LoginController : Controller
    {
        private Usuario usuario = new Usuario();
        // GET: Admin/Login
        [NoLogin]
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult Acceder(string email, string password)
        {
            var rm = usuario.Acceder(email, password);

            if (rm.response)
            {
                rm.href = Url.Content("~/Admin/Usuario/Index");
                //rm.href = Url.Content("http://localhost:56922/Admin/Usuario/Index");
            }

            return Json(rm);
        }

        public ActionResult Logout()
        {
            //Eliminar la sesion actual
            SessionHelper.DestroyUserSession();
            return Redirect("~/");
        }
    }
}