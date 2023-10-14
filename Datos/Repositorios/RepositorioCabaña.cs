using Datos.ContextoEF;
using Dominio_Interfaces.EnitdadesNegocio;
using Dominio_Interfaces.ExepcionesPropias;
using Dominio_Interfaces.InterfacesRepositorios;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Datos.Repositorios
{
    public class RepositorioCabaña : IRepositorioCabaña
    {
        public LibreriaContexto Contexto { get; set; }
        public RepositorioCabaña(LibreriaContexto ctx)
        {
            Contexto = ctx;
        }
        public void Add(Cabaña obj)
        {
            try
            {
                obj.Validar();
                var existeTipo = Contexto.TiposCabañas.Where(tipo => tipo.Id == obj.IdTipoCabaña).FirstOrDefault();
                var existeDueño = Contexto.Usuarios.Where(usu => usu.Id == obj.Usuario.Id).FirstOrDefault();
                var existeYa = Contexto.Cabañas.Where(cab => cab.Nombre.valor == obj.Nombre.valor).FirstOrDefault();
                if (existeYa==null && existeDueño!=null && existeTipo!=null)
                {
                    obj.EstaHabilitada = true;
                    Contexto.Cabañas.Add(obj);
                    Contexto.SaveChanges();
                }
                else if(existeYa != null)
                {
                    throw new ExcepcionesCabaña("There is already a cabin with that name in the system");
                }
                else if(existeDueño==null)
                {
                    throw new ExcepcionesCabaña("The user trying to create the cabin does not exist in the system");
                }
                else
                {
                    throw new ExcepcionesCabaña("The type of cabin selected does not exist in the system");
                }
            }
            catch (ExcepcionesBaseDeDatos ex)
            {
                throw new ExcepcionesBaseDeDatos("Error connecting to database" + ex.Message);
            }
        }

        public IEnumerable<Cabaña> BuscarCabañaPorCantPersonas(int cantPersonas)
        {
            try
            {
                var ListaCabañas = Contexto.Cabañas.Where(cab => cab.CantidadPersonasMax >= cantPersonas && cab.EstaHabilitada).Include(cab => cab.Usuario).ToList();
                return ListaCabañas;
            }
            catch (ExcepcionesBaseDeDatos ex)
            {
                throw new ExcepcionesBaseDeDatos("Error connecting to database" + ex.Message);
            }
        }
        public IEnumerable<Cabaña> BuscarCabañaPorTexto(string texto)
        {
            try
            {
                texto.ToLower();
                var ListaCabañas = Contexto.Cabañas.Where(cab => cab.Nombre.valor.ToLower() == texto || cab.Nombre.valor.ToLower().Contains(texto) && cab.EstaHabilitada).Include(cab => cab.Usuario).ToList();
                return ListaCabañas;
            }
            catch (ExcepcionesBaseDeDatos ex)
            {
                throw new ExcepcionesBaseDeDatos("Error connecting to database" + ex.Message);
            }
        }

        public IEnumerable<Cabaña> BuscarCabañaPorTipo(int idTipoCabaña)
        {
            try
            {
                var ListaCabañas = Contexto.Cabañas.Where(cab => cab.IdTipoCabaña == idTipoCabaña && cab.EstaHabilitada).Include(cab => cab.Usuario).ToList();
                return ListaCabañas;
            }
            catch (ExcepcionesBaseDeDatos ex)
            {
                throw new ExcepcionesBaseDeDatos("Error connecting to database" + ex.Message);
            }
        }

        public Cabaña FindById(int id)
        {
            try
            {
                Cabaña aBuscar = Contexto.Cabañas.Include(cab => cab.Usuario).Include(cab=>cab.TipoCabaña).FirstOrDefault(cab=>cab.NumeroHabitacion==id);
                return aBuscar;
            }
            catch (ExcepcionesBaseDeDatos ex)
            {
                throw new ExcepcionesBaseDeDatos("Error connecting to database" + ex.Message);
            }
        }

        public IEnumerable<Cabaña> GetAll()
        {
            try
            {
                var listaCabañas = Contexto.Cabañas.Include(cab=>cab.TipoCabaña).Where(cab=>cab.EstaHabilitada).Include(cab => cab.Usuario).ToList();

                return listaCabañas;
            }
            catch (ExcepcionesBaseDeDatos ex)
            {
                throw new ExcepcionesBaseDeDatos("Error connecting to database" + ex.Message);
            }
        }

        public IEnumerable<Cabaña> ObtenerCabañasNOHabilitadas()
        {
            try
            {
                var ListaCabañas = Contexto.Cabañas.Where(cab => cab.EstaHabilitada == false).Include(cab=>cab.Usuario).ToList();
                return ListaCabañas;

            }
            catch (ExcepcionesBaseDeDatos ex)
            {
                 throw new ExcepcionesBaseDeDatos("Error connecting to database" + ex.Message);
            }
        }

        public bool Remove(int id)
        {
            throw new Exception("Not Implemented");
        }
        public void Update(Cabaña obj)
        {
            throw new Exception("Not Implemented");
        }
        public void EditarCabaña(Cabaña obj, string email)
        {
            try
            {
                obj.Validar();
                var cabaña = Contexto.Cabañas.Where(cab => cab.NumeroHabitacion == obj.NumeroHabitacion).Include(cab=>cab.Usuario).FirstOrDefault();
                if(cabaña == null) throw new ExcepcionesCabaña("The cabin that you want to edit does not exist in the system");
                if(cabaña.Usuario.Email.Valor.ToLower()!=email.ToLower()) throw new ExcepcionesCabaña("The user that is trying to edit the cabin is not the owner of the cabin");
                Contexto.Cabañas.Update(obj);
                Contexto.SaveChanges();
            }
            catch (ExcepcionesBaseDeDatos ex)
            {
                throw new ExcepcionesBaseDeDatos("Error connecting to database" + ex.Message);
            }
        }

        public IEnumerable<Cabaña> ObtenerCabañasMonto(int monto)
        {
            try
            {
                    var resultado = Contexto.Cabañas.Include(cab => cab.TipoCabaña)
                                    .Include(cab=>cab.Usuario)
                                    .Where(cab=>cab.PrecioPorDia <= monto && cab.EstaHabilitada)
                                    .ToList();
                    return resultado;
            }
            catch (ExcepcionesBaseDeDatos ex)
            {
                throw new ExcepcionesBaseDeDatos("Error connecting to database" + ex.Message);
            }
        }

        public bool EliminarCabaña(string emailDueño, int idCabaña)
        {
            try
            {
                Usuario dueño = Contexto.Usuarios.Where(usu => usu.Email.Valor.ToLower() == emailDueño.ToLower()).FirstOrDefault();
                if (dueño != null)
                {
                    Cabaña cabaña= Contexto.Cabañas.Where(cab => cab.NumeroHabitacion == idCabaña).Include(cab=>cab.Usuario).FirstOrDefault();
                    if (cabaña != null)
                    {
                        if(dueño.Id==cabaña.Usuario.Id || dueño.Rol.Valor == "administrador")
                        {
                            var mantenimientosCabaña = Contexto.Mantenimientos.Where(mant => mant.IdCabaña == cabaña.NumeroHabitacion);
                            if (mantenimientosCabaña.Any())
                            {
                              Contexto.Mantenimientos.RemoveRange(mantenimientosCabaña);
                            }
                            Contexto.Cabañas.Remove(cabaña);
                            Contexto.SaveChanges();
                            return true;
                        }
                        else
                        {
                            throw new ExcepcionesCabaña("The user does not have the necessary permissions to delete the cabin");
                        }
                    }
                    else
                    {
                        throw new ExcepcionesCabaña("The cabin you want to delete does not exist in the system");
                    }
                }
                else
                {
                    throw new ExcepcionesCabaña("The user with Email: " + emailDueño + " doesn't exists.");
                }
            }
            catch (ExcepcionesBaseDeDatos ex)
            {
                throw new ExcepcionesBaseDeDatos("Error connecting to database" + ex.Message);
            }
        }

        public IEnumerable<Cabaña> ObtenerCabañasDisponiblesEnRangoFecha(DateTime desde, DateTime hasta)
        {
            try
            {
                if(desde < hasta)
                {
                    var cabañasConAlquileresEnRango = Contexto.AlquileresCabañas.Where(alquiler =>(alquiler.FechaAlquilerDesde >= desde && alquiler.FechaAlquilerDesde <= hasta) ||(alquiler.FechaAlquilerHasta >= desde && alquiler.FechaAlquilerHasta <= hasta)&& alquiler.Cabaña.EstaHabilitada)
                                                                                .Select(alquiler => alquiler.Cabaña.NumeroHabitacion)
                                                                                .ToList();

                    var cabañasDisponibles = Contexto.Cabañas
                                              .Include(cabaña=>cabaña.Usuario)
                                              .Include(cabaña=>cabaña.TipoCabaña)
                                              .Where(cabaña => !cabañasConAlquileresEnRango.Contains(cabaña.NumeroHabitacion))
                                              .ToList(); // Excluir cabañas con alquileres en el rango
                    return cabañasDisponibles;
                }
                else
                {
                    throw new ExcepcionesCabaña("The start date cannot be greater than the end date");
                }
            }
            catch (ExcepcionesBaseDeDatos ex)
            {
                throw new ExcepcionesBaseDeDatos("Error connecting to database" + ex.Message);
            }
        }

        public bool DeshabilitarCabaña(string emailDueño, int idCabaña)
        {
            try
            {
                if(emailDueño!=null && idCabaña >= 0)
                {
                    var usuario = Contexto.Usuarios.Where(usu => usu.Email.Valor.ToLower() == emailDueño.ToLower()).FirstOrDefault();
                    if(usuario==null) throw new ExcepcionesCabaña("The user doesn't exist in the system. ");
                    if (usuario.Rol.Valor.ToLower() != "administrador") throw new ExcepcionesCabaña("The user does not have the necessary permissions ");
                    var cabaña = Contexto.Cabañas.Where(cab => cab.NumeroHabitacion == idCabaña).FirstOrDefault();
                    if(cabaña==null) throw new ExcepcionesCabaña("The cabin doesn't exist in the system. ");
                    cabaña.EstaHabilitada = false;
                    Contexto.SaveChanges();
                    return true;
                }
                else
                {
                    throw new ExcepcionesCabaña("A valid email and cabin must be provided ");
                }
            }
            catch (ExcepcionesBaseDeDatos ex)
            {
                throw new ExcepcionesBaseDeDatos("error connecting to database" + ex.Message);
            }
        }

        public bool HabilitarCabaña(string emailDueño, int idCabaña)
        {
            try
            {
                if (emailDueño != null && idCabaña >= 0)
                {
                    var usuario = Contexto.Usuarios.Where(usu => usu.Email.Valor.ToLower() == emailDueño.ToLower()).FirstOrDefault();
                    if (usuario == null) throw new ExcepcionesCabaña("The user doesn't exist in the system. ");
                    if (usuario.Rol.Valor.ToLower() != "administrador") throw new ExcepcionesCabaña("The user does not have the necessary permissions ");
                    var cabaña = Contexto.Cabañas.Where(cab => cab.NumeroHabitacion == idCabaña).FirstOrDefault();
                    if (cabaña == null) throw new ExcepcionesCabaña("The cabin doesn't exist in the system. ");
                    cabaña.EstaHabilitada = true;
                    Contexto.SaveChanges();
                    return true;
                }
                else
                {
                    throw new ExcepcionesCabaña("A valid email and cabin must be provided ");
                }
            }
            catch (ExcepcionesBaseDeDatos ex)
            {
                throw new ExcepcionesBaseDeDatos("error connecting to database" + ex.Message);
            }
        }
    }
}

