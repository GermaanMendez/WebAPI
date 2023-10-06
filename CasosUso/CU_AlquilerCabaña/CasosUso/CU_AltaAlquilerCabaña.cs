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
    public class CU_AltaAlquilerCabaña : IAltaAlquilerCabaña
    {
        IRepositorioAlquilerCabaña RepoAlquiler { get; set; }
        IRepositorioCabaña RepositorioCabaña { get; set; }
        IRepositorioUsuario RepositorioUsuario { get; set; }    
        public CU_AltaAlquilerCabaña(IRepositorioAlquilerCabaña rpo, IRepositorioCabaña repositorioCabaña, IRepositorioUsuario repositorioUsuario)
        {
            RepoAlquiler = rpo;
            RepositorioCabaña = repositorioCabaña;
            RepositorioUsuario = repositorioUsuario;
        }

        public void AltaAlquilerCabaña(AlquilerCabañaNuevoDTO nuevo)
        {
            AlquilerCabaña alq = nuevo.ToAlquilerCabaña();
            Usuario UsuarioAlquila = RepositorioUsuario.GetUsuarioById(nuevo.UsuarioId);
            Cabaña CabañaAlquilada = RepositorioCabaña.FindById(nuevo.CabañaId);
            alq.Usuario=UsuarioAlquila;
            alq.Cabaña = CabañaAlquilada;
            alq.Validar();
            RepoAlquiler.Add(alq);
        }
    }
}
