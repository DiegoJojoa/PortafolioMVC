﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortafolioMVC.App_Start
{
    public class FronOfficeStartUp
    {
        public static int UsuarioVisualizando()
        {
            int usuario_id_por_defecto = 1;
            string usuario_id = HttpContext.Current.Request.QueryString["id"];

            return usuario_id != null ? Convert.ToInt32(usuario_id) : usuario_id_por_defecto;
        }

    }
}