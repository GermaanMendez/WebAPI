using CasosUso.CU_Cabaña.InterfacesCU;
using Dominio_Interfaces.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasosUso.CU_Cabaña.CasosUso
{
    public class CU_HabilitarCabaña : IHabilitarCabaña
    {
        IRepositorioCabaña RepositorioCabaña { get; set; }
        public CU_HabilitarCabaña(IRepositorioCabaña rpo)
        {
            RepositorioCabaña = rpo;
        }
        public bool HabiliarCabaña(string email, int idCabaña)
        {
            return RepositorioCabaña.HabilitarCabaña(email, idCabaña);
        }
    }
}
