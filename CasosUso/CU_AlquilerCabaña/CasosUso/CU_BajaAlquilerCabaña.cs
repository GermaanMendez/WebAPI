using CasosUso.CU_AlquilerCabaña.InterfacesCU;
using Dominio_Interfaces.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasosUso.CU_AlquilerCabaña.CasosUso
{
    public class CU_BajaAlquilerCabaña : IEliminarAlquilerCabaña
    {

        IRepositorioAlquilerCabaña RepoAlquiler { get;set; }
        public CU_BajaAlquilerCabaña(IRepositorioAlquilerCabaña rpo)
        {
            RepoAlquiler = rpo;
        }
        public bool EliminarAlquilerCabaña(int id, string emailUsuarioAlquilo)
        {
            return RepoAlquiler.EliminarAlquilerCabaña(emailUsuarioAlquilo, id);
        }
    }
}
