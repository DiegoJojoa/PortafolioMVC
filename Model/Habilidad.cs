namespace Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;

    [Table("Habilidad")]
    public partial class Habilidad
    {
        public int id { get; set; }

        public int usuario_id { get; set; }

        [StringLength(50)]
        public string nombre { get; set; }

        [StringLength(50)]
        public string dominio { get; set; }

        public virtual Usuario Usuario { get; set; }

        public int Conteo()
        {
            using(var ctx=new ProyectoContext()) {
                return ctx.Habilidad.Count();
            }
        }
    }
}
