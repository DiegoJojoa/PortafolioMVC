namespace Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Data.Entity.Spatial;
    using System.Linq;
   

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


        public Experiencia Obtener(int id)
        {
            var experiencia = new Experiencia();
            try
            {
                using (var ctx = new ProyectoContext())
                {
                    experiencia = ctx.Experiencia.Where(x => x.id == id).SingleOrDefault();
                }
            }
            catch (Exception)
            {

                throw;
            }

            return experiencia;
        }


        public ResponseModel Guardar()
        {
            var rm = new ResponseModel();
            try
            {
                using (var ctx = new ProyectoContext())
                {
                    if (this.id > 0) ctx.Entry(this).State = EntityState.Modified;
                    else ctx.Entry(this).State = EntityState.Added;

                    ctx.SaveChanges();
                    rm.SetResponse(true);
                }
            }

            catch (Exception)
            {

                throw;
            }

            return rm;
        }


        public ResponseModel Eliminar(int id)
        {
            var rm = new ResponseModel();
            try
            {
                using (var ctx = new ProyectoContext())
                {
                    this.id = id;
                    ctx.Entry(this).State = EntityState.Deleted;

                    ctx.SaveChanges();
                    rm.SetResponse(true);
                }
            }

            catch (Exception)
            {

                throw;
            }

            return rm;
        }

        public AnexGRIDResponde Listar(AnexGRID grid, int tipo)
        {
            try
            {
                using (var ctx = new ProyectoContext())
                {

                    ctx.Configuration.LazyLoadingEnabled = false;
                    grid.Inicializar();

                    

                    var query = ctx.Experiencia.Where(x => x.tipo == tipo);  //expresión lambda
                    //sintaxis de una expresión lambda (parameters)=>expression-or-statement-block
                    //ej: x=>x*x

                    //ordenamiento
                    //id, titulo, desde, hasta
                    if (grid.columna == "id")
                    {
                        query = grid.columna_orden == "DESC" ? query.OrderByDescending(x => x.id) : query.OrderBy(x => x.id);
                    }

                    if (grid.columna == "nombre")
                    {
                        query = grid.columna_orden == "DESC" ? query.OrderByDescending(x => x.nombre) : query.OrderBy(x => x.nombre);
                    }

                    if (grid.columna == "titulo")
                    {
                        query = grid.columna_orden == "DESC" ? query.OrderByDescending(x => x.titulo) : query.OrderBy(x => x.titulo);
                    }

                    if (grid.columna == "desde")
                    {
                        query = grid.columna_orden == "DESC" ? query.OrderByDescending(x => x.desde) : query.OrderBy(x => x.desde);
                    }

                    if (grid.columna == "hasta")
                    {
                        query = grid.columna_orden == "DESC" ? query.OrderByDescending(x => x.hasta) : query.OrderBy(x => x.hasta);
                    }

                    var experiencias = query.Skip(grid.pagina).Take(grid.limite).ToList();

                    var total = query.Count();

                    grid.SetData(experiencias, total);

                }
            }
            catch (Exception E)
            {

                throw;
            }

            return grid.responde();
           
        }

    }
}
    

