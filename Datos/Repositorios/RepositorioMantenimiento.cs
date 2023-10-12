using Datos.ContextoEF;
using Dominio_Interfaces.EnitdadesNegocio;
using Dominio_Interfaces.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio_Interfaces.ExepcionesPropias;
using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace Datos.Repositorios
{
    public class RepositorioMantenimiento : IRepositorioMantenimiento
    {
        public LibreriaContexto Contexto { get; set; }
        public RepositorioMantenimiento(LibreriaContexto ctx)
        {
            Contexto=ctx;
        }
        public void Add(Mantenimiento obj)
        {
            
        }

        public Mantenimiento FindById(int id)
        {
            try
            {
                Mantenimiento aBuscar=Contexto.Mantenimientos.Find(id);
                return aBuscar;
            }
            catch (ExcepcionesBaseDeDatos ex)
            {
                throw new ExcepcionesBaseDeDatos("Error connecting to database" + ex.Message);
            }
        }

        public IEnumerable<Mantenimiento> GetAll()
        {
            try
            {
                var listaMantenimientos = Contexto.Mantenimientos.ToList();
                return listaMantenimientos;
            }
            catch (ExcepcionesBaseDeDatos ex)
            {
                throw new ExcepcionesBaseDeDatos("Error connecting to database" + ex.Message);
            }
        }

        public IEnumerable<Mantenimiento> ObtenerMantenimientosPorCabaña(int idCabaña)
        {
            try
            {
                
                var resultado = Contexto.Mantenimientos.Include(mant => mant.Cabaña).Where(mant => mant.Cabaña.NumeroHabitacion == idCabaña).ToList();
                return resultado;
            }
            catch (ExcepcionesBaseDeDatos ex)
            {
                throw new ExcepcionesBaseDeDatos("Error connecting to database" + ex.Message);
            }
        }

        public IEnumerable<Mantenimiento> ObtenerMantenimientosPorCabañaPorFechas(int idCabaña, DateTime fecha1, DateTime fecha2)
        {
            try
            {
                var resultado = Contexto.Mantenimientos.Include(mant => mant.Cabaña).OrderByDescending(mant=>mant.CostoMantenimiento).Where(mant => mant.Cabaña.NumeroHabitacion == idCabaña && mant.FechaMantenimiento >= fecha1 && mant.FechaMantenimiento <= fecha2).ToList();

                return resultado;
            }
            catch (ExcepcionesBaseDeDatos ex)
            {
                throw new ExcepcionesBaseDeDatos("Error connecting to database" + ex.Message);
            }
        }

        public bool Remove(int id)
        {
            try
            {
                Mantenimiento aBorrar = FindById(id);
                if (aBorrar == null)
                {
                    throw new ExcepcionesMantenimiento("The maintenance you want to delete does not exist in the system");
                }
                Contexto.Mantenimientos.Remove(aBorrar);
                Contexto.SaveChanges(); 
               return true;
            }
            catch (ExcepcionesBaseDeDatos ex)
            {
                throw new ExcepcionesBaseDeDatos("Error connecting to database" + ex.Message);
            }
        }

        public void Update(Mantenimiento obj)
        {
            try
            {
                obj.Validar();
                if (Contexto.Mantenimientos.Find(obj.Id) != null)
                {
                    Contexto.Mantenimientos.Update(obj);
                    Contexto.SaveChanges();
                }
                else
                {
                    throw new ExcepcionesMantenimiento("The maintenance you want to update does not exist in the system");
                }
            }
            catch (ExcepcionesBaseDeDatos ex)
            {
                throw new ExcepcionesBaseDeDatos("Error connecting to database" + ex.Message);
            }
        }

        public IEnumerable<Mantenimiento> ObtenerMantenimientosPorValores(double valor1, double valor2)
        {
            try
            {
                var resultado = Contexto.Mantenimientos.Include(mant => mant.Cabaña).Where(cab => cab.Cabaña.CantidadPersonasMax >= valor1 && cab.Cabaña.CantidadPersonasMax <= valor2).ToList();
                return resultado;
            }
            catch (ExcepcionesBaseDeDatos ex)
            {
                throw new ExcepcionesBaseDeDatos("Error connecting to database" + ex.Message);
            }
        }

        public void AddMaintenance(Mantenimiento obj, string email)
        {
            try
            {
                var owner = Contexto.Usuarios.Where(usu => usu.Email.Valor.ToLower() == email.ToLower()).FirstOrDefault();
                if (owner == null)
                {
                    throw new ExcepcionesMantenimiento("The user that is trying add a new maintenance doesn't exists in the system");
                }
                else
                {
                    var cabinToAddMaintenance = Contexto.Cabañas.Include(cab=>cab.Usuario).Where(cab => cab.NumeroHabitacion == obj.IdCabaña).FirstOrDefault();
                    if (cabinToAddMaintenance == null) { throw new ExcepcionesMantenimiento("The cabin doesn't exists"); }
                    if (cabinToAddMaintenance.Usuario.Id==owner.Id)
                    {
                        obj.Validar();
                        var FechaaAgregar = obj.FechaMantenimiento;
                        var cuantosMantenimientosDiariosTiene = Contexto.Mantenimientos.Where(mant => mant.IdCabaña == obj.IdCabaña && mant.FechaMantenimiento.Year == FechaaAgregar.Year &&
                                                                mant.FechaMantenimiento.Month == FechaaAgregar.Month && mant.FechaMantenimiento.Day == FechaaAgregar.Day)
                                                                .Count();
                        if (cuantosMantenimientosDiariosTiene <= 2)
                        {
                            Contexto.Mantenimientos.Add(obj);
                            Contexto.SaveChanges();
                        }
                        else
                        {
                            throw new ExcepcionesMantenimiento("No more than 3 maintenances can be added on the same date.");
                        }
                    }
                    else
                    {
                        throw new ExcepcionesMantenimiento("The user who is trying to add a new maintenance to the cabin is not the owner of the cabin");
                    }
                   
                }
            }
            catch (ExcepcionesBaseDeDatos ex)
            {
                throw new ExcepcionesBaseDeDatos("Error connecting to database" + ex.Message);
            }
        }
    }
}
