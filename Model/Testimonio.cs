namespace Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Testimonio")]
    public partial class Testimonio
    {
        public int id { get; set; }

        public int usuario_id { get; set; }

        [StringLength(50)]
        public string ip { get; set; }

        [Required]
        [StringLength(50)]
        public string nombre { get; set; }

        [StringLength(200)]
        public string comentario { get; set; }

        [Required]
        [StringLength(50)]
        public string fecha { get; set; }

        public virtual Usuario Usuario { get; set; }
    }
}
