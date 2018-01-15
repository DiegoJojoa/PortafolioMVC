namespace Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
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


        public Habilidad Obtener(int id)
        {
            var habilidad = new Habilidad();
            try
            {
                using (var ctx = new ProyectoContext())
                {
                    habilidad = ctx.Habilidad.Where(x => x.id == id).SingleOrDefault();
                }
            }
            catch (Exception)
            {

                throw;
            }

            return habilidad;
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

        public AnexGRIDResponde Listar(AnexGRID grid,  int usuario_id)
        {
            try
            {
                using (var ctx = new ProyectoContext())
                {

                    ctx.Configuration.LazyLoadingEnabled = false;
                    grid.Inicializar();



                    var query = ctx.Habilidad.Where(x => x.usuario_id == usuario_id); 
                    
                    if (grid.columna == "id")
                    {
                        query = grid.columna_orden == "DESC" ? query.OrderByDescending(x => x.id) : query.OrderBy(x => x.id);
                    }

                    if (grid.columna == "nombre")
                    {
                        query = grid.columna_orden == "DESC" ? query.OrderByDescending(x => x.nombre) : query.OrderBy(x => x.nombre);
                    }

                    if (grid.columna == "dominio")
                    {
                        query = grid.columna_orden == "DESC" ? query.OrderByDescending(x => x.dominio) : query.OrderBy(x => x.dominio);
                    }

    
                    var habilidades = query.Skip(grid.pagina).Take(grid.limite).ToList();

                    var total = query.Count();

                    grid.SetData(habilidades, total);

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
