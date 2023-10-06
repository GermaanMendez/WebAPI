using CasosUso.CU_AlquilerCabaña.InterfacesCU;
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
    public class CU_EditarAlquilerCabaña : IEditarAlquilerCabaña
    {
        IRepositorioAlquilerCabaña RepoAlquiler { get; set; }
        public CU_EditarAlquilerCabaña(IRepositorioAlquilerCabaña repo)
        {
            RepoAlquiler = repo;
        }

        public void EditarAlquilerCabaña(AlquilerCabañaDTO alquiler, string emailUsuarioAlquilo)
        {
           AlquilerCabaña alqActualizar = alquiler.ToAlquilerCabaña();
           alqActualizar.Validar();
           RepoAlquiler.EditarAlquilerCabaña(emailUsuarioAlquilo, alqActualizar);
        }
    }
}
