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
    public class CU_ActualizarTipoCabaña : IActualizarTipoCabaña
    {
        IRepositorioTipoCabaña RepoTipoCabaña { get; set; }
        IRepositorioParametros RepoParam { get; set; }
        public CU_ActualizarTipoCabaña(IRepositorioTipoCabaña  repo,IRepositorioParametros repoParam)
        {
            RepoTipoCabaña  = repo;
            RepoParam= repoParam;   
        }
        public void ActualizarTipoCabaña(TipoCabañaDTO aActualizar)
        {
            TipoCabaña cabActualizar= aActualizar.ToTipoCabaña();

            TipoCabaña.CantMinCarDescripcionTipoCabaña = int.Parse(RepoParam.ValorParametro("CantMinCarDescripcionTipoCabaña"));
            TipoCabaña.CantMaxCarDescripcionTipoCabaña = int.Parse(RepoParam.ValorParametro("CantMaxCarDescripcionTipoCabaña"));

            cabActualizar.Validar();
            RepoTipoCabaña.Update(cabActualizar);
        }
    }
}
