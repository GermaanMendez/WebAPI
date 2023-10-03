using Datos.ContextoEF;
using Dominio_Interfaces.EnitdadesNegocio;
using Dominio_Interfaces.ExepcionesPropias;
using Dominio_Interfaces.InterfacesRepositorios;
using Dominio_Interfaces.ValueObjects.Usuario;
using ExcepcionesPropias;
using Microsoft.EntityFrameworkCore;
using NHibernate.Id.Insert;
using NHibernate.Persister.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Repositorios
{
    public class RepositorioAlquilerCabaña : IRepositorioAlquilerCabaña
    {
        public LibreriaContexto Contexto { get; set; }
        public RepositorioAlquilerCabaña(LibreriaContexto ctx)
        {
            Contexto = ctx;
        }


        public void Add(AlquilerCabaña obj)
        {
            try
            {
                if (obj != null)
                {
                    obj.AsignarPrecio();
                    obj.Validar();

                    var YaExisteAlquiler = Contexto.AlquileresCabañas.Where(alq => alq.Cabaña == obj.Cabaña && ((obj.FechaAlquilerDesde >=alq.FechaAlquilerDesde && obj.FechaAlquilerDesde<=alq.FechaAlquilerHasta)
                                                                                                                ||obj.FechaAlquilerHasta<=alq.FechaAlquilerHasta && obj.FechaAlquilerHasta>=alq.FechaAlquilerDesde)).FirstOrDefault();
                    if (YaExisteAlquiler == null)
                    {
                        Contexto.AlquileresCabañas.Add(obj);    
                        Contexto.SaveChanges();
                    }
                    else
                    {
                        throw new ExepcionesAlquileresCabaña("La cabaña no esta disponible para ser alquilada en ese rango de fechas");
                    }
                }
                else
                {
                    throw new ExepcionesAlquileresCabaña("Se debe ingresar un Alquiler no nulo");
                }
            }
            catch (ExcepcionesBaseDeDatos ex)
            {
                throw new ExcepcionesBaseDeDatos("Error al conectarse a la base de datos " + ex.Message);
            }
        }

        public bool EliminarAlquilerCabaña(string emailUsuario, int idAlquiler)
        {
            try
            {
                if (idAlquiler == null || idAlquiler < 1 || emailUsuario==null || emailUsuario.Length<1) throw new ExepcionesAlquileresCabaña("Credenciales para eliminar no validas");
                var usuario = ObtenerUsuarioPorEmail(emailUsuario);
                if (usuario == null) throw new ExepcionesAlquileresCabaña("No existe un usuario con las credenciales ingresadas");
                var alquilerEliminar = Contexto.AlquileresCabañas.Where(alq => alq.IdAlquiler == idAlquiler).FirstOrDefault();
                if (alquilerEliminar == null) throw new ExepcionesAlquileresCabaña("No un alquiler con las credenciales ingresadas");
                if (alquilerEliminar.Usuario == usuario)
                {
                    Contexto.AlquileresCabañas.Remove(alquilerEliminar);
                    Contexto.SaveChanges();
                    return true;
                }
                else
                {
                    throw new ExepcionesAlquileresCabaña("El usuario no tiene los permisos necesarios para eliminar el alquiler");
                }
            }
            catch (ExcepcionesBaseDeDatos ex)
            {
                throw new ExcepcionesBaseDeDatos("Error al conectarse a la base de datos " + ex.Message);
            }
        }

        public void EditarAlquilerCabaña(string emailUsuario, AlquilerCabaña obj)
        {
            try
            {
                if (obj == null || emailUsuario == null || emailUsuario.Length < 1) throw new ExepcionesAlquileresCabaña("Credenciales para editar no validas");
                var usuario = ObtenerUsuarioPorEmail(emailUsuario);
                if (usuario == null) throw new ExepcionesAlquileresCabaña("No existe un usuario con las credenciales ingresadas");
                if (obj.Usuario == usuario)
                {
                    obj.Validar();
                    Contexto.AlquileresCabañas.Update(obj);
                    Contexto.SaveChanges();
                }
                else
                {
                    throw new ExepcionesAlquileresCabaña("El usuario no tiene los permisos necesarios para editar el alquiler");
                }
            }
            catch (ExcepcionesBaseDeDatos ex)
            {
                throw new ExcepcionesBaseDeDatos("Error al conectarse a la base de datos " + ex.Message);
            }
        }
        public bool Remove(int id)
        {
            throw new NotImplementedException();
        }
        public void Update(AlquilerCabaña obj)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AlquilerCabaña> GetAll()
        {
            var resultado = Contexto.AlquileresCabañas.ToList();
            return resultado;
        }
        public AlquilerCabaña FindById(int id)
        {
            if(id==null || id<1) throw new ExepcionesAlquileresCabaña("Credenciales de busqueda no validas");
            var resultado = Contexto.AlquileresCabañas.Include(alq=>alq.Usuario).Include(alq=>alq.Cabaña).Where(alq=>alq.IdAlquiler==id).FirstOrDefault();
            return resultado;
        }
        public IEnumerable<AlquilerCabaña> ObtenerAlquileresDeMiCabaña(string emailUsuario, int idCabaña)
        {
            try
            {
                if(string.IsNullOrEmpty(emailUsuario) ||idCabaña<1) throw new ExepcionesAlquileresCabaña("Credenciales no validas");
                var cabañaBuscada = Contexto.Cabañas.Include(cab=>cab.Usuario).Where(cab => cab.NumeroHabitacion == idCabaña).FirstOrDefault();
                if (cabañaBuscada.Usuario.Email.Valor.ToLower()!=emailUsuario.ToLower()) throw new ExepcionesAlquileresCabaña("El usuario no es dueño de la cabaña seleccionada");
                var resultado = Contexto.AlquileresCabañas.Include(alq=>alq.Usuario).Where(alq => alq.Cabaña.NumeroHabitacion == idCabaña && alq.Cabaña.Usuario.Email.Valor.ToLower() == emailUsuario.ToLower()).ToList();
                return resultado;
            }
            catch (ExcepcionesBaseDeDatos ex)
            {
                throw new ExcepcionesBaseDeDatos("Error al conectarse a la base de datos " + ex.Message);
            }
        }
        //Obtener alquileres que yo usuario he realizado
        public IEnumerable<AlquilerCabaña> ObtenerAlquileresRealizadosPorUsuario(string emailUsuario)
        {
            try
            {
                if (emailUsuario == null || emailUsuario.Length < 1) throw new ExepcionesAlquileresCabaña("Credenciales no validas");
                var user= ObtenerUsuarioPorEmail(emailUsuario);
                if(user == null) throw new ExepcionesAlquileresCabaña("El usuario no existe");
                var resultado = Contexto.AlquileresCabañas.Include(alq => alq.Cabaña).Where(alq => alq.Usuario == user).ToList();
                return resultado;
            }
            catch (ExcepcionesBaseDeDatos ex)
            {
                throw new ExcepcionesBaseDeDatos("Error al conectarse a la base de datos " + ex.Message);
            }
        }

        private Usuario ObtenerUsuarioPorEmail(string email)
        {
            var user = Contexto.Usuarios.Where(usu => usu.Email.Valor.ToLower() == email.ToLower()).FirstOrDefault();
            return user;
        }
    }
}
