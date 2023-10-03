using CasosUso.CU_AlquilerCabaña.InterfacesCU;
using CasosUso.CU_Cabaña.CasosUso;
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
        public CU_BuscarAlquilerPorId(IRepositorioAlquilerCabaña rpo)
        {
            RepoAlquiler = rpo;
        }
        public AlquilerCabañaDTO ObtenerPorId(int idAqluiler)
        {
            AlquilerCabañaDTO alquilerDto = null;
            AlquilerCabaña alq = RepoAlquiler.FindById(idAqluiler);
            if (alq != null)
            {
                alquilerDto = new AlquilerCabañaDTO()
                {
                    FechaAlquilerDesde = alq.FechaAlquilerDesde,
                    FechaAlquilerHasta = alq.FechaAlquilerDesde,
                    Precio = alq.Precio,
                    UsuarioId = alq.UsuarioId,
                    Usuario = alq.Usuario,
                    CabañaId = alq.CabañaId
                };
            }
            return alquilerDto;
        }
    }
}
