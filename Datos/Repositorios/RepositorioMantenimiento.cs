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
            try
            {
                obj.Validar();
                var FechaaAgregar = obj.FechaMantenimiento;
                var cuantosMantenimientosDiariosTiene = Contexto.Mantenimientos.Where(mant =>mant.IdCabaña==obj.IdCabaña&& mant.FechaMantenimiento.Year == FechaaAgregar.Year &&
                                                        mant.FechaMantenimiento.Month == FechaaAgregar.Month && mant.FechaMantenimiento.Day == FechaaAgregar.Day)
                                                        .Count();
                if (cuantosMantenimientosDiariosTiene <= 2)
                {
                    Contexto.Mantenimientos.Add(obj);
                    Contexto.SaveChanges();
                }
                else
                {
                    throw new ExcepcionesMantenimiento("No se puede agregar mas de 3 mantenimientos diarios a una misma cabaña");
                }

            }
            catch (ExcepcionesBaseDeDatos ex)
            {
                throw new ExcepcionesBaseDeDatos("Error al acceder a la base de datos" + ex.Message);
            }
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
                throw new ExcepcionesBaseDeDatos("Error al conectarse con la base de datos" + ex.Message);
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
                throw new ExcepcionesBaseDeDatos("Error al conectarse con la base de datos" + ex.Message);
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
                throw new ExcepcionesBaseDeDatos("Error al conectarse con la base de datos" + ex.Message);
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
                throw new ExcepcionesBaseDeDatos("Error al conectarse con la base de datos" + ex.Message);
            }
        }

        public bool Remove(int id)
        {
            try
            {
                Mantenimiento aBorrar = FindById(id);
                if (aBorrar == null)
                {
                    throw new ExcepcionesMantenimiento("El mantenimiento que se quiere borrar no existe en el sistema");
                }
                Contexto.Mantenimientos.Remove(aBorrar);
                Contexto.SaveChanges(); 
               return true;
            }
            catch (ExcepcionesBaseDeDatos ex)
            {
                throw new ExcepcionesBaseDeDatos("Error al conectarse con la base de datos" + ex.Message);
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
                    throw new ExcepcionesMantenimiento("El mantenimiento que se quiere actualizar no existe en el sistema");
                }
            }
            catch (ExcepcionesBaseDeDatos ex)
            {
                throw new ExcepcionesBaseDeDatos("Error al conectarse con la base de datos" + ex.Message);
            }
        }


        //SELECT PARA OBLIGATORIOR 2 
        //b.Dados dos valores, obtener los mantenimientos realizados a las cabañas con una capacidad que
        //esté comprendida(topes inclusive) entre ambos valores.El resultado se agrupará por nombre de
        //la persona que realizó el mantenimiento, e incluirá el nombre de la persona y el monto total de
        //los mantenimientos que realizó.
        public IEnumerable<Mantenimiento> ObtenerMantenimientosPorValores(double valor1, double valor2)
        {
            try
            {
                var resultado = Contexto.Mantenimientos.Include(mant => mant.Cabaña).Where(cab => cab.Cabaña.CantidadPersonasMax >= valor1 && cab.Cabaña.CantidadPersonasMax <= valor2).ToList();
                return resultado;
            }
            catch (ExcepcionesBaseDeDatos ex)
            {
                throw new ExcepcionesBaseDeDatos("Error al conectarse con la base de datos" + ex.Message);
            }
        }
    }
}
