using Dominio_Interfaces.EnitdadesNegocio;
using Dominio_Interfaces.EntidadesAuxiliares;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.ContextoEF
{
    public class LibreriaContexto:DbContext
    {
        public DbSet<Mantenimiento> Mantenimientos { get; set; }
        public DbSet<Cabaña> Cabañas { get; set; }
        public DbSet<TipoCabaña> TiposCabañas{ get; set; }
        public DbSet<Parametros> Parametros { get; set; }

        public DbSet<Usuario>Usuarios{ get; set; }
        public DbSet<AlquilerCabaña>AlquileresCabañas { get; set; }
        public LibreriaContexto(DbContextOptions<LibreriaContexto>options):base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           
            //VALUE OBJECTS DE CABAÑA
            modelBuilder.Entity<Cabaña>().OwnsOne(cab => cab.Nombre, nomVO => nomVO.HasIndex(nomVO => nomVO.valor).IsUnique());
            modelBuilder.Entity<Cabaña>().OwnsOne(cab => cab.Descripcion);

            //VALUE OBJECTS DE USUARIO
            modelBuilder.Entity<Usuario>().OwnsOne(usu => usu.Email, emailVO => emailVO.HasIndex(emailVO => emailVO.Valor).IsUnique());
            modelBuilder.Entity<Usuario>().OwnsOne(usu => usu.Contraseña);
            modelBuilder.Entity<Usuario>().OwnsOne(usu => usu.Nombre);
            modelBuilder.Entity<Usuario>().OwnsOne(usu => usu.Apellido);
            modelBuilder.Entity<Usuario>().OwnsOne(usu => usu.Rol);



            base.OnModelCreating(modelBuilder);
        }
    }
}
