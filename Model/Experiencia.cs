namespace Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Experiencia")]
    public partial class Experiencia
    {
        public int id { get; set; }

        public int? usuario_id { get; set; }

        public byte tipo { get; set; }

        [StringLength(100)]
        public string nombre { get; set; }

        [Required]
        [StringLength(50)]
        public string titulo { get; set; }

        [Required]
        [StringLength(50)]
        public string desde { get; set; }

        [Required]
        [StringLength(50)]
        public string hasta { get; set; }

        [Column(TypeName = "text")]
        public string descripcion { get; set; }

        public virtual Usuario Usuario { get; set; }
    }
}
