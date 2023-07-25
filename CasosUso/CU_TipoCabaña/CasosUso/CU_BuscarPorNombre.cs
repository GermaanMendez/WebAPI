using CasosUso.CU_TipoCabaña.InterfacesCU;
using Dominio_Interfaces.EnitdadesNegocio;
using Dominio_Interfaces.InterfacesRepositorios;
using DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasosUso.CU_TipoCabaña.CasosUso
{
    public class CU_BuscarPorNombre : IBuscarPorNomre
    {
        IRepositorioTipoCabaña RepoTipoCabaña { get; set; }
        public CU_BuscarPorNombre(IRepositorioTipoCabaña repo)
        {
            RepoTipoCabaña = repo;   
        }
        public TipoCabañaDTO BuscarTipoPorNombre(string nombre)
        {
            TipoCabañaDTO buscadoDto = null;
            TipoCabaña buscado = RepoTipoCabaña.BuscarTipoPorNombre(nombre);
            if (buscado != null)
            {
                buscadoDto = new TipoCabañaDTO()
                {
                    Id = buscado.Id,
                    Nombre = buscado.Nombre,
                    Descripcion = buscado.Descripcion,
                    CostoPorHuesped = buscado.CostoPorHuesped,
                };
            }
            return buscadoDto;
        }
    }
}
