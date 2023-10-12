using CasosUso.CU_AlquilerCabaña.InterfacesCU;
using CasosUso.CU_Cabaña.InterfacesCU;
using CasosUso.CU_Usuario.CUInterfaces;
using Dominio_Interfaces.InterfacesRepositorios;
using DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasosUso.CU_AlquilerCabaña.CasosUso
{
    public class CU_ListarAlquileresDeMiCabañaDueño : IListarAlquileresDeMiCabañaDueño
    {
        IRepositorioAlquilerCabaña RepoAlquiler { get; set; }
        IConvertUserToDTO ConvertUserToDTO { get; set; }
        IConvertCabañaToDTO ConvertCabañaToDTO { get; set; }
        public CU_ListarAlquileresDeMiCabañaDueño(IRepositorioAlquilerCabaña repo, IConvertUserToDTO convert, IConvertCabañaToDTO convertCabaña)
        {
            RepoAlquiler = repo;
            ConvertUserToDTO = convert;
            ConvertCabañaToDTO = convertCabaña;
        }
        public IEnumerable<AlquilerCabañaDTO> ListarAlquileresDeMiCabañaDueño(string emailUsuario, int idCabaña)
        {
            return RepoAlquiler.ObtenerAlquileresDeMiCabaña(emailUsuario, idCabaña).Select(alq => new AlquilerCabañaDTO()
            {
                FechaAlquilerDesde = alq.FechaAlquilerDesde,
                FechaAlquilerHasta = alq.FechaAlquilerDesde,
                Precio = alq.Precio,
                UsuarioId = alq.UsuarioId,
                Usuario = ConvertUserToDTO.ToUsuarioDTO(alq.Usuario),
                CabañaId = alq.CabañaId,
                Cabaña = ConvertCabañaToDTO.convertCabañaToDTO(alq.Cabaña)
            }) ;
        }
    }
}
