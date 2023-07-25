using Datos.ContextoEF;
using Dominio_Interfaces.EnitdadesNegocio;
using Dominio_Interfaces.ExepcionesPropias;
using Dominio_Interfaces.InterfacesRepositorios;
using Microsoft.EntityFrameworkCore;
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
                var existeYa = Contexto.Cabañas.Where(cab => cab.Nombre.valor == obj.Nombre.valor).FirstOrDefault();
                if (existeYa==null)
                {
                    Contexto.Cabañas.Add(obj);
                    Contexto.SaveChanges();
                }
                else
                {
                    throw new ExcepcionesCabaña("Ya existe una cabaña con ese nombre en el sistema");
                }
            }
            catch (ExcepcionesBaseDeDatos ex)
            {
                throw new ExcepcionesBaseDeDatos("Error al acceder a la base de datos" + ex.Message);
            }
        }

        public IEnumerable<Cabaña> BuscarCabañaPorCantPersonas(int cantPersonas)
        {
            try
            {
                var ListaCabañas = Contexto.Cabañas.Where(cab => cab.CantidadPersonasMax >=cantPersonas).ToList();
                return ListaCabañas;
            }
            catch (ExcepcionesBaseDeDatos ex)
            {
                throw new ExcepcionesBaseDeDatos("Error al conectarse con la base de datos" + ex.Message);
            }
        }
        public IEnumerable<Cabaña> BuscarCabañaPorTexto(string texto)
        {
            try
            {
                texto.ToLower();
                var ListaCabañas = Contexto.Cabañas.Where(cab => cab.Nombre.valor.ToLower() == texto || cab.Nombre.valor.ToLower().Contains(texto)).ToList();
                return ListaCabañas;
            }
            catch (ExcepcionesBaseDeDatos ex)
            {
                throw new ExcepcionesBaseDeDatos("Error al conectarse con la base de datos" + ex.Message);
            }
        }

        public IEnumerable<Cabaña> BuscarCabañaPorTipo(int idTipoCabaña)
        {
            try
            {
                var ListaCabañas = Contexto.Cabañas.Where(cab => cab.IdTipoCabaña == idTipoCabaña).ToList();
                return ListaCabañas;

            }
            catch (ExcepcionesBaseDeDatos ex)
            {
                throw new ExcepcionesBaseDeDatos("Error al conectarse con la base de datos" + ex.Message);
            }
        }

        public Cabaña FindById(int id)
        {
            try
            {
                Cabaña aBuscar = Contexto.Cabañas.Find(id);
                return aBuscar;

            }
            catch (ExcepcionesBaseDeDatos ex)
            {
                throw new ExcepcionesBaseDeDatos("Error al conectarse con la base de datos" + ex.Message);
            }
        }

        public IEnumerable<Cabaña> GetAll()
        {
            try
            {
                var listaCabañas = Contexto.Cabañas.Include(cab=>cab.TipoCabaña).ToList();

                return listaCabañas;
            }
            catch (ExcepcionesBaseDeDatos ex)
            {
                throw new ExcepcionesBaseDeDatos("Error al conectarse con la base de datos" + ex.Message);
            }
        }

        public IEnumerable<Cabaña> ObtenerCabañasHabilitadas()
        {
            try
            {
                var ListaCabañas = Contexto.Cabañas.Where(cab => cab.EstaHabilitada == true).ToList();
                return ListaCabañas;

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
                Cabaña aBorrar = FindById(id);
                if (aBorrar == null)
                {
                    throw new ExcepcionesCabaña("La cabaña que se quiere borrar no existe en el sistema");
                }
                var TieneAlMenosUnMantenimiento = Contexto.Mantenimientos.Include(mant => mant.Cabaña).Where(mant=> mant.Cabaña.NumeroHabitacion==id).FirstOrDefault();
                if (TieneAlMenosUnMantenimiento != null)
                {
                    throw new ExcepcionesCabaña("La cabaña que se quiere borrar tiene uno o mas mantenimientos asignados. Por favor primero borre los mantenimientos correspondientes y vuelva a intentarlo.");
                }
                else
                {
                    Contexto.Cabañas.Remove(aBorrar);
                    Contexto.SaveChanges();
                    return true;
                }
            }
            catch (ExcepcionesBaseDeDatos ex)
            {
                throw new ExcepcionesBaseDeDatos("Error al conectarse con la base de datos" + ex.Message);
            }
        }

        public void Update(Cabaña obj)
        {
            try
            {
                obj.Validar();
                if (Contexto.Cabañas.Find(obj.NumeroHabitacion) != null)
                {
                    Contexto.Cabañas.Update(obj);
                    Contexto.SaveChanges();
                }
                else
                {
                    throw new ExcepcionesCabaña("La cabaña que se quiere actualizar no existe en el sistema");
                }
            }
            catch (ExcepcionesBaseDeDatos ex)
            {
                throw new ExcepcionesBaseDeDatos("Error al conectarse con la base de datos" + ex.Message);
            }
        }


        //SELECT OBLIGATORIOR 2
        //Dado un monto, obtener el nombre y capacidad(cantidad de huéspedes que puede alojar) de las cabañas que tengan un 
        // costo diario menor a ese monto, que tengan jacuzzi y estén habilitadas para reserva.
        public IEnumerable<Cabaña> ObtenerCabañasPorTipoYMonto( double monto)
        {
            try
            {
                    var resultado = Contexto.Cabañas.Include(cab => cab.TipoCabaña)
                                    .Where(cab => cab.CantidadPersonasMax  * cab.TipoCabaña.CostoPorHuesped < monto && cab.PoseeJacuzzi == true   && cab.EstaHabilitada == true)
                                    .ToList();
                    return resultado;
            }
            catch (ExcepcionesBaseDeDatos ex)
            {
                throw new ExcepcionesBaseDeDatos("Error al conectarse con la base de datos" + ex.Message);
            }
        }
    }
}
