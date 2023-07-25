using Dominio_Interfaces.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasosUso.CU_Parametros
{
    public class CU_ObtenerValorParametro : IObtenerValorParam
    {
        IRepositorioParametros RepoParam { get; set; }
        public CU_ObtenerValorParametro(IRepositorioParametros repo)
        {
            RepoParam = repo;
        }
        public string ValorParametro(string nombre)
        {
            return RepoParam.ValorParametro(nombre);
        }
    }
}
