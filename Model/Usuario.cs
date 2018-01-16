namespace Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Data.Entity.Spatial;
    using System.Data.Entity.Validation;
    using System.IO;
    using System.Linq;
    using System.Web;
    using Helper;

    [Table("Usuario")]
    public partial class Usuario
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Usuario()
        {
            Experiencia = new HashSet<Experiencia>();
            Habilidad = new HashSet<Habilidad>();
            Testimonio = new HashSet<Testimonio>();
        }

        public int id { get; set; }

        [Required]
        [StringLength(50)]
        public string nombre { get; set; }

        [Required]
        [StringLength(50)]
        public string apellido { get; set; }

        [Required]
        [StringLength(50)]
        public string email { get; set; }

        [Required]
        [StringLength(32)]
        public string password { get; set; }

        [Column(TypeName = "text")]
        public string direccion { get; set; }

        [StringLength(50)]
        public string ciudad { get; set; }

        public int? pais_id { get; set; }

        [StringLength(100)]
        public string telefono { get; set; }

        [StringLength(100)]
        public string facebook { get; set; }

        [StringLength(100)]
        public string twitter { get; set; }

        [StringLength(100)]
        public string youtube { get; set; }

        [StringLength(50)]
        public string foto { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Experiencia> Experiencia { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Habilidad> Habilidad { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Testimonio> Testimonio { get; set; }

        public ResponseModel Acceder(string email, string password)
        {
            var rm = new ResponseModel();

            try
            {
                //Abrimos la conexión
                using (var ctx = new ProyectoContext())
                {
                    password = HashHelper.MD5(password);
                    var usuario = ctx.Usuario.Where(x => x.email == email)
                                             .Where(x => x.password == password)
                                             .SingleOrDefault();
                    if (usuario!=null)
                    {
                        SessionHelper.AddUserToSession(usuario.id.ToString());
                        rm.SetResponse(true);
                    }
                    else
                    {
                        rm.SetResponse(false, "Correo o contraseña incorrecta");
                    }
                }                   
            }
            catch (Exception)
            {

                throw;
            }

            return rm;
        }

        public Usuario Obtener(int id, bool includes=false)
        {
            var usuario = new Usuario();
            try
            {
                using(var ctx=new ProyectoContext())
                {
                    if (!includes)
                    {
                        usuario = ctx.Usuario.Where(x => x.id == id).SingleOrDefault();
                    }
                    else
                    {
                        usuario = ctx.Usuario.Include("Experiencia")
                                             .Include("Habilidad")
                                             .Include("Testimonio")
                            .Where(x => x.id == id).SingleOrDefault();

                    }             
                }
            }
            catch (Exception)
            {

                throw;
            }

            return usuario;
        }


        public ResponseModel Guardar(HttpPostedFileBase foto)
        {
            var rm = new ResponseModel();
            try
            {
                using (var ctx = new ProyectoContext())
                {
                    //Decirle al contexto actual que las validaciones cuando se graben
                    //esten deshabilitadas
                    ctx.Configuration.ValidateOnSaveEnabled = false;

                    var eUsuario = ctx.Entry(this);
                    eUsuario.State = EntityState.Modified;

                    if (foto != null)
                    {
                        //Nombre del archivo, es decir, lo renombramos para que no se repita nunca
                        string archivo = DateTime.Now.ToString("yyyyMMddHHmmss") + Path.GetExtension(foto.FileName);
                        //La ruta donde la vamos a guardar
                        foto.SaveAs(HttpContext.Current.Server.MapPath("~/Uploads/" + archivo));
                        //Establecemos en nuestro modelo el nombre del archivo
                        this.foto = archivo;
                    }
                    else
                    {
                        eUsuario.Property(x => x.foto).IsModified = false;
                    }

                    //Campos que se quiere ignorar, en este caso el password
                    if (this.password==null) eUsuario.Property(x => x.password).IsModified = false;
                    ctx.SaveChanges();

                    rm.SetResponse(true);
                }
            }
            catch(DbEntityValidationException e)
            {
                throw;
            }
            catch (Exception)
            {

                throw;
            }

            return rm;
        }

    }
}
