using CasosUso.CU_AlquilerCabaña.InterfacesCU;
using CasosUso.CU_Cabaña.CasosUso;
using CasosUso.CU_Cabaña.InterfacesCU;
using CasosUso.CU_Usuario.CUInterfaces;
using Dominio_Interfaces.EnitdadesNegocio;
using Dominio_Interfaces.InterfacesRepositorios;
using DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasosUso.CU_AlquilerCabaña.CasosUso
{
    public class CU_BuscarAlquilerPorId:IBuscarAlquilerPorId
    {
        IRepositorioAlquilerCabaña RepoAlquiler {  get; set; }
        IConvertUserToDTO ConvertUserToDTO { get; set; }
        IConvertCabañaToDTO ConvertCabañaToDTO { get; set; }
        public CU_BuscarAlquilerPorId(IRepositorioAlquilerCabaña rpo, IConvertUserToDTO conver, IConvertCabañaToDTO convertCabaña)
        {
            RepoAlquiler = rpo;
            ConvertUserToDTO = conver;
            ConvertCabañaToDTO = convertCabaña;
        }
        public AlquilerCabañaDTO ObtenerPorId(int idAqluiler)
        {
            AlquilerCabañaDTO alquilerDto = null;
            AlquilerCabaña alq = RepoAlquiler.FindById(idAqluiler);
            if (alq != null)
            {
                alquilerDto = new AlquilerCabañaDTO()
                {
                    Id = alq.IdAlquiler,
                    FechaAlquilerDesde = alq.FechaAlquilerDesde,
                    FechaAlquilerHasta = alq.FechaAlquilerDesde,
                    Precio = alq.Precio,
                    UsuarioId = alq.UsuarioId,
                    Cabaña=ConvertCabañaToDTO.convertCabañaToDTO(alq.Cabaña),
                    Usuario = ConvertUserToDTO.ToUsuarioDTO(alq.Usuario),
                    CabañaId = alq.CabañaId
                };
            }
            return alquilerDto;
        }
    }
}
