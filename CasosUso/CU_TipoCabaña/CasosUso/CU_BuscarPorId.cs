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
    public class CU_BuscarPorId : IBuscarTipoPorId
    {
        IRepositorioTipoCabaña RepositorioTipoCabaña { get; set; }
        public CU_BuscarPorId(IRepositorioTipoCabaña repo)
        {
            RepositorioTipoCabaña = repo;
        }
        public TipoCabañaDTO BuscarPorId(int id)
        {
            TipoCabañaDTO dtoBuscado = null;
            TipoCabaña buscado = RepositorioTipoCabaña.FindById(id);
            if (buscado != null)
            {
                dtoBuscado = new TipoCabañaDTO()
                {
                    Id = buscado.Id,
                    Nombre = buscado.Nombre,
                    Descripcion = buscado.Descripcion,
                };
            }
            return dtoBuscado;
        }
    }
}
