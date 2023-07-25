using CasosUso.CU_TipoCabaña.InterfacesCU;
using Dominio_Interfaces.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasosUso.CU_TipoCabaña.CasosUso
{
    public class CU_BajaTipoCabaña : IEliminarTipoCabaña
    {
        IRepositorioTipoCabaña RepositorioTipoCabaña { get; set; }
        public CU_BajaTipoCabaña(IRepositorioTipoCabaña repo)
        {
            RepositorioTipoCabaña = repo;   
        }
        public bool BajaTipoCabaña(int idTipo)
        {
            return RepositorioTipoCabaña.Remove(idTipo);    
        }
    }
}
