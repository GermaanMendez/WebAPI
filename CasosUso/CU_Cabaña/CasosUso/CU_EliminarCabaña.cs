using CasosUso.CU_Cabaña.InterfacesCU;
using Dominio_Interfaces.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasosUso.CU_Cabaña.CasosUso
{
    public class CU_EliminarCabaña : IEliminarCabaña
    {
        public IRepositorioCabaña Repositorio { get; set; }
        public CU_EliminarCabaña( IRepositorioCabaña repo)
        {
            Repositorio = repo;
        }

        public bool EliminarCabaña(string emailDueño, int idCabaña)
        {
            return Repositorio.EliminarCabaña(emailDueño, idCabaña);
        }
    }
}
