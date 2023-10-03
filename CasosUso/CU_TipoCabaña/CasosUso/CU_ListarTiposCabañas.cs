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
    public class CU_ListarTiposCabañas : IListarTiposCabañas
    {
        IRepositorioTipoCabaña RepoTipoCabaña { get; set; }
        public CU_ListarTiposCabañas(IRepositorioTipoCabaña repo)
        {
            RepoTipoCabaña = repo;  
        }
        public List<TipoCabañaDTO> ListarTodosLista()
        {
            return null;
        }
        public IEnumerable<TipoCabañaDTO> ListarTodos()
        {
            return RepoTipoCabaña.GetAll().Select(mant => new TipoCabañaDTO()
            {
                Id = mant.Id,
                Nombre = mant.Nombre,
                Descripcion = mant.Descripcion,
            }); ;
        }

    }
}
