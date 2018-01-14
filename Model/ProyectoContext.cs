namespace Model
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class ProyectoContext : DbContext
    {
        public ProyectoContext()
            : base("name=ProyectoContext")
        {
        }

        public virtual DbSet<Experiencia> Experiencia { get; set; }
        public virtual DbSet<Habilidad> Habilidad { get; set; }
        public virtual DbSet<Testimonio> Testimonio { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            

            modelBuilder.Entity<Usuario>()
                .HasMany(e => e.Experiencia)
                .WithOptional(e => e.Usuario)
                .HasForeignKey(e => e.usuario_id);

            modelBuilder.Entity<Usuario>()
                .HasMany(e => e.Habilidad)
                .WithRequired(e => e.Usuario)
                .HasForeignKey(e => e.usuario_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Usuario>()
                .HasMany(e => e.Testimonio)
                .WithRequired(e => e.Usuario)
                .HasForeignKey(e => e.usuario_id)
                .WillCascadeOnDelete(false);
        }
    }
}
