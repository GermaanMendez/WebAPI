using CasosUso.CU_Cabaña.InterfacesCU;
using CasosUso.CU_Usuario.CUInterfaces;
using Dominio_Interfaces.EnitdadesNegocio;
using DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasosUso.CU_Cabaña.CasosUso
{
    public class CU_ConvertCabañaToDTO : IConvertCabañaToDTO
    {
        IConvertUserToDTO ConvertUserToDTO { get; set; }
        public CU_ConvertCabañaToDTO(IConvertUserToDTO convert)
        {
            ConvertUserToDTO = convert;
        }
        public CabañaDTO convertCabañaToDTO(Cabaña cabaña)
        {
            return new CabañaDTO()
            {
                NumeroHabitacion = cabaña.NumeroHabitacion,
                Nombre = cabaña.Nombre.valor,
                Foto = cabaña.Foto,
                Descripcion = cabaña.Descripcion.valor,
                PoseeJacuzzi = cabaña.PoseeJacuzzi,
                EstaHabilitada = cabaña.EstaHabilitada,
                CantidadPersonasMax = cabaña.CantidadPersonasMax,
                PrecioDiario = cabaña.PrecioPorDia,
                IdTipoCabaña = cabaña.IdTipoCabaña,
                tipoCabaña = cabaña.TipoCabaña,
                Usuario = ConvertUserToDTO.ToUsuarioDTO(cabaña.Usuario),
            };
        }
    }
}
