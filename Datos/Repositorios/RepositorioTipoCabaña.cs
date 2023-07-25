using Datos.ContextoEF;
using Dominio_Interfaces.EnitdadesNegocio;
using Dominio_Interfaces.ExepcionesPropias;
using Dominio_Interfaces.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Repositorios
{
    public class RepositorioTipoCabaña : IRepositorioTipoCabaña
    {
        public LibreriaContexto Contexto { get; set; }
        public RepositorioTipoCabaña(LibreriaContexto ctx)
        {
            Contexto = ctx;
        }
        public void Add(TipoCabaña obj)
        {
            try
            {
                obj.Validar();
                obj.Nombre.ToLower();
                var existeYa = Contexto.TiposCabañas.Where(tipo => tipo.Nombre.ToLower() == obj.Nombre).FirstOrDefault();
                if (existeYa == null)
                {
                    Contexto.TiposCabañas.Add(obj);
                    Contexto.SaveChanges();
                }
                else
                {
                    throw new ExcepcionesTipoCabaña("Ya existe un tipo de cabaña con ese nombre en el sistema");
                }
            }
            catch (ExcepcionesBaseDeDatos ex)
            {
                throw new ExcepcionesBaseDeDatos("Error al acceder a la base de datos" + ex.Message);
            }
        }

        public TipoCabaña BuscarTipoPorNombre(string nombre)
        {
            try
            {
                var tipoCabañaBuscado = Contexto.TiposCabañas.Where(tipo => tipo.Nombre.ToLower() == nombre.ToLower()).FirstOrDefault();
                return tipoCabañaBuscado;


            }
            catch (ExcepcionesBaseDeDatos ex )
            {
                throw new ExcepcionesBaseDeDatos("Error al acceder a la base de datos" + ex.Message);
            }
        }
        public TipoCabaña FindById(int id)
        {
            try
            {
                TipoCabaña aBuscar = Contexto.TiposCabañas.Find(id);
                return aBuscar;

            }
            catch (ExcepcionesBaseDeDatos ex)
            {
                throw new ExcepcionesBaseDeDatos("Error al conectarse con la base de datos" + ex.Message);
            }
        }

        public IEnumerable<TipoCabaña> GetAll()
        {
            try
            {
                var listaTiposCabañas = Contexto.TiposCabañas.ToList();
                return listaTiposCabañas;
            }
            catch (ExcepcionesBaseDeDatos ex)
            {
                throw new ExcepcionesBaseDeDatos("Error al conectarse con la base de datos" + ex.Message);
            }
        }
        public List<TipoCabaña> GetAllLista()
        {
            try
            {
                var listaTiposCabañas = Contexto.TiposCabañas.ToList();
                return listaTiposCabañas;
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
                TipoCabaña aBorrar = FindById(id);
                if (aBorrar == null)
                {
                    throw new ExcepcionesTipoCabaña("El tipo de cabaña que se quiere borrar no existe en el sistema");
                }
                var HayAlMenosUnaCabañaDelTipo = Contexto.Cabañas.Where(cab => cab.IdTipoCabaña == id).FirstOrDefault();
                if (HayAlMenosUnaCabañaDelTipo != null)
                {
                    throw new ExcepcionesTipoCabaña("El tipo de cabaña que se quiere borrar es usado por una o mas cabañas. Por favor primero borre las cabañas correspondientes y vuelva a intentarlo.");
                }
                else
                {
                    Contexto.TiposCabañas.Remove(aBorrar);
                    Contexto.SaveChanges();
                    return true;
                }
            }
            catch (ExcepcionesBaseDeDatos ex)
            {
                throw new ExcepcionesBaseDeDatos("Error al conectarse con la base de datos" + ex.Message);
            }
        }

        public void Update(TipoCabaña obj)
        {
            try
            {
                    obj.Validar();
                    Contexto.TiposCabañas.Update(obj);
                    Contexto.SaveChanges();
            }
            catch (ExcepcionesBaseDeDatos ex)
            {
                throw new ExcepcionesBaseDeDatos("Error al conectarse con la base de datos" + ex.Message);
            }
        }

    }
}
