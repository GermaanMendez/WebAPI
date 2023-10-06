using CasosUso.CU_Cabaña.InterfacesCU;
using Dominio_Interfaces.EnitdadesNegocio;
using Dominio_Interfaces.InterfacesRepositorios;
using DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CasosUso.CU_Cabaña.CasosUso
{
    public class CU_ListarTodas : IListarTodas
    {
        IRepositorioCabaña RepoCabaña { get; set; }
        public CU_ListarTodas(IRepositorioCabaña repo)
        {
            RepoCabaña = repo;
        }
        public IEnumerable<CabañaDTO> ListarTodas()
        {
            return RepoCabaña.GetAll().Select(cab => new CabañaDTO()
            {
                NumeroHabitacion = cab.NumeroHabitacion,
                Nombre = cab.Nombre.valor,
                Foto = cab.Foto,
                Descripcion = cab.Descripcion.valor,
                PoseeJacuzzi = cab.PoseeJacuzzi,
                EstaHabilitada = cab.EstaHabilitada,
                CantidadPersonasMax = cab.CantidadPersonasMax,
                tipoCabaña=cab.TipoCabaña,
                IdTipoCabaña = cab.IdTipoCabaña,
                Usuario = cab.Usuario,
                //IdUsuario = cab.IdUsuario,
                PrecioDiario= cab.PrecioPorDia

            });
        }
    }
}

