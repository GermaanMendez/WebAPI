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
                        throw new ExepcionesAlquileresCabaña("The cabin is not available to rent in that date range");
                    }
                }
                else
                {
                    throw new ExepcionesAlquileresCabaña("The Rent to create cannot be null");
                }
            }
            catch (ExcepcionesBaseDeDatos ex)
            {
                throw new ExcepcionesBaseDeDatos("Error connecting to database " + ex.Message);
            }
        }

        public bool EliminarAlquilerCabaña(string emailUsuario, int idAlquiler)
        {
            try
            {
                if (idAlquiler < 1 || emailUsuario==null || emailUsuario.Length<1) throw new ExepcionesAlquileresCabaña("Invalid credentials to delete");
                var usuario = ObtenerUsuarioPorEmail(emailUsuario);
                if (usuario == null) throw new ExepcionesAlquileresCabaña("The user you are trying to delete does not exist on the system");
                var alquilerEliminar = Contexto.AlquileresCabañas.Where(alq => alq.IdAlquiler == idAlquiler).FirstOrDefault();
                if (alquilerEliminar == null) throw new ExepcionesAlquileresCabaña("The rental you are trying to delete does not exist in the system");
                if (alquilerEliminar.Usuario == usuario)
                {
                    Contexto.AlquileresCabañas.Remove(alquilerEliminar);
                    Contexto.SaveChanges();
                    return true;
                }
                else
                {
                    throw new ExepcionesAlquileresCabaña("The user does not have the necessary permissions to delete the rental");
                }
            }
            catch (ExcepcionesBaseDeDatos ex)
            {
                throw new ExcepcionesBaseDeDatos("Error connecting to database " + ex.Message);
            }
        }

        public void EditarAlquilerCabaña(string emailUsuario, AlquilerCabaña obj)
        {
            try
            {
                if (obj == null || emailUsuario == null) throw new ExepcionesAlquileresCabaña("The rental to be edited cannot be null");
                var usuario = ObtenerUsuarioPorEmail(emailUsuario);
                if (usuario == null) throw new ExepcionesAlquileresCabaña("The user trying to edit the rental does not exist in the system");
                if (obj.Usuario == usuario)
                {
                    obj.Validar();
                    Contexto.AlquileresCabañas.Update(obj);
                    Contexto.SaveChanges();
                }
                else
                {
                    throw new ExepcionesAlquileresCabaña("The user trying to edit the rental does not have the necessary permissions");
                }
            }
            catch (ExcepcionesBaseDeDatos ex)
            {
                throw new ExcepcionesBaseDeDatos("Error connecting to database " + ex.Message);
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
            if(id<1) throw new ExepcionesAlquileresCabaña("The rental id to search must be a number greater than 0");
            var resultado = Contexto.AlquileresCabañas.Include(alq=>alq.Usuario).Include(alq=>alq.Cabaña).Where(alq=>alq.IdAlquiler==id).FirstOrDefault();
            return resultado;
        }
        public IEnumerable<AlquilerCabaña> ObtenerAlquileresDeMiCabaña(string emailUsuario, int idCabaña)
        {
            try
            {
                if(string.IsNullOrEmpty(emailUsuario) ||idCabaña<1) throw new ExepcionesAlquileresCabaña("A valid email and cabin ID must be provided");
                var cabañaBuscada = Contexto.Cabañas.Include(cab=>cab.Usuario).Where(cab => cab.NumeroHabitacion == idCabaña).FirstOrDefault();
                if (cabañaBuscada.Usuario.Email.Valor.ToLower()!=emailUsuario.ToLower()) throw new ExepcionesAlquileresCabaña("The user is not the owner of the cabin");
                var resultado = Contexto.AlquileresCabañas.Include(alq=>alq.Usuario).Where(alq => alq.Cabaña.NumeroHabitacion == idCabaña && alq.Cabaña.Usuario.Email.Valor.ToLower() == emailUsuario.ToLower()).ToList();
                return resultado;
            }
            catch (ExcepcionesBaseDeDatos ex)
            {
                throw new ExcepcionesBaseDeDatos("Error connecting to database " + ex.Message);
            }
        }
        //Obtener alquileres que yo usuario he realizado
        

        private Usuario ObtenerUsuarioPorEmail(string email)
        {
            var user = Contexto.Usuarios.Where(usu => usu.Email.Valor.ToLower() == email.ToLower()).FirstOrDefault();
            return user;
        }
    }
}
