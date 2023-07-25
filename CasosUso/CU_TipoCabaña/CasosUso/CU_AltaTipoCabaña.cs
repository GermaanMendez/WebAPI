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
    public class CU_AltaTipoCabaña : IAltaTipoCabaña
    {
        IRepositorioTipoCabaña RepoTipoCabaña { get; set; }
        IRepositorioParametros RepoParam { get; set; }
        public CU_AltaTipoCabaña(IRepositorioTipoCabaña  repoCab, IRepositorioParametros repoParam)
        {
            RepoTipoCabaña = repoCab;
            RepoParam = repoParam;  
        }
        public void AltaTipoCabaña(TipoCabañaDTO nuevo)
        {
            TipoCabaña nuevotipo = nuevo.ToTipoCabaña();
            TipoCabaña.CantMinCarDescripcionTipoCabaña = int.Parse(RepoParam.ValorParametro("CantMinCarDescripcionTipoCabaña"));
            TipoCabaña.CantMaxCarDescripcionTipoCabaña = int.Parse(RepoParam.ValorParametro("CantMaxCarDescripcionTipoCabaña"));
            
            nuevotipo.Validar();
            RepoTipoCabaña.Add(nuevotipo);
            nuevo.Id = nuevotipo.Id;

        }
    }
}