using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PortafolioMVC.App_Start;
using PortafolioMVC.ViewsModels;
using System.Net.Mail;
using Rotativa.MVC; //Descargar el puglins de nuget RotativaMVC

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

        public JsonResult EnviarCorreo(ContactoViewModel model)
        {
            var rm = new ResponseModel();


          

           
            if (ModelState.IsValid)
            {

                try
                {
                    var _usuario = usuario.Obtener(FronOfficeStartUp.UsuarioVisualizando());

                    var mail = new MailMessage();
                    mail.From = new MailAddress(model.correo,model.nombre);
                    mail.To.Add(_usuario.email);
                    mail.Subject = "Correo desde contacto";
                    mail.IsBodyHtml = true;
                    mail.Body = model.mensaje;

                    var SmtpServer = new SmtpClient("smpt.live.com"); //or "smtp.gmail.com"
                    SmtpServer.Port = 587;
                    SmtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;
                    SmtpServer.UseDefaultCredentials = false;
                    SmtpServer.Credentials = new System.Net.NetworkCredential("diegofernando-58@hotmail.com", "Contraseña");
                    SmtpServer.EnableSsl = true;
                    SmtpServer.Send(mail);

                }
                catch (Exception e)
                {

                    rm.SetResponse(false, e.Message);
                    return Json(rm);

                    throw;
                }


                rm.SetResponse(true);
                rm.function = "cerrarContacto();";


            }

            return Json(rm);
        }


        public ActionResult ExportarPDF()
        {
            return new ActionAsPdf("PDF");
        }

        public ActionResult PDF()
        {
            return View(
                usuario.Obtener(FronOfficeStartUp.UsuarioVisualizando(), true));
        }


    }
}