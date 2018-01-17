using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PortafolioMVC.ViewsModels
{
    public class ContactoViewModel
    {
        [Required]
        public string nombre { get; set; }
        [Required]
        [EmailAddress]
        public string correo { get; set; }
        [Required]
        public string mensaje { get; set; }
    }
}