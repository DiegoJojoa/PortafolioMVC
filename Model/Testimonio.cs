namespace Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Data.Entity.Spatial;
    using System.Linq;

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

        public Testimonio Obtener(int id)
        {
            var testimonio = new Testimonio();
            try
            {
                using (var ctx = new ProyectoContext())
                {
                    testimonio = ctx.Testimonio.Where(x => x.id == id).SingleOrDefault();
                }
            }
            catch (Exception)
            {

                throw;
            }

            return testimonio;
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

        public AnexGRIDResponde Listar(AnexGRID grid, int usuario_id)
        {
            try
            {
                using (var ctx = new ProyectoContext())
                {

                    ctx.Configuration.LazyLoadingEnabled = false;
                    grid.Inicializar();



                    var query = ctx.Testimonio.Where(x => x.usuario_id == usuario_id);

                    if (grid.columna == "id")
                    {
                        query = grid.columna_orden == "DESC" ? query.OrderByDescending(x => x.id) : query.OrderBy(x => x.id);
                    }

                    if (grid.columna == "nombre")
                    {
                        query = grid.columna_orden == "DESC" ? query.OrderByDescending(x => x.nombre) : query.OrderBy(x => x.nombre);
                    }

                    if (grid.columna == "ip")
                    {
                        query = grid.columna_orden == "DESC" ? query.OrderByDescending(x => x.ip) : query.OrderBy(x => x.ip);
                    }

                    if (grid.columna == "fecha")
                    {
                        query = grid.columna_orden == "DESC" ? query.OrderByDescending(x => x.fecha) : query.OrderBy(x => x.fecha);
                    }


                    var testimonios = query.Skip(grid.pagina).Take(grid.limite).ToList();

                    var total = query.Count();

                    grid.SetData(testimonios, total);

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
